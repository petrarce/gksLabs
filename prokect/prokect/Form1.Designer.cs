namespace Lab1_form_1_
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.addStr = new System.Windows.Forms.Button();
            this.DelButton = new System.Windows.Forms.Button();
            this.SolveBut = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // addStr
            // 
            this.addStr.Location = new System.Drawing.Point(38, 56);
            this.addStr.Name = "addStr";
            this.addStr.Size = new System.Drawing.Size(75, 23);
            this.addStr.TabIndex = 1;
            this.addStr.Text = "Add new";
            this.addStr.UseVisualStyleBackColor = true;
            this.addStr.Click += new System.EventHandler(this.addStr_Click);
            // 
            // DelButton
            // 
            this.DelButton.Location = new System.Drawing.Point(211, 57);
            this.DelButton.Name = "DelButton";
            this.DelButton.Size = new System.Drawing.Size(24, 20);
            this.DelButton.TabIndex = 4;
            this.DelButton.Text = "-";
            this.DelButton.UseVisualStyleBackColor = true;
            this.DelButton.Visible = false;
            this.DelButton.Click += new System.EventHandler(this.Del_Click);
            // 
            // SolveBut
            // 
            this.SolveBut.Location = new System.Drawing.Point(310, 372);
            this.SolveBut.Name = "SolveBut";
            this.SolveBut.Size = new System.Drawing.Size(75, 23);
            this.SolveBut.TabIndex = 5;
            this.SolveBut.Text = "Solve";
            this.SolveBut.UseVisualStyleBackColor = true;
            this.SolveBut.Click += new System.EventHandler(this.SolveBut_Click);
            this.SolveBut.Leave += new System.EventHandler(this.SolveBut_Leave);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkRate = 0;
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(609, 407);
            this.Controls.Add(this.SolveBut);
            this.Controls.Add(this.DelButton);
            this.Controls.Add(this.addStr);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        private void InitializeComponent_user(){
            this.TextBoxes = new System.Collections.Generic.List<newTextBox>();
            this.MatrixTextBox = new System.Windows.Forms.RichTextBox();
            this.GroupsTextBox = new System.Windows.Forms.RichTextBox();
            this.mainLabSolver = new ConsoleApplication1.lab1Solver();

            // 
            // GroupsTextBox
            // 
            this.GroupsTextBox.Location = new System.Drawing.Point(462, 71);
            this.GroupsTextBox.Name = "GroupsTextBox";
            this.GroupsTextBox.Size = new System.Drawing.Size(135, 132);
            this.GroupsTextBox.TabIndex = 6;
            this.GroupsTextBox.Text = "";
            this.GroupsTextBox.ReadOnly = true;
            // 
            // MatrixTextBox
            // 
            this.MatrixTextBox.Location = new System.Drawing.Point(310, 71);
            this.MatrixTextBox.Name = "MatrixTextBox";
            this.MatrixTextBox.Size = new System.Drawing.Size(135, 132);
            this.MatrixTextBox.TabIndex = 6;
            this.MatrixTextBox.Text = "";
            this.MatrixTextBox.ReadOnly = true;
            //
            //TextBoxes
            //
            this.TextBoxes.Add(new newTextBox());
            this.TextBoxes[TextBoxes.Count - 1].Size = new System.Drawing.Size(169, 20);
            this.TextBoxes[TextBoxes.Count - 1].Location = new System.Drawing.Point(38, 31);
            this.TextBoxes[TextBoxes.Count - 1].Name = "TextBox[" + (TextBoxes.Count - 1).ToString() + "]";
            this.TextBoxes[TextBoxes.Count - 1].Leave += new System.EventHandler(TextEntered);
            this.TextBoxes[TextBoxes.Count - 1].Text = (TextBoxes.Count - 1).ToString();
            //
            //
            //
            this.Controls.Add(TextBoxes[TextBoxes.Count - 1]);
            this.Controls.Add(GroupsTextBox);
            this.Controls.Add(MatrixTextBox);
        }
        private delegate void EventHeandler1(object sender, System.EventArgs e, System.String str);
        private void NewTextbox() {
            if (TextBoxes.Count != 0)
            {
                this.TextBoxes.Add(new newTextBox());
                this.TextBoxes[TextBoxes.Count - 1].Size = this.TextBoxes[TextBoxes.Count - 2].Size;
                this.TextBoxes[TextBoxes.Count - 1].Location = new System.Drawing.Point(
                            this.TextBoxes[TextBoxes.Count - 2].Location.X,
                            this.TextBoxes[TextBoxes.Count - 2].Location.Y + 26);
                this.TextBoxes[TextBoxes.Count - 1].Name = "TextBoxes[" + (TextBoxes.Count - 1).ToString() + "]";
                this.TextBoxes[TextBoxes.Count - 1].Leave += new System.EventHandler(TextEntered);
                this.TextBoxes[TextBoxes.Count - 1].Text = (TextBoxes.Count - 1).ToString();
                this.Controls.Add(TextBoxes[TextBoxes.Count - 1]);

            }
         }
        #endregion
        private class newTextBox : System.Windows.Forms.TextBox {
            public bool isCorrectlyChecked;
        }
        private System.Windows.Forms.Button addStr;
        private System.Collections.Generic.List<newTextBox> TextBoxes;
        private System.Windows.Forms.Button DelButton;
        private ConsoleApplication1.lab1Solver mainLabSolver;
        private System.Windows.Forms.Button SolveBut; 
        private System.Windows.Forms.RichTextBox GroupsTextBox;
        private System.Windows.Forms.RichTextBox MatrixTextBox;
        private System.Windows.Forms.ErrorProvider errorProvider1;

    }
}

