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
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.OverlapRemoval;
using GraphX.PCL.Logic.Models;
using GraphX.Controls;
using QuickGraph;
using System.Windows;

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

        private void getAllInfo() 
        {
            Thread Thread1;
            InitializeMatrix();
            GridMatrix.TopLeftHeaderCell.Value ="K="+ mainLabSolver.KElem.ToString();
            Thread1 = new Thread(GetGroupsFunction);
            Thread1.Name = "secondary1";
            Thread1.Start();
            OutputMatrix();
            WaitForThread.WaitOne();
            Thread1 = new Thread(GetNewGroupsFunction);
            Thread1.Start();
            OutputGroups(ref GridGroups);
            WaitForThread.WaitOne();
            OutputGroups(ref GridNewGroups);
            mainLabSolver.StartLab4Task();
        }

        private void SolveBut_Click(object sender, EventArgs e)
        {
            if (!CheckgetAllInfo()) {
                errorProvider1.SetError((sender as Button), "check your textboxes for correcr input");
                return;
            }
            getAllInfo();
        }

        private void SolveBut_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OutConnectionMatrix(ref dataGridView1, Convert.ToInt16(textBox1.Text));
            dataGridView1.AutoResizeColumns();
        }

    }
}
