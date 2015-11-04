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
using WindowsFormsProject;
using System.Windows;

namespace Lab1_form_1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeComponent_user();
            Load += Form1_Load;
        }


        private ZoomControl _zoomctrl;
        private GraphAreaExample _gArea;

        private UIElement GenerateWpfVisuals()
        {
            _zoomctrl = new ZoomControl();
            ZoomControl.SetViewFinderVisibility(_zoomctrl, Visibility.Visible);
            /* ENABLES WINFORMS HOSTING MODE --- >*/
            var logic = new GXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>();
            _gArea = new GraphAreaExample() { EnableWinFormsHostingMode = true, LogicCore = logic };
            logic.Graph = GenerateGraph();
            logic.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.LinLog;
            logic.DefaultLayoutAlgorithmParams = logic.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.LinLog);
            //((LinLogLayoutParameters)logic.DefaultLayoutAlgorithmParams). = 100;
            logic.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            logic.DefaultOverlapRemovalAlgorithmParams = logic.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;
            logic.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
            logic.AsyncAlgorithmCompute = false;
            _zoomctrl.Content = _gArea;
            _gArea.RelayoutFinished += gArea_RelayoutFinished;


            var myResourceDictionary = new ResourceDictionary { Source = new Uri("Templates\\template.xaml", UriKind.Relative) };
            _zoomctrl.Resources.MergedDictionaries.Add(myResourceDictionary);

            return _zoomctrl;
        }

        void gArea_RelayoutFinished(object sender, EventArgs e)
        {
            _zoomctrl.ZoomToFill();
        }

        private GraphExample GenerateGraph()
        {
            String _tmprStr;

            //FOR DETAILED EXPLANATION please see SimpleGraph example project
            var dataGraph = new GraphExample();
            foreach(Modul modul in mainLabSolver.Groups[0].Moduls)
            {
                _tmprStr = "";
                foreach (String str in modul.Operations)
                {
                    if (str != modul.Operations[0])
                        _tmprStr += ", ";
                    _tmprStr += str;
                }
                var dataVertex = new DataVertex(_tmprStr);
                dataGraph.AddVertex(dataVertex);
            }
            DataEdge dataEdge;
            var vlist = dataGraph.Vertices.ToList();
            //Then create two edges optionaly defining Text property to show who are connected
            foreach (var Row in mainLabSolver.Groups[0].ConnectionMatrix)
            {
                foreach(var Column in Row.Value)
                {
                    if (Column.Value)
                    {
                        dataEdge = new DataEdge(
                            vlist.Find(x => x.Text == Row.Key),
                            vlist.Find(x => x.Text == Column.Key))
                            {
                                Text = string.Format("{0} -> {1}",
                                vlist.Find(x => x.Text.Contains(Row.Key)),
                                vlist.Find(x => x.Text.Contains(Column.Key)))
                            };
                        dataGraph.AddEdge(dataEdge);
                    }
                        
                }
            }

            //dataEdge = new DataEdge(vlist[2], vlist[2]) { Text = string.Format("{0} -> {1}", vlist[2], vlist[2]) };
            //dataGraph.AddEdge(dataEdge);
            return dataGraph;
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
            /*if (!mainLabSolver.GetNewModul1(0))
            {
                System.Windows.Forms.MessageBox.Show("Graph is already done");
                return;
            }*/
            elementHost1.Child = GenerateWpfVisuals();
            _zoomctrl.ZoomToFill();
            _gArea.GenerateGraph(true);
            _gArea.SetVerticesDrag(true, true);
            _zoomctrl.ZoomToFill();

        }
    }
}
