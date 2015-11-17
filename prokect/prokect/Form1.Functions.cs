﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApplication1;
using System.Threading;
using System.Data;

namespace Lab1_form_1_
{
    partial class Form1
    {

        private void InitializeMatrix(){
            List<String[]> tmpVar=new List<String[]>();
            foreach(TextBox Element in TextBoxes){
                tmpVar.Add(Element.Text.Split(' '));
            }
            mainLabSolver = new lab1Solver() { OperList = tmpVar };

        }

        
        private void OutputMatrix() {
            GridMatrix.ColumnCount = GridMatrix.RowCount = mainLabSolver.Matrix.Length;
            for(Int16 i=0;i<mainLabSolver.Matrix.Length;i++){
                GridMatrix.Columns[i].Name = (i + 1).ToString();
                for (Int16 j = 0; j < mainLabSolver.Matrix.Length; j++) {
                    GridMatrix.Rows[j].HeaderCell.Value = (j + 1).ToString();
                    GridMatrix.Rows[j].Cells[i].Value = Convert.ToString(mainLabSolver.Matrix[i][j]);
                }
            }
            GridMatrix.AutoResizeColumns();
        }
        private void OutputGroups(ref DataGridView Grid)
        {
            Grid.Rows.Clear();
            Grid.Columns.Clear();
            Grid.RowCount = mainLabSolver.Groups.Count;
            Grid.ColumnCount = 1;
            Grid.Columns[0].HeaderCell.Value = "Elements";
            for (Int16 i = 0; i < mainLabSolver.Groups.Count; i++)
            {
                Grid.Rows[i].HeaderCell.Value = (i + 1).ToString();
                foreach (Int16 element in mainLabSolver.Groups[i].Elements) {
                    Grid.Rows[i].Cells[0].Value += element.ToString() + "-> ";
                }
            }
            Grid.AutoResizeColumns();
        }
        private void OutConnectionMatrix(ref DataGridView Grid,Matrix Matrix)
        {
            Grid.ColumnCount = Grid.RowCount = Matrix.Values.Count;
            int i=0,j=0;
            foreach (var row in Matrix)
            {
                j=0;
                foreach (var Value in row.Value.Values)
                {
                    Grid.Rows[i].HeaderCell.Value = row.Key;
                    Grid.Columns[i].HeaderCell.Value = row.Key;
                    Grid.Rows[i].Cells[j].Value = Value.ToString();
                    j++;
                    
                }
                i++;
            }

        }
        private void OutConnectionMatrix(ref DataGridView Grid, Int16 GroupNumber)
        {
            OutConnectionMatrix(ref Grid, mainLabSolver.Groups[GroupNumber].ConnectionMatrix);
        }
        private void OutModul(ref DataGridView Grid, Int16 GroupNumber)
        {
            int i = 0;
            String Str="";
            Grid.RowCount = mainLabSolver.Groups[GroupNumber].Moduls.Count;
            Grid.ColumnCount = 1;
            Grid.Columns[0].HeaderText = "Operations";
            foreach (var modul in mainLabSolver.Groups[GroupNumber].Moduls)
            {
                Str = "";
                foreach (var operation in modul.Operations)
                {
                    Str += operation.ToString()+" ;";
                }
                Grid.Rows[i].Cells[0].Value = Str;
                Grid.Rows[i].HeaderCell.Value = modul.ModulName;
                i++;
            }
        }
        private void GetGroupsFunction(){
            mainLabSolver.createGroups();
            WaitForThread.Set();
        }
        private void GetNewGroupsFunction()
        {
            mainLabSolver.GetNewGroups();
            WaitForThread.Set();
        }

        

        private static AutoResetEvent WaitForThread=new AutoResetEvent(false);
    }
}
