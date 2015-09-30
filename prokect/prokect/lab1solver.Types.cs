using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

        public class Group:Object
        {
            public Group BackupGroup;
            public List<GroupToDeleteInfo> ToDeleteInfo;
            private List<Int16> elements;
            private List<String> operations;

            public List<Int16> Elements { get { return elements; } set { elements = value; } }
            public List<String> Operations { get { return operations; } set { operations = value; } }

            public Group()
            {
                elements = new List<Int16>();
                operations = new List<String>();
                ToDeleteInfo = new List<GroupToDeleteInfo>();
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
}
