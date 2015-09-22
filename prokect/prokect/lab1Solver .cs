using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class lab1Solver
    {
        private Int16 kElem;
        private Int16[][] matrix;
        public List<String[]> operList;
        public Int16 KElem { get { return kElem; } }
        public Int16[][] Matrix { get { return matrix; } }
        public List<String[]> OperList { 
            get { 
                return operList; 
            } 
            set { 
                operList = value;
                initLabSolver();
            }
        }
        private delegate void CreateNewThread();
        private void getKElem(){
            List<String> tempStr=new List<string>(), tempStrII=new List<string>();
            bool CountPlus;
            kElem=0;
            foreach(String[] fsString in operList){
                foreach(String ssString in fsString){
                    try{
                            CountPlus = true;
                            foreach (String fixStr in tempStr)
                            {
                                if (fixStr == ssString)
                                {
                                    CountPlus = false;
                                    break;
                                }
                            }
                            if (CountPlus) {
                                tempStr.Add(ssString);
                                kElem += 1;
                            }
                        
                    
                    }
                    catch(NullReferenceException){
                          tempStr.Add(ssString);
                          kElem += 1;
                    }
                }
            }
                
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
                    matrix[j][i] = matrix[i][j]= (Int16)tempVar.Count;
                    tempVar.Clear();
                    tempStrList.Clear();
                }
            }
        }
        private void resizeMatrix (int size ) {
            Array.Resize<Int16[]>( ref matrix, size );
            for (int i = 0; i < size; i++)
            {
            Array.Resize<Int16>( ref matrix[i], size );
            }
        }
        public void outMatrix ( ) {
        for (int i = 0; i < operList.Count; i++)
            {
            Console.Write( "\n" );
            for (int j = 0; j < operList.Count; j++) 
                {
                Console.Write(matrix[i][j] +" ");
                }
            }
        }


        public void initLabSolver() {
            getKElem();
            getMatrix();
        }
    }
}
