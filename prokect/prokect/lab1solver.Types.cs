using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Modul() {
                this.operations = new List<string>();
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
            public Matrix():base() { }                
        }
        
}
