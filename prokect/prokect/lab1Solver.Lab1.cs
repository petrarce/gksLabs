using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    public partial class lab1Solver
    {
        private Int16 kElem;
        private Int16[][] matrix;
        private List<String[]> operList;
        public Int16 KElem { get { return kElem; } }
        public Int16[][] Matrix { get { return matrix; } }
        public List<String[]> OperList { 
            get { 
                return operList; 
            } 
            set { 
                operList = value;
                getKElem();
                getMatrix();
            }
        }
                
                private Int16 getKElem(List<List<String>> OurString){
                    List<String> tempStr = new List<String>();//, tempStrII=new List<string>();
                    foreach (List<String> row in OurString)
                    {
                        foreach (String cell in row)
                        {
                            tempStr.Add(cell);
                        }
                    }
                    tempStr.Sort();
                    DeleteAllRepeatedOperations(ref tempStr);
                    return (Int16)(tempStr.Count);
                }
                private Int16 getKElem(List<String[]> OurString){
                    List<List<String>> tempConcrtedFromDtringToLidst = new List<List<String>>();
                    for (Int16 i = 0; i < OurString.Count; i++) {
                        tempConcrtedFromDtringToLidst.Add(new List<String>());
                        foreach(String str in OurString[i]) { 
                            tempConcrtedFromDtringToLidst[i].Add(str);
                        }
                    }
                        return getKElem(tempConcrtedFromDtringToLidst);
                }

                internal void getKElem(){
                    kElem=getKElem(operList);
                }
                private void getMatrix(){
                    List<String> tempStrList=new List<string>();
                    List<String> tempVar=new List<string>();
                    resizeMatrix( operList.Count );
                    for (int i = 0; i < operList.Count - 1; i++)
                    {
                        for (int j = i + 1; j < operList.Count; j++)
                        {
                            tempVar.AddRange(operList[i]);
                            tempVar.AddRange(operList[j]);
                            for (int k = 0; k < operList[i].Length; k++)
                            {
                                for (int l = operList[i].Length; l <= tempVar.Count-1; l++)
                                {
                                    if (tempVar[k] == tempVar[l])
                                    {
                                        tempStrList.Add(tempVar[l]);
                                    }
                                }
                            }
                            foreach (String tmp in tempStrList)
                                tempVar.RemoveAll(item => item == tmp);
                            matrix[j][i] = matrix[i][j]= (Int16)(KElem-tempVar.Count);
                            tempVar.Clear();
                            tempStrList.Clear();
                        }
                    }
                }
                //
                //Creates Matrix Of with sizeXsize elements
                //
                private void resizeMatrix (int size ) {
                    Array.Resize<Int16[]>( ref matrix, size );
                    for (int i = 0; i < size; i++)
                    {
                        Array.Resize<Int16>( ref matrix[i], size );
                    }
                }
                public  void outMatrix ( ) {
                    for (int i = 0; i < operList.Count; i++)
                        {
                        Console.Write( "\n" );
                        for (int j = 0; j < operList.Count; j++) 
                            {
                            Console.Write(matrix[i][j] +" ");
                            }
                        }
                    }

                public lab1Solver()
                {
                    groups = new List<Group>();
                    operList = new List<string[]>();
                    
                }

    }
}
