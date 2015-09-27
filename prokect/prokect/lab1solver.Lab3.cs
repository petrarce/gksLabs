
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;


namespace ConsoleApplication1
{
    
    public class GroupToDeleteInfo
    {
        private Int16 group;
        private  List<Int16> elements;

        public Int16 Group { get { return group; } set { group = value; } }
        public List<Int16> Elements { get { return elements; } set { elements = value; } }
        public GroupToDeleteInfo()
        {
            elements = new List<Int16>();
        }
    }

    public partial class Group
    {
        public Group BackupGroup;
        public List<GroupToDeleteInfo> ToDeleteInfo;

        public Group()
        {
            elements = new List<Int16>();
            operations = new List<String>();
            ToDeleteInfo = new List<GroupToDeleteInfo>();
        }
    }

    partial class lab1Solver
    {
    
        #region getNewGroups
        //GetNewElements
        public void GetNewGroups() {
            FindAllOperationsInAllGroups();
            GetNewGroupsLocal();
        }

        private void FindAllOperationsInAllGroups(){
            for(Int16 i=0;i<groups.Count;i++){
                FindOperationsInGroup(i);
            }
        }
            #region FindAllOperationsInAllGroupsWithRepeated Functions
             //FindAllOperationsInGroup reloads
            private void FindOperationsInGroup(Int16 i)
            {
                groups[i].Operations.Clear() ;
                FindAllOperationsInGroup(i);
                groups[i].Operations.Sort();
                DeleteAllRepeatedOperations(i);
            }
            private void FindAllOperationsInGroup(Int16 i)
            {
                for(Int16 j=0;j<groups[i].Elements.Count;j++) {
                    for (Int16 k = 0; k < operList[groups[i].Elements[j]].Length; k++)
                        groups[i].Operations.Add(operList[groups[i].Elements[j]][k]);
                }

            }
            //
            //DeleteAllRepeatedOperations is anderstandable
            //have 2 reloads (first for FindAllOperationsInGroup;second for using with List<string> T)
            private void DeleteAllRepeatedOperations(Int16 i)
            {
                List<String> tmprOperationListWithRepeats = groups[i].Operations;
                DeleteAllRepeatedOperations(ref tmprOperationListWithRepeats);
                groups[i].Operations = tmprOperationListWithRepeats;
            }
            private void DeleteAllRepeatedOperations(ref List<String> OperationsWithRepeats){
                for (int j = 0; j < OperationsWithRepeats.Count - 1; j++)
                {
                    if (OperationsWithRepeats[j] == OperationsWithRepeats[j + 1])
                    {
                        OperationsWithRepeats.Remove(OperationsWithRepeats[j + 1]);
                        j--;
                    }
                }
            }
            #endregion 


        private void GetNewGroupsLocal() { 
            for(Int16 i=0;i<groups.Count;i++){
                SortGroups(i);
                RefreshGroup(i);
            }
        }
            #region GetNewGroupsLocal
            //
            //bubble sorting of groups by uniaue elements
            //
            private void SortGroups(Int16 group) {
            Group tempWar=new Group();
            for (Int16 i = group; i < groups.Count-1; i++) {
                for (Int16 j = (Int16)(i+1); j < groups.Count; j++) {
                    if (groups[i].Operations.Count < groups[j].Operations.Count)
                    {
                        tempWar = groups[i];
                        groups[i] = groups[j];
                        groups[j] = tempWar;
                    }
                }
            }
        }
            private void RefreshGroup(Int16 i) {
                Int16 groupsToCompare=0, MaxGroupNumber;
                for (Int16 j = (Int16)(i + 1); j < groups.Count; j++) {
                    for (Int16 k = i; k <= i + groupsToCompare; k++) {
                        if (CompareGroups(k, j))
                        {
                            if (groups[k].BackupGroup == null) {
                                groups[k].BackupGroup =groups[k];
                            }
                            groups[k + 1].BackupGroup = groups[k + 1];
                            groupsToCompare++;
                            continue;
                        }
                    }
                    MaxGroupNumber = FinMaxGroupe(j);
                    DeleteOperationsAndElementsFromGroup(MaxGroupNumber);
                    DeleteGroups(MaxGroupNumber);
                }
            }
                #region RefreshGroup functions
                     #region CompareGroups functions
                     private Boolean CompareGroups(Int16 k, Int16 j)
                        {
                            Int16 switcher=CompareGroups(groups[k].Operations,groups[j].Operations);
                            switch (switcher) { 
                                case 0:
                                    return true;
                                case 1:
                                    GetElementsAll(k, j);
                                    return false;
                                case 2:
                                    return false;
                                case 3:
                                    GetElementsPartialy(k, j);
                                    return false;
                            }
                            return false;
                            }
                    //function checks all operations in group 
                    //and compare this groups by operations
                    //returns 0 if amount of operations in first and secound groups are equal and operations are too equaly
                    //returms 1 if amount equaly and operations are different
                    //returns 2 if second group are IN first group
                    //returns 3 if secound group are NOT IN first group
                    //returns 4 if operations are partialy compare 
                    //function getKElem calculate amount of operations if feirst and second groups without repeats
                     private Int16   CompareGroups(List<String> first, List<String> second)
                        { 
                                List<List<String>> tempVar=new List<List<string>>();
                                Int16 tempKElem;
                                tempVar.Add(first);
                                tempVar.Add(second);
                                tempKElem = getKElem(tempVar);
                                if (first.Count == second.Count && tempKElem != first.Count)
                                {
                                        return 0;//equaly but ! fully compare
                                }
                                else if(tempKElem==first.Count){
                                        return 1;//fulycompare adn not equaly (or equaly)
                                }
                                else if (tempKElem == first.Count+second.Count) {
                                    return 2;//!compare
                                }
                                return 3;//partialy compare
                            }
                        #region GetElementsPartialy functions
                        private void GetElementsAll(Int16 k, Int16 j)
                        {
                            //IEnumerator<Int16> tempEnumerator = groups[j].Elements.GetEnumerator();
                            //while (tempEnumerator.MoveNext()){
                            for(int i=0;i<groups[j].Elements.Count-1;i++){
                                groups[k].Elements.Add(groups[j].Elements[i]);
                            }
                            groups[k].ToDeleteInfo.Add(new GroupToDeleteInfo());
                            groups[k].ToDeleteInfo[groups[k].ToDeleteInfo.Count - 1].Group = j;
                            groups[k].ToDeleteInfo[groups[k].ToDeleteInfo.Count - 1].Elements = null;
                        }
                        private void GetElementsPartialy(Int16 k, Int16 j)
                        {
                            IEnumerator<GroupToDeleteInfo> tmpr;
                            List<String> tmprOperList=new List<string>();
                            foreach (Int16 element in groups[j].Elements) {
                                tmprOperList.Clear();
                                foreach(String oper in operList[element])
                                    tmprOperList.Add(oper);
                                if (CompareGroups(groups[k].Operations, tmprOperList) == 1) {
                                    groups[k].Elements.Add(element);
                                    groups[k].ToDeleteInfo.Add(new GroupToDeleteInfo());
                                    groups[k].ToDeleteInfo[groups[k].ToDeleteInfo.Count - 1].Group = j;
                                    groups[k].ToDeleteInfo[groups[k].ToDeleteInfo.Count - 1].Elements.Add(element);

                                }
                            }  
                        }
                        private void GetOperFromElem(Int16 k, List<String> Operations) {
                            foreach (String operation in Operations) {
                                if (!groups[k].Operations.Contains(operation)) {
                                    groups[k].Operations.Add(operation);
                                }
                            }
                        }
                        #endregion


                    #endregion
            // find max by using index
            private Int16 FinMaxGroupe(Int16 j) {
                Int16 i=0;
                try
                {
                    while (groups[j].BackupGroup != null)
                    {
                        if (groups[i].BackupGroup.Elements.Count < groups[j].BackupGroup.Elements.Count)
                        {
                            groups[i] = groups[i].BackupGroup;
                            groups[i].BackupGroup = null;
                            i = j;
                        }
                        j++;
                    }
                }
                catch (ArgumentOutOfRangeException) { }
                    ComparerEqualyGroups(i, (Int16)(j - 1));
                    return i;                 

            }
            //compare all equaly groups 
                private void ComparerEqualyGroups(Int16 MaxGroup,Int16 j){
                while (j != 0) {
                    CompareGroups(MaxGroup,(Int16)(j));
                    j--;
                }
                groups[MaxGroup].BackupGroup = null;
            }
            private void DeleteGroups(Int16 MaxGroup) {
                GroupToDeleteInfo tempEnumerator = new GroupToDeleteInfo();
                for(Int16 i=(Int16)(groups[MaxGroup].ToDeleteInfo.Count-1);i==0;i++){
                    if (groups[MaxGroup].ToDeleteInfo[i].Elements == null) {
                        groups.RemoveAt(groups[MaxGroup].ToDeleteInfo[i].Group);
                    }
                }    
                        
            }
            private void DeleteOperationsAndElementsFromGroup(Int16 MaxGroup) {
                IEnumerator<GroupToDeleteInfo> tempToDeleteInfo=groups[MaxGroup].ToDeleteInfo.GetEnumerator();
                while(tempToDeleteInfo.MoveNext()) {
                    if (tempToDeleteInfo.Current.Elements == null)
                        continue;
                    IEnumerator<Int16> tempElements = tempToDeleteInfo.Current.Elements.GetEnumerator();
                    while (tempElements.MoveNext())
                    {
                            groups[tempToDeleteInfo.Current.Group - 1].Elements.Remove(tempElements.Current);
                    }
                    FindOperationsInGroup(tempToDeleteInfo.Current.Group);
                }
            }
                    #endregion

            #endregion

        #endregion

    }
}
