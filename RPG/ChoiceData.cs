using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class ChoiceData
    {
        public string[] options;
        public bool[] selectable;
        public int[] returnValue;

        public ChoiceData(string[] options)
        {
            bool[] selectable = new bool[options.Length];
            int[] returnValue = new int[options.Length];
            for (int i = 0; i < selectable.Length; i++)
            {
                selectable[i] = true;
                returnValue[i] = i;
            }

            this.options = options;
            this.selectable = selectable;
            this.returnValue = returnValue;
        }

        public ChoiceData(string[] options, int[] returnValue)
        {
            bool[] selectable = new bool[options.Length];
            for(int i = 0; i < selectable.Length; i++)
            {
                selectable[i] = true;
            }

            this.options = options;
            this.selectable = selectable;
            this.returnValue = returnValue;
        }

        public ChoiceData(string[] options, bool[] selectable)
        {
            int[] returnValue = new int[options.Length];
            for (int i = 0; i < selectable.Length; i++)
            {
                returnValue[i] = i;
            }

            this.options = options;
            this.selectable = selectable;
            this.returnValue = returnValue;
        }

        public ChoiceData(string[] options, bool[] selectable, int[] returnValue)
        {
            this.options = options;
            this.selectable = selectable;
            this.returnValue = returnValue;
        }
    }
}
