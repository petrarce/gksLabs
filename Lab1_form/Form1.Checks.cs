using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_form_1_
{
    partial class Form1
    {
        private bool CheckTextBoxes(String[] str){
            try
            {
                for (int i = 0; i > -1; i++)
                {
                    if (str[i] == "0") { };//костыль для выхода из цыкла
                    try
                    {
                        for (int j = i + 1; j > i; j++)
                        {
                            if (str[i].Equals(str[j]))
                                return false;
                        }
                    }
                    catch(IndexOutOfRangeException) { }
                }
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
            return true;
        }
        private bool CheckgetAllInfo() {
            foreach (newTextBox e in TextBoxes) {
                if (!e.isCorrectlyChecked)
                    return false;
            }
            return true;
        }
        private delegate bool DelegCheckTextBoxes(String[] str);

    }
}
