using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    partial class lab1Solver
    {
        public void StartLab4Task()
        {
            InitializeNewElements();
            for(Int16 i = 0; i < Groups.Count; i++)
                GetNewModuls(i);
        }
        internal void InitializeNewElements() 
        {
            for (Int16 GroupNumber = 0; GroupNumber < Groups.Count; GroupNumber++) {
                InitializeNewElement(GroupNumber);
            }    
        }
            private void InitializeNewElement(Int16 GroupNumber)
                {
                    Groups[GroupNumber].Moduls = new List<Modul>();
                    groups[GroupNumber].ConnectionMatrix = new Matrix();
                    InitializeFirstModuls(GroupNumber);
                    GetComunicationMatrix(GroupNumber);

                }
                private void InitializeFirstModuls(Int16 GroupNumber) 
            {
                    foreach (String modulName in Groups[GroupNumber].Operations) {
                        Groups[GroupNumber].Moduls.Add(new Modul(modulName, new []{modulName}));
                    }
            }
                private void GetComunicationMatrix(Int16 GroupNumber)
                {
                    CreateMatrix(GroupNumber);
                    InitializeMatrix(GroupNumber);
                }
                    private void CreateMatrix(Int16 GroupNumber)
                    {
                        foreach (Modul modulNameRow in Groups[GroupNumber].Moduls)
                        {
                            Groups[GroupNumber].ConnectionMatrix.Add(modulNameRow.ModulName, new Dictionary<String, Boolean>());
                            foreach (Modul modulNameColumn in Groups[GroupNumber].Moduls)
                            {
                                Groups[GroupNumber].ConnectionMatrix[modulNameRow.ModulName].Add(modulNameColumn.ModulName, false);
                            }
                        }
                    }
                    private void InitializeMatrix(Int16 GroupNumber)
                    {
                        foreach (Int16 Element in Groups[GroupNumber].Elements)
                        {
                            InitializeMatrixUsingElements(Element, GroupNumber);
                        }
                    }
                    private void InitializeMatrixUsingElements(Int16 Currentlement, Int16 GroupNumber)
                    {
                            String tempElementOperation = null;
                            foreach (String operation in operList[Currentlement]) {
                                if (tempElementOperation != null) {
                                    Groups[GroupNumber].ConnectionMatrix[tempElementOperation][operation] = true;
                                }
                                tempElementOperation = operation;
                            }
                    }
        
        
        
        private void GetNewModuls(Int16 GroupNumber)
        {
            while(GetNewModul(GroupNumber)) { }
        }
        private bool GetNewModul(Int16 GroupNumber)
            {
               // if(FindWeight(GroupNumber))
                //    return true;
                if(CheckChain(GroupNumber))
                    return true;
                //CheckInterrelation( GroupNumber);
                //CheckInOutOnly( GroupNumber);
                return false;
            }
            //Checking for chain existing
            private bool CheckChain(Int16 GroupNumber)
            {
                String Current;
                List<String> PotentialModul=new List<String>();
                List<String> tempChecked = new List<String>();//Names of checked moduls
                foreach(Modul modul in Groups[GroupNumber].Moduls) {
                    if(ModulInChecked(tempChecked, modul))
                        continue;
                    PotentialModul.Clear();
                    tempChecked.Add(modul.ModulName);
                    Current = modul.ModulName;
                    if(!RowAndColWeight(1,1,GroupNumber,Current)) {
                        continue;
                    }
                    do{
                        PotentialModul.Add(Current);
                        Current = FindNext(Current, GroupNumber);
                        tempChecked.Add(modul.ModulName);
                    } while(RowAndColWeight(1,1,GroupNumber,Current));
                    if(!RowAndColWeight(1, 0, GroupNumber, Current))//!!!
                    {
                        continue;
                    }
                    if(Groups[GroupNumber].ConnectionMatrix[PotentialModul[0]][Current] == true ||
                        Groups[GroupNumber].ConnectionMatrix[Current][PotentialModul[0]] == true)
                    {
                        CreateModul(GroupNumber,PotentialModul);
                        return true;
                    }

                }
                return false;
            }
                private String FindNext(String Current,Int16 GroupNumber) 
                {
                    foreach(var Element in Groups[GroupNumber].ConnectionMatrix[Current]) {
                        if(Element.Value == true)
                            return Element.Key;
                    }
                    return "";
                }
                private void CreateModul(Int16 GroupNumber,List<String> PotentialModul)
                {
                    /*Modul tempFirstModul,tempModul;
                    foreach(var modul in Groups[GroupNumber].Moduls )
                        if(modul.ModulName==PotentialModul[0]){
                            tempFirstModul = modul;
                            break;
                        }*/
                    foreach(var Element in PotentialModul) { 
                        if(Element==PotentialModul[0])
                            continue;
                        AddRowsAndColumns(PotentialModul[0], Element, GroupNumber);
                        //AddColumns(PotentialModul[0], Element);
                        DeleteModuls(PotentialModul, GroupNumber);
                    }
                }
                    private void AddRowsAndColumns(String FirstModul, String DeletingModul,Int16 GroupNumber)
                    {
                        foreach(var element in Groups[GroupNumber].Moduls){
                            if(Groups[GroupNumber].ConnectionMatrix[DeletingModul][element.ModulName]==true)
                                Groups[GroupNumber].ConnectionMatrix[FirstModul][element.ModulName] = true;
                            if(Groups[GroupNumber].ConnectionMatrix[element.ModulName][DeletingModul]==true)
                                Groups[GroupNumber].ConnectionMatrix[element.ModulName][FirstModul] = true;
                        }
                        Groups[GroupNumber].ConnectionMatrix[FirstModul][FirstModul] = false;
                    }
                    private void DeleteModuls(List<String> PotentialModul,Int16 GroupNumber){
                        foreach(var element in PotentialModul)
                        {
                            foreach(var row in Groups[GroupNumber].Moduls)
                            {
                                Groups[GroupNumber].ConnectionMatrix[row.ModulName].Remove(element);
                            }
                            Groups[GroupNumber].ConnectionMatrix.Remove(element);
                            foreach(var modul in Groups[GroupNumber].Moduls)
                                if(modul.ModulName==element)
                                    Groups[GroupNumber].Moduls.Remove(modul);
                        }
                    }
            //checks if modul are in checked
            private bool ModulInChecked(List<String> tempChecked,Modul modul)
            {
                foreach(String CheckItemn in tempChecked) {
                    if(modul.ModulName == CheckItemn)
                        return true;

                }
                return false;
            }
            private void FindWeight(Int16 GroupNumber)
            {
                Groups[GroupNumber].ConnectionMatrix.FindColWeight();
                Groups[GroupNumber].ConnectionMatrix.FindRowWeight();
            }
            private bool RowAndColWeight(Int16 value1, Int16 value2,Int16 GroupNumber,String Current) 
            {
                return Groups[GroupNumber].ConnectionMatrix.RowWeight[Current] == value1 && Groups[GroupNumber].ConnectionMatrix.ColumnWeight[Current] == value2; 
            }
    }   
}
