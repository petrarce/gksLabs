using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ConsoleApplication1
{
    public partial class Group {
        private List<Int16> elements;
        private List<String> operations;

        public List<Int16> Elements{ get{return elements;} set {elements=value;}}
        public List<String> Operations{get {return operations;} set {operations=value;}}

    }

    public partial class lab1Solver
    {
        private List<Group> groups;
        public List<Group> Groups { get { return groups; } }
        
        private bool elementFound ( Int16 val, Int16 i, Int16 j, List<Int16> reserved )
        {
            if (Matrix[i][j] == val)
            {
                foreach (Int16 elem in reserved)//check if element already used
                {
                    if (i == elem || j == elem)
                        return false;
                }
                return true;
            }
            return false;
        }
        //open new group
        private void openGroup ( Int16 i, Int16 j, ref List<Int16> reserved )
        {
            Groups.Add( new Group() );
            addElInGroup( i, ref reserved );
            addElInGroup( j, ref reserved );
        }
        private void searchRow ( Int16 Val, Int16 i, Int16 j, ref List<Int16> reserved )
        {
            bool noElemInRes = true; ;
            for (j += 1; j < Matrix.Length; j++)
            {
                if (Matrix[i][j] == Val)
                {
                    noElemInRes = true;
                    foreach (Int16 elem in reserved)
                    {
                        if (j == elem)
                        {
                            noElemInRes = false;
                            break;
                        }
                        if (noElemInRes) { 
                            addElInGroup( j, ref reserved );
                            break;
                        }
                    }
                }
            }
        }
        
        private void searchInCol ( Int16 Val, Int16 Col, ref List<Int16> reserved ) {
            for (int i = Col+1; i < Matrix.Length; i++) {
                if (Matrix[i][Col] == Val)
                    if (!checkElemInReserved((Int16)i, ref reserved))
                    {
                        addElInGroup((Int16)i, ref reserved);
                    }
                
                }
        }
        private void searchInRow ( Int16 Val, Int16 Row, ref List<Int16> reserved ) {
            for (Int16 j = 0; j < Row-1; j++)
            {
                if (Matrix[Row][j] == Val)
                    if (!checkElemInReserved(j, ref reserved))
                    {
                        addElInGroup(j, ref reserved);
                    }
                
                }
        }
        private bool checkElemInReserved ( Int16 Val, ref List<Int16> reserved )
        {
            foreach (Int16 elem in reserved) {
                if (elem == Val) return true;
            }
            return false;
        }
        private void addElInGroup ( Int16 value, ref List<Int16> reserved )
        {
            Groups[Groups.Count-1].Elements.Add(value);//add value intp group(!(groupCount) while becomes )
            reserved.Add( value );// add value into reserved list (not to take it again)
        }
        public  void createGropus ( )
        {
            Int16 e;
            List<Int16> reserved = new List<Int16>( );
            for (Int16 k = KElem; k >= 0; k--)
            {
                for (Int16 i = 1; i < operList.Count; i++)
                {
                    for (Int16 j = 0; j < i; j++)
                    {
                        if (elementFound( k, i, j, reserved ))
                        {
                            openGroup( i, j, ref reserved );
                            for (int l = 0; l < Groups[Groups.Count - 1].Elements.Count;l++ )
                            {
                                e = Groups[Groups.Count - 1].Elements[l];
                                searchInCol(k, e, ref reserved);
                                searchInRow(k, e, ref reserved);
                            }

                            if (reserved.Count == Matrix.Length)
                                return;
                        }
                    }
                }
            }
            if (reserved.Count == Matrix.Length - 1) {//GroupCount-1 while index of n element ind=n-1
                Groups.Add(new Group());
                Groups[Groups.Count - 1].Elements.Add(getLastElem(reserved));    
            }
        }
        public  void outGroups(){
            for (int i = 0; i < Groups.Count; i++)
            {
                for(int j=0;j<Groups[i].Elements.Count;j++)
                    Console.Write (Groups[i].Elements[j].ToString()+' ');
                Console.WriteLine( );
            }
        }
        /*get last element if such was not found*/
        private Int16 getLastElem (List<Int16> reserved ) 
        {
            Int16 i,j;
            for ( i = 0; i < Matrix.Length-1; i++)
            {
                j = 0;
                foreach (Int16 e in reserved)
                {
                    if (i == e) {
                        j++;
                    }
                }
                if (j == 0) return i;
            }
            
            return i;
        }


        public lab1Solver()
        {
            groups = new List<Group>();
            operList = new List<string[]>();
        }
    }
}
