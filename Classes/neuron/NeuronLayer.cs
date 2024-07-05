using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGBot.Classes.neuron
{
    internal class NeuronLayer
    {
        List<Neuron> _neuronList;
        int _count;
        int _countInput;

        public NeuronLayer(int count, int countInput)
        {
            _neuronList = new List<Neuron>();
            _count = count;
            _countInput = countInput;
            for (int i = 0; i < _count; i++) _neuronList.Add(new Neuron(countInput));
        }
        public NeuronLayer(string l, int count, int countInput)
        {
            _neuronList = new List<Neuron>();
            _count = count;
            _countInput = countInput;
            string[] s = l.Split('$');
            try
            {
                for (int i = 0; i < count; i++) _neuronList.Add(new Neuron(_countInput, s[i]));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ошибка в создании нейронов");
            }
        }
        public decimal[] OutputLayer(decimal[] input)
        {
            decimal[] outputLayer = new decimal[_count];
            for (int i = 0; i < _count; i++)
            {
                _neuronList[i].InputInitialDate(input);
                outputLayer[i] = _neuronList[i].output;
            }
            return outputLayer;
        }
        public decimal[] LearningLayerGradient(decimal[] e, decimal l)
        {
            decimal[] gradientList = new decimal[_countInput];
            for (int i = 0; i < _count; i++)
            {
                _neuronList[i].LearningOutput(e[i], l);
            }
            for (int i = 0; i < _countInput; i++)
            {
                decimal d = 0;
                for (int j = 0; j < _count; j++)
                {
                    d += _neuronList[j].LocalGrad(i);
                }
                gradientList[i] = d;
            }

            return gradientList;
        }
        public void PrintNeuron()
        {
            foreach (var i in _neuronList)
            {
                Console.WriteLine("<<<<New Neuron>>>>");
                i.ConsolePrint();
            }
        }
        public string ReturnString()
        {
            string resault = "";
            foreach (var i in _neuronList)
            {
                resault += i.ReturnString() + "$";
            }
            return resault;
        }
        public void LoadValue(string value)
        {
            var a = value.Split('$');
            for (int i = 0; i < _count; i++)
            {
                _neuronList[i].LoadValue(a[i]);
            }
        }
    }
}
