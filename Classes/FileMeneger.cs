using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TGBot;



namespace TGBot.Classes
{
    internal class FileMeneger
    {
        StreamReader streamReader;
        string path;
        string pathSave;
        decimal[] mass;
        string[] massSt;
        string eurusd;
        Random random;
        public FileMeneger()

        {
            DirectoryInfo dirInfoS = new DirectoryInfo(Path()+"//save");
            if (!dirInfoS.Exists)
            {
                dirInfoS.Create();   
            }
            DirectoryInfo dirInfoD = new DirectoryInfo(Path() + "//data");
            if (!dirInfoD.Exists)
            {
                dirInfoD.Create();
            }
            path = Path() + @"\\data\\eurusd.txt";
            pathSave = Path() + @"\\save\\";
            random = new Random();
            streamReader = new StreamReader(path);
            Read();
        }
        string Path()
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Replace(@"\", @"\\");
            return path;
        }
        void Read()
        {
            List<string> resault = new List<string>();
            while (!streamReader.EndOfStream)
            {
                resault.Add(streamReader.ReadLine());
            }
            massSt = resault.ToArray();
            int n = massSt.Length;
            mass = new decimal[n * 4];
            for (int i = 0; i < n * 4 - 4; i += 4)
            {
                string[] st = massSt[i / 4].Split(',');
                mass[i] = decimal.Parse(st[2]);
                mass[i + 1] = decimal.Parse(st[3]);
                mass[i + 2] = decimal.Parse(st[4]);
                mass[i + 3] = decimal.Parse(st[5]);
            }
        }
        public decimal[] ReadRandomArray(int input, int output)
        {
            decimal[] resault = new decimal[input + output];
            int r = random.Next((massSt.Length - input - output) / 4);
            for (int i = 0; i < input + output; i++)
            {
                resault[i] = mass[i + r * 4];
            }
            return resault;
        }
        public string[] SaveList()
        {
            string[] list = Directory.GetFiles(pathSave);
            return list.ToArray();
        }
        

    }
}
