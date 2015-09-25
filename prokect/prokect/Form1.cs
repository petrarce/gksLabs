using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApplication1;
using System.Threading;

namespace Lab1_form_1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeComponent_user();
        }

        private void TextEntered(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                DelegCheckTextBoxes currentThread = new DelegCheckTextBoxes(CheckTextBoxes);
                IAsyncResult reult=currentThread.BeginInvoke((sender as TextBox).Text.Split(' '), null, null);
                if (!currentThread.EndInvoke(reult)) {
                    errorProvider1.SetError((sender as newTextBox),"this string has repeateing value");
                    (sender as newTextBox).isCorrectlyChecked = false;
                    return;
                }
                errorProvider1.Clear();
                (sender as newTextBox).isCorrectlyChecked = true;
            }
        }

        private void Del_Click(object sender, EventArgs e){

           this.Controls.Remove(TextBoxes[TextBoxes.Count - 1]);
           this.TextBoxes.RemoveAt(TextBoxes.Count - 1);
           this.addStr.Location = new System.Drawing.Point(
                                    this.addStr.Location.X,
                                    this.addStr.Location.Y - 26);
           if (TextBoxes.Count == 1)
            {
               DelButton.Visible = false;
               return;
           }
           this.DelButton.Location = new System.Drawing.Point(
                this.DelButton.Location.X,
                this.DelButton.Location.Y - 26);


           
        }

        private void addStr_Click(object sender, EventArgs e)
            
        {
            if (this.addStr.Location.Y + 100 < this.Size.Height)
            {
                NewTextbox();
                this.addStr.Location = new System.Drawing.Point(
                                        this.addStr.Location.X,
                                        this.addStr.Location.Y + 26);
                if (DelButton.Visible == true) this.DelButton.Location = new System.Drawing.Point(
                                          this.DelButton.Location.X,
                                          this.DelButton.Location.Y + 26);
                else DelButton.Visible = true;
            }


            
        }

        private void getAllInfo(){
            List<String[]> tmpVar=new List<String[]>();
            foreach(TextBox Element in TextBoxes){
                tmpVar.Add(Element.Text.Split(' '));
            }
            mainLabSolver = new lab1Solver() { OperList = tmpVar };
            MatrixTextBox.Text = "K= " + Convert.ToString(mainLabSolver.KElem)+"\n";
            foreach(Int16[] str in mainLabSolver.Matrix) {
                foreach (Int16 element in str) {
                    MatrixTextBox.Text += Convert.ToString(element)+" ";
                }
                MatrixTextBox.Text += "\n";
            }
            Int16 i=1;
            mainLabSolver.createGropus();
            foreach (Group group in mainLabSolver.Groups) {
                GroupsTextBox.Text += "Group " + i.ToString()+":";
                foreach (Int16 element in group.Elements) {
                    GroupsTextBox.Text += element.ToString() + " ";
                }
                GroupsTextBox.Text += "\n";
                i++;
            }
            mainLabSolver.GetNewGroups();
            

        }

        private void SolveBut_Click(object sender, EventArgs e)
        {
            if (!CheckgetAllInfo()) {
                errorProvider1.SetError((sender as Button), "check your textboxes for correcr input");
                return;
            }

            CreateGroupTextBox();
            CreateMatrixTextBox();
            getAllInfo();
        }

        private void SolveBut_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();

        }
    }
}
