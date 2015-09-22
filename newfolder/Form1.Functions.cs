using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_form_1_
{
    partial class Form1
    {
        delegate void getAllInfoDelegate();
        /*private void CreateTextBox() {
            if (TextBoxes.Count != 0)
            {
                this.TextBoxes.Add(new TextBox());
                this.TextBoxes[TextBoxes.Count - 1].Size = this.TextBoxes[TextBoxes.Count - 2].Size;
                this.TextBoxes[TextBoxes.Count - 1].Location = new System.Drawing.Point(
                            this.TextBoxes[TextBoxes.Count - 2].Location.X,
                            this.TextBoxes[TextBoxes.Count - 2].Location.Y + 26);
                this.TextBoxes[TextBoxes.Count - 1].Name = "TextBoxes[" + Convert.ToString(TextBoxes.Count - 1) + "]";
                this.Controls.Add(TextBoxes[TextBoxes.Count - 1]);

            }
            else
            {
                this.TextBoxes.Add(new TextBox());
                this.TextBoxes[TextBoxes.Count - 1].Size = new System.Drawing.Size(23, 20);
                this.TextBoxes[TextBoxes.Count - 1].Location = new System.Drawing.Point(213, 30);
                this.TextBoxes[TextBoxes.Count - 1].Name = "TextBoxes[" +
                                                              Convert.ToString(TextBoxes.Count - 1) +
                                                              "]";
                this.Controls.Add(TextBoxes[TextBoxes.Count - 1]);
            }
        }*/
        private void CreateMatrixTextBox() {
        }
        private void CreateGroupTextBox() {

        }
    }
}
