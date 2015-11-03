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
            {
                GetNewModuls(i);
            }
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
                        Groups[GroupNumber].ConnectionMatrix = new Matrix();
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
            GetComunicationMatrix(GroupNumber);
            while(GetNewModul(GroupNumber)) { }
        }
        private bool GetNewModul(Int16 GroupNumber)
            {
                FindWeight(GroupNumber);
                if(CheckChain(GroupNumber))
                    return true;
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
                    PotentialModul.Add(FindPrev(Current, GroupNumber));
                    while (RowAndColWeight(1, 1, GroupNumber, Current))
                    {
                        PotentialModul.Add(Current);
                        Current = FindNext(Current, GroupNumber);
                        tempChecked.Add(Current);
                        
                    }
                    PotentialModul.Add(Current);
                    if(RowAndColWeight(1, 0, GroupNumber, Current))//!!!
                    {
                        continue;
                    }
                    if(Groups[GroupNumber].ConnectionMatrix[PotentialModul[0]][Current] == true ||
                        Groups[GroupNumber].ConnectionMatrix[Current][PotentialModul[0]] == true)
                    {
                        CreateModul(GroupNumber,PotentialModul);
                        CleanConnectionMatrix(GroupNumber, PotentialModul);
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
                private String FindPrev(String Current, Int16 GroupNumber)
                {
                    foreach(var Element in Groups[GroupNumber].ConnectionMatrix)
                    {
                        if(Element.Value[Current] == true)
                            return Element.Key;
                    }
                    return "";
                }

                private void CreateModul(Int16 GroupNumber,List<String> PotentialModul)
                {
                    foreach(var Element in PotentialModul) { 
                        if(Element==PotentialModul[0])
                            continue;
                        AddRowsAndColumns(PotentialModul, Element, GroupNumber);
                    }
                    DeleteModuls(PotentialModul, GroupNumber);
                }
                private void AddRowsAndColumns(List<String> PotentialModul, String DeletingModul, Int16 GroupNumber)
                    {
                        foreach (var Element in PotentialModul)
                        {
                            if (Element == PotentialModul[0])
                                continue;
                            if (Groups[GroupNumber].ConnectionMatrix[DeletingModul][Element] == true)
                                Groups[GroupNumber].ConnectionMatrix[PotentialModul[0]][Element] = true;
                            if (Groups[GroupNumber].ConnectionMatrix[Element][DeletingModul] == true)
                                Groups[GroupNumber].ConnectionMatrix[Element][PotentialModul[0]] = true;
                        }
                        Groups[GroupNumber].ConnectionMatrix[PotentialModul[0]][PotentialModul[0]] = false;
                    }
                    private void DeleteModuls(List<String> PotentialModul,Int16 GroupNumber){
                        Modul modul= new Modul(), firstModul=new Modul();
                        foreach (var mod in Groups[GroupNumber].Moduls) {
                            if (mod.ModulName == PotentialModul[0])
                                firstModul = mod;
                        }
                        foreach (var Element in PotentialModul)
                        {
                            if (Element == PotentialModul[0])
                                continue;
                            foreach (var mod in Groups[GroupNumber].Moduls) {
                                if (mod.ModulName == Element)
                                    modul = mod;
                            }
                            Groups[GroupNumber].Moduls[Groups[GroupNumber].Moduls.FindIndex(x => x.Equals(firstModul))].Operations.AddRange(
                                    Groups[GroupNumber].Moduls[Groups[GroupNumber].Moduls.FindIndex(x => x.Equals(modul))].Operations);
                            Groups[GroupNumber].Moduls.Remove(modul);
                        }                   
                    }
                    private void CleanConnectionMatrix(Int16 GroupNumber, List<String> PotentialModul)
                    {
                        foreach (String Element in PotentialModul) 
                        {
                            if (Element == PotentialModul[0])
                                continue;
                            foreach (String Row in Groups[GroupNumber].ConnectionMatrix.Keys)
                            {
                                Groups[GroupNumber].ConnectionMatrix[Row].Remove(Element);
                            }
                            Groups[GroupNumber].ConnectionMatrix.Remove(Element);
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
