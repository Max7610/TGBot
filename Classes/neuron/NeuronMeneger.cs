using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using TGBot.Classes.telegramBot;

namespace TGBot.Classes.neuron
{
    internal class NeuronMeneger : ICloneable
    {
        decimal _convergenceStep;
        List<NeuronLayer> _layers = new List<NeuronLayer>();
        FileMeneger fileMeneger;
        int[] _neyronStruct;
        public decimal statistic;
        public NeuronMeneger(int[] lauers)
        {
            fileMeneger = new FileMeneger();
            _neyronStruct = lauers;
            var a = new NeuronLayer(_neyronStruct[0], _neyronStruct[0]);
            _layers.Add(a);
            for (int i = 1; i < _neyronStruct.Length; i++)
            {
                a = new NeuronLayer(_neyronStruct[i], _neyronStruct[i - 1]);
                _layers.Add(a);
            }
        }
        public NeuronMeneger(string p)
        {
            fileMeneger = new FileMeneger();
            string path = "save/" + p.Split('/')[p.Split('/').Length - 1];
            string fileString = "";
            try
            {
                fileString = OpenSave(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} \n ошибка чтения файла");
            }
            string[] str = fileString.Split('\n')[0].Split(' ');
            _neyronStruct = new int[str.Length - 1];
            try
            {
                for (int i = 0; i < _neyronStruct.Length; i++)
                {
                    _neyronStruct[i] = Convert.ToInt32(str[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка формирования структуры");
            }
            string[] value = fileString.Split('\n')[1].Split('*');
            _layers.Add(new NeuronLayer(value[0], _neyronStruct[0], _neyronStruct[0]));

            for (int i = 1; i < _neyronStruct.Length; i++)
            {
                try
                {
                    _layers.Add(new NeuronLayer(value[i], _neyronStruct[i], _neyronStruct[i - 1]));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ошибка при создании слоя");
                }
            }
            Console.WriteLine("нейросеть загружена");
        }
        void LoadingValue(string save)
        {
            string[] s = save.Split('\n');
            List<int> l = new List<int>();
            var a = s[0].Split(' ');
            foreach (var i in a)
            {
                if (int.TryParse(i, out int r))
                    l.Add(Convert.ToInt32(r));
            }
            _neyronStruct = l.ToArray();
            MakeLauers(s[1]);
        }
        void MakeLauers(string value)
        {
            var a = new NeuronLayer(_neyronStruct[0], _neyronStruct[0]);
            var b = value.Split('*');
            _layers.Add(a);
            for (int i = 1; i < _neyronStruct.Length; i++)
            {
                a = new NeuronLayer(_neyronStruct[i], _neyronStruct[i - 1]);
                a.LoadValue(b[i]);
                _layers.Add(a);
            }
        }
        public void PrintLayers()
        {
            foreach (var i in _layers)
            {
                Console.WriteLine("-----New Layer-----");
                i.PrintNeuron();
            }
        }
        public decimal[] Colculait(decimal[] input)
        {
            /// decimal[] result = new decimal[_neyronStruct[_neyronStruct.Length-1]];
            for (int i = 0; i < _neyronStruct.Length; i++)
            {
                input = _layers[i].OutputLayer(input);
            }
            return input;
        }
        public void LearningNeuron(decimal[][] input, decimal[][] resault, int count, decimal step)
        {
            statistic = 0;
            _convergenceStep = step;
            for (int i = 0; i < count; i++)
            {
                var r = new Random();
                int n = r.Next(input.Length);
                LearningCycle(input[n], resault[n]);
            }
        }
        public decimal LearningNeuronGetStatistic(decimal[][] input, decimal[][] resault, int count, decimal step)
        {
            statistic = 0;
            _convergenceStep = step;
            var r = new Random();
            for (int i = 0; i < count; i++)
            {
                int n = r.Next(input.Length);
                LearningCycle(input[n], resault[n]);
            }
            return statistic / count;
        }
        void LearningCycle(decimal[] input, decimal[] resault)
        {
            var realResault = Colculait(input);
            decimal[] error = new decimal[realResault.Length];

            for (int i = 0; i < realResault.Length; i++)
            {
                error[i] = (realResault[i] - resault[i]);
            }
            foreach (var i in error)
            {
                statistic += Math.Abs(i);
            }
            for (int i = _neyronStruct.Length - 1; i >= 0; i--)
            {
                error = _layers[i].LearningLayerGradient(error, _convergenceStep);
            }
        }
        public override string ToString()
        {
            string resault = "";
            foreach (var i in _neyronStruct)
            {
                resault += i.ToString() + " ";
            }
            resault += "\n";
            foreach (var i in _layers)
            {
                resault += i.ReturnString() + "*";
            }
            return resault;
        }
        public string NeyronStruct()
        {
            string s = "";
            foreach (var i in _neyronStruct)
            {
                s += i.ToString() + "_";
            }
            return s;
        }
        public void FileSave()
        {
            DateTime dataTime = new DateTime();
            string fileName = NeyronStruct() + "_" + dataTime + ".txt";
            string path = Path() + "/save/" + fileName;
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(ToString());
                }

            }
            else
            {
                Directory.Delete(path, true);
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(ToString());
                }
            }
            Console.WriteLine($"Файл создан {NeyronStruct() + "_" + dataTime + ".txt"}");
        }
        public string OpenSave(string nameFile)
        {
            string resault = "";
            using (StreamReader sr = new StreamReader(nameFile))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    resault += s + "\n";
                }
            }
            return resault;
        }
        string Path()
        {
            string path = Directory.GetCurrentDirectory();
            
            return path;
        }
        public List<string> SaveList()
        {
            List<string> list = new List<string>();
            var path = Directory.GetCurrentDirectory();
            path += @"/save";
            
            var l = Directory.GetFiles(path);
            foreach (var f in l)
            {
                var a = f.Replace(@"/", " ");
                list.Add(a.Split(' ')[a.Split(' ').Length - 1]);
            }
            return list;
        }
        public void LearningForFile(int count, decimal step)
        {
            _convergenceStep = step;
            for (int i = 0; i < count; i++)
            {
                int countInput = _neyronStruct[0];
                int countOutput = _neyronStruct[_neyronStruct.Length - 1];
                decimal[] a = fileMeneger.ReadRandomArray(countInput, countOutput);
                decimal[] inp = new decimal[countInput];
                decimal[] outp = new decimal[countOutput];
                for (int j = 0; j < countInput; j++)
                {
                    inp[j] = (decimal)a[j] / 2;
                }
                for (int j = countInput; j < countInput + countOutput; j++)
                {
                    outp[j - countInput] = (decimal)a[j] / 2;
                }
                LearningCycle(inp, outp);
            }
        }

        public string TestForFile()
        {
            string res = "";
            int countInput = _neyronStruct[0];
            int countOutput = _neyronStruct[_neyronStruct.Length - 1];
            decimal[] a = fileMeneger.ReadRandomArray(countInput, countOutput);
            Console.WriteLine($"выбран рандомный интервал {a.Length}");
            decimal[] inp = new decimal[countInput];
            decimal[] outp = new decimal[countOutput];
            for (int j = 0; j < countInput; j++)
            {
                inp[j] = (decimal)a[j] / 2;
            }
            for (int j = countInput; j < countInput + countOutput; j++)
            {
                outp[j - countInput] = a[j];
            }
            Console.WriteLine($"Сгенерированы входные и выходные данные {inp.Length}  {outp.Length}") ;
            var r = Colculait(inp);
            for (int i = 0; i < countOutput; i++)
            {
                res += $"оригинал {outp[i]} результат {r[i] * 2} разность {outp[i] - (r[i] * 2)} \n";
            }
            Console.WriteLine("Получен результат");
            //try
            //{
            //    MakeGrapg grapg = new MakeGrapg();
            //    grapg.DrawGraph(inp, outp);
            //}catch (Exception ex)
            //{
            //    Console.WriteLine($"Ошибка создания изображения {ex.Message}");
            //}
            return res;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
