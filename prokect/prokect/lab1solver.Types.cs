using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.OverlapRemoval;
using GraphX.PCL.Logic.Models;
using GraphX.Controls;
using QuickGraph;
using WindowsFormsProject;

namespace ConsoleApplication1
{
        public class OperationsClass:Object{
            protected List<String> operations;
            public List<String> Operations { get { return operations; } set { operations = value; } }
        }

        public sealed class Group:OperationsClass
        {

            private List<Int16> elements;
            private List<Modul> moduls;
            private Matrix connectionMatrix;

            public Group BackupGroup;
            public List<GroupToDeleteInfo> ToDeleteInfo;
            public List<Int16> Elements { get { return elements; } set { elements = value; } }
            public List<Modul> Moduls { get { return moduls; } set { moduls = value; } }
            public Matrix ConnectionMatrix { get { return connectionMatrix; } set { connectionMatrix = value; } }

            public Group()
            {
                elements = new List<Int16>();
                operations = new List<String>();
                ToDeleteInfo = new List<GroupToDeleteInfo>();
                connectionMatrix = new Matrix();
            }
        }

        public sealed class Modul : OperationsClass {
            private String modulName;

            public String ModulName 
            {
                get { return modulName; }
                set { modulName = value; }
            }

            public Modul() 
            {
                this.operations = new List<string>();
            }
            public Modul(String Modulname,String[] ModulOperations) {
                this.operations = new List<String>();
                this.modulName = Modulname;
                foreach (String moduloperation in ModulOperations) {
                    operations.Add(moduloperation);
                }
            }
        }   
        
        public class GroupToDeleteInfo : Object
        {
            private Int16 group;//asdasd
            private List<Int16> elements;


            public Int16 Group { get { return group; } set { group = value; } }
            public List<Int16> Elements { get { return elements; } set { elements = value; } }

            public GroupToDeleteInfo()
            {
                elements = new List<Int16>();
            }
        }

        public class Matrix:Dictionary<String,Dictionary<String,Boolean>>
        {
            Dictionary<String,Int16> rowWeight;
            Dictionary<String, Int16> columnWeight;

            public Dictionary<String, Int16> RowWeight { get { return rowWeight; } set { rowWeight = value; } }
            public Dictionary<String, Int16> ColumnWeight { get { return columnWeight; } set { columnWeight = value; } }

            public void FindRowWeight() {
                rowWeight = new Dictionary<string, short>();
                foreach(String Row in this.Keys) {
                    rowWeight.Add(Row, 0);
                    foreach(String Col in this[Row].Keys) {
                        if(this[Row][Col] == true) {
                            rowWeight[Row] += 1;
                        }
                    }
                }
            }
            public void FindColWeight()
            {
                columnWeight = new Dictionary<string, short>();
                foreach(String Col in this.Keys)
                {
                    columnWeight.Add(Col, 0);
                    foreach(String Row in this[Col].Keys)
                    {
                        if(this[Row][Col] == true)
                        {
                            columnWeight[Col] += 1;
                        }
                    }
                }
            }


            public Matrix() : base() {
                rowWeight = new Dictionary<string, short>();
                columnWeight = new Dictionary<string, short>();
            }


        }
        public class GraphExample : BidirectionalGraph<DataVertex, DataEdge> { }

        public class GraphAreaExample : GraphArea<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>> { }


        
}
