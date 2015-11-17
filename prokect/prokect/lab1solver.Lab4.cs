using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApplication1
{
    internal static class Variables 
    {
        static bool before = true;
        static bool after = false;
        public static bool BEFORE { get { return before; } }
        public static bool AFTER { get { return after; } }
    }
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
        public bool GetNewModul1(Int16 GroupNumber)
        {
            return GetNewModul(GroupNumber);
        }
        private bool GetNewModul(Int16 GroupNumber)
            {
                FindWeight(GroupNumber);
                if (CheckMutual(GroupNumber))
                    return true;
                if(CheckChain(GroupNumber))
                    return true;
                if (CheckRoundChain(GroupNumber))
                    return true;
                return false;
            }
            //Checking for chain existing
            private bool CheckChain(Int16 GroupNumber)
            {
                String Current,tempStr;
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
                    if (PotentialModulOperationsCount(GroupNumber, PotentialModul) > 5)
                        return false;
                    tempStr = Current;
                    while (tempStr!= "")//||RowAndColWeight(1, 1, GroupNumber, Current))
                    {
                        Current = tempStr;
                        PotentialModul.Add(Current);
                        if (PotentialModulOperationsCount(GroupNumber, PotentialModul) > 5)
                            return false;
                        tempStr = FindNext(Current, GroupNumber, tempChecked);
                        tempChecked.Add(Current);
                        
                    }
                    PotentialModul.Add(Current);
                    if (PotentialModulOperationsCount(GroupNumber, PotentialModul) > 5)
                        return false;
                    if(RowAndColWeight(1, 0, GroupNumber, Current))//!!!
                    {
                        continue;
                    }
                    if(Groups[GroupNumber].ConnectionMatrix[PotentialModul[0]][Current] == true)
                    {
                        CreateModul(GroupNumber,PotentialModul);
                        CleanConnectionMatrix(GroupNumber, PotentialModul);
                        return true;
                    }

                }
                return false;
            }
                private String FindNext(String Current,Int16 GroupNumber,List<String> Checked) 
                {
                    Modul tempModul=new Modul();
                    foreach(var Element in Groups[GroupNumber].ConnectionMatrix[Current]) {
                        tempModul.ModulName=Element.Key;
                        if (Element.Value == true && !ModulInChecked(Checked,tempModul))
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
            private bool CheckMutual(Int16 GroupNumber) {
                String Current;
                List<String> PotentialModul=new List<String>();
                List<String> tempChecked = new List<String>();//Names of checked moduls
                foreach (Modul modul in Groups[GroupNumber].Moduls)
                {
                    if (ModulInChecked(tempChecked, modul))
                        continue;
                    PotentialModul.Clear();
                    Current = modul.ModulName;
                    tempChecked.Add(Current);
                    PotentialModul.Add(Current);
                    if (PotentialModulOperationsCount(GroupNumber, PotentialModul) > 5)
                        return false;
                    foreach (var Column in Groups[GroupNumber].ConnectionMatrix[Current])
                    {
                        if(Groups[GroupNumber].ConnectionMatrix[Current][Column.Key]==true&&
                            Groups[GroupNumber].ConnectionMatrix[Column.Key][Current] == true)
                        {
                            PotentialModul.Add(Column.Key);
                            if (PotentialModulOperationsCount(GroupNumber, PotentialModul) > 5)
                                return false;
                            CreateModul(GroupNumber, PotentialModul);
                            CleanConnectionMatrix(GroupNumber, PotentialModul);
                            return true;
                        }
                    }
                }
                return false;
            }
            private bool CheckRoundChain(Int16 GroupNumber)
            {
                List<String> tempChecked = new List<string>();
                List<String> PotentialModul=new List<string>();
                Modul tempModul = new Modul();
                String tempStr;
                foreach (var Operation in groups[GroupNumber].ConnectionMatrix.Keys)
                {
                    tempModul.ModulName = Operation;
                    if (ModulInChecked(tempChecked, tempModul)||(tempStr=FindNext(Operation, GroupNumber,tempChecked))=="")
                        continue;
                    if (FindWay(tempStr,ref PotentialModul,ref tempChecked,GroupNumber))
                        return true;
                }
                return false;
            }
                private bool FindWay(String Current, ref List<String> PotentialModul, ref List<String> Checked,Int16 GroupNumber)
                {
                    Modul tempModul=new Modul(){ModulName=Current};
                    String tempStr;
                    Checked.Add(Current);
                    if (ModulInChecked(PotentialModul, tempModul))
                    {
                        RefreshPotentialModul(ref PotentialModul, Current,Variables.BEFORE);
                        CreateModul(GroupNumber, PotentialModul);
                        CleanConnectionMatrix(GroupNumber, PotentialModul);
                        return true;
                    }
                    PotentialModul.Add(Current);
                    if (PotentialModulOperationsCount(GroupNumber, PotentialModul) > 5)
                        return false;
                    while ((tempStr = FindNext(Current, GroupNumber,Checked)) != "")
                    {
                        if (FindWay(tempStr, ref PotentialModul, ref Checked, GroupNumber))
                            return true;
                    }
                    PotentialModul.Remove(Current);
                    return false;
                }
                    private void RefreshPotentialModul(ref List<String> PotentialModul,String ModulName,bool Before)
                    {
                        if (Before)
                            RefreshBefore(ref PotentialModul,ModulName);
                        else 
                            RefreshAfter(ref PotentialModul,ModulName);
                    }
                        private void RefreshBefore(ref List<String> PotentialModul, String ModulName)
                        {
                            foreach(var Value in PotentialModul)
                            {
                                if (Value == ModulName)
                                {
                                    PotentialModul.RemoveRange(0, PotentialModul.FindIndex(x => x == ModulName));
                                    return;

                                }
                            }

                        }
                        private void RefreshAfter(ref List<String> PotentialModul, String ModulName)
                        {
                            foreach (var Value in PotentialModul)
                            {
                                if (Value == ModulName)
                                {
                                    PotentialModul.RemoveRange(PotentialModul.FindIndex(x => x == Value), PotentialModul.Count - PotentialModul.FindIndex(x => x == Value));
                                    return;
                                }
                            }
                        }




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
            private Int16 PotentialModulOperationsCount(Int16 GroupNumber, List<String> PotentialModul)
            {
                Int16 count=0;
                Modul tempModul = new Modul();
                foreach (var str in PotentialModul)
                {
                    foreach (var modul in Groups[GroupNumber].Moduls)
                    {
                        if (modul.ModulName == str)
                        {
                            tempModul = modul;
                            break;
                        }
                    }
                    count += (Int16)tempModul.Operations.Count;
                }
                return count;

            }
    }       
}
