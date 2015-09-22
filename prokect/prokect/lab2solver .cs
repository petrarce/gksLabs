using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class lab2solver:lab1Solver
    {
        private Int16 groupCount;
        public Int16 GroupCount { get { return groupCount; } }
        private List<List<Int16>> groups;
        public List<List<Int16>> Groups { get { return groups; } }
        
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
        private void openGroup ( Int16 i, Int16 j, ref List<Int16> reserved )
        {//open new group
            Groups.Add( new List<Int16>( ) );
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
            Groups[groupCount].Add( value );//add value intp group(!(groupCount) while becomes )
            reserved.Add( value );// add value into reserved list (not to take it again)
        }
        public  void createGropus ( )
        {
  //          Int16 groupElIndex=0;
            Int16 e;
            List<Int16> reserved = new List<Int16>( );
            for (Int16 k = 9; k >= 0; k--)
            {
                for (Int16 i = 1; i < operList.Count; i++)
                {
                    for (Int16 j = 0; j < i; j++)
                    {
                        if (elementFound( k, i, j, reserved ))
                        {
                            openGroup( i, j, ref reserved );
                            for (int l = 0; l < Groups[Groups.Count - 1].Count;l++ )
                            {
                                e = Groups[Groups.Count - 1][l];
                                searchInCol(k, e, ref reserved);
                                searchInRow(k, e, ref reserved);
                            }

                            groupCount += 1;
                            if (reserved.Count == Matrix.Length)
                                return;
                        }
                    }
                }
            }
            if (reserved.Count == Matrix.Length - 1) {//GroupCount-1 while index of n element ind=n-1
                Groups[GroupCount-1].Add( getLastElem(reserved) );    
            }
        }
        public void outGroups(){
            for (int i = 0; i < Groups.Count; i++)
            {
                for(int j=0;j<Groups[i].Count;j++)
                    Console.Write (Groups[i][j].ToString()+' ');
                Console.WriteLine( );
            }
        }
        public new void initLabSolver ( ) {
            groups = new List<List<short>>( );
            base.initLabSolver( );
            createGropus( );
        }
        private Int16 getLastElem (List<Int16> reserved )/*get last element if such was not found*/ 
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
    }
}
