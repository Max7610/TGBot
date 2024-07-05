using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGBot.Classes.neuron
{
    internal class Neuron
    {
        decimal[] _value;
        int _count;
        decimal[] _input;
        decimal _output;
        decimal _porogSum;
        decimal _localGrad;
        public decimal output
        {
            get
            {
                return _output;
            }
        }
        public Neuron(int count)
        {
            _count = count + 1;
            _value = new decimal[_count];
            _input = new decimal[_count];

            for (int i = 0; i < _count; i++)
            {
                var r = new Random();
                _value[i] = Convert.ToDecimal(r.NextDouble());
                System.Threading.Thread.Sleep(1);
            }
        }
        public Neuron(int count, string s)
        {
            _count = count + 1;
            _value = new decimal[_count];
            _input = new decimal[_count];
            string[] value = s.Split('#');
            for (int i = 0; i < _count; i++)
            {
                try
                {
                    _value[i] = Convert.ToDecimal(value[i]);
                }
                catch { Console.WriteLine("Ошибка в загрузке весов"); }
            }
        }
        public void ConsolePrint()
        {
            string s = "";
            foreach (var i in _value)
            {
                s += i.ToString() + " ";
            }
            Console.WriteLine(s);
        }
        public void InputInitialDate(decimal[] date)
        {
            if (date.Length < _count)
            {
                for (int i = 0; i < date.Length; i++)
                {
                    _input[i] = date[i];
                }
                _input[_count - 1] = 1;
            }
            CalculatingOutput();

        }
        void CalculatingOutput()
        {
            _porogSum = 0;
            for (int i = 0; i < _count; i++)
            {
                _porogSum += _input[i] * _value[i];
            }
            _output = Sigmoid(_porogSum);
        }
        decimal Sigmoid(decimal s)
        {
            return Convert.ToDecimal(1 / (1 + Math.Exp(Convert.ToDouble(-s))));
        }
        decimal DeaktSigmoid(decimal x)
        {
            return Sigmoid(x) * (1 - Sigmoid(x));
        }
        public void PrintOutput()
        {
            Console.WriteLine(_output);
        }
        public void LearningOutput(decimal y, decimal l)
        {
            _localGrad = y * _output * (1 - _output);
            for (int i = 0; i < _count; i++)
            {
                _value[i] -= _localGrad * l * _input[i];
            }
        }
        public decimal LocalGrad(int numberValue)
        {
            return _localGrad * _value[numberValue];
        }
        public string ReturnString()
        {
            string resault = "";
            foreach (var i in _value)
            {
                resault += i.ToString() + "#";
            }
            return resault;
        }
        public void LoadValue(string value)
        {
            var a = value.Split('#');
            for (int i = 0; i < _count; i++)
            {
                _value[i] = Convert.ToDecimal(a[i]);
            }
        }
    }
}
