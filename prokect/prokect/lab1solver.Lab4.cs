using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    partial class lab1Solver
    {
        //private List<Matrix> groupsModuls;

        /*public List<Matrix> GroupsModuls
        {
            get { return groupsModuls; }
            set { groupsModuls = value; }
        }*/

        public void GetNewModuls() {
            for (Int16 GroupNumber = 0; GroupNumber < Groups.Count; GroupNumber++)
            {
                GetNewModuls(GroupNumber);
            }
        }
        private void GetNewModuls(Int16 GrupNumber) {
            GetComunicationMatrix(GrupNumber);
            GetModuls(GrupNumber);
        }
            private void GetComunicationMatrix(Int16 GrupNumber)
            {
                CreateMatrix(GrupNumber);
                InitializeMatrix(GrupNumber);
            }
                private void CreateMatrix(Int16 GrupNumber)
                {
                    foreach (String operationRow in Groups[GrupNumber].Operations)
                    {
                        Groups[GrupNumber].ConnectionMatrix.Add(operationRow, new Dictionary<String, Boolean>());
                        foreach (String operationColumn in Groups[GrupNumber].Operations)
                        {
                            Groups[GrupNumber].ConnectionMatrix[operationRow].Add(operationColumn, false);
                        }
                    }
                }
                private void InitializeMatrix(Int16 GrupNumber)
                {
                    foreach (Int16 Element in Groups[GrupNumber].Elements)
                    {
                        InitializeMatrixForElement(Element,GrupNumber);
                    }
                }
                private void InitializeMatrixForElement(Int16 Currentlement, Int16 GrupNumber)
                {
                        String tempElementOperation = null;
                        foreach (String operation in operList[Currentlement]) {
                            if (tempElementOperation != null) {
                                Groups[GrupNumber].ConnectionMatrix[tempElementOperation][operation] = true;
                            }
                            tempElementOperation = operation;
                        }
                    }
            private void GetModuls(Int16 GroupNumber) { }
    }
}
