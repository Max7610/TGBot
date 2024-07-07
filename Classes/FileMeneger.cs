using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TGBot;
using System.Globalization;



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
            DirectoryInfo dirInfoS = new DirectoryInfo(Path() + @"/save");
            if (!dirInfoS.Exists)
            {
                dirInfoS.Create();
            }
            DirectoryInfo dirInfoD = new DirectoryInfo(Path() + @"/data");
            if (!dirInfoD.Exists)
            {
                dirInfoD.Create();
            }
            path = Path() + @"/date/eurusd.txt";
            pathSave = Path() + @"/save/";
            random = new Random();
            try
            {
                streamReader = new StreamReader(path);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Read();
        }
        string Path()
        {
            string path = Directory.GetCurrentDirectory();

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
                mass[i] = ConvertDecimal(st[2]);
                mass[i + 1] = ConvertDecimal(st[3]);
                mass[i + 2] = ConvertDecimal(st[4]);
                mass[i + 3] = ConvertDecimal(st[5]);
            }
            ConsolWriteDate();
        }
        void ConsolWriteDate()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(mass[i]);
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
            foreach (var i in list) Console.WriteLine(i);
            return list.ToArray();
        }
        decimal ConvertDecimal(string s)
        {
            decimal number = 0;
            if (Decimal.TryParse(s, NumberStyles.Number, CultureInfo.CreateSpecificCulture("fr-FR"), out  number))
                return number;
            else if (Decimal.TryParse(s, NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-GB"), out number))
                return number;
            return number;
        }
        

    }
}
