using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;

public class GroupsToDeleteInfo
{
    private Int16 groupToDelete;
    private List<String> operationsToDelete;

    public Int16 GroupToDelete { get { return groupToDelete; } set { groupToDelete = value; } }
    public List<String> OperateionsToDelete { get { return operationsToDelete; } set { operationsToDelete = value; } }
    public GroupsToDeleteInfo()
    {
        operationsToDelete = new List<string>();
    }
}

namespace ConsoleApplication1
{
    partial class lab1Solver
    {
        public GroupsToDeleteInfo ToDeleteInfo;

        #region getNewGroups
        //GetNewElements
        public void GetNewGroups() {
            FindAllElementsInAllGroups();
            GetNewGroupsLocal();
        }

        private void FindAllElementsInAllGroups(){
            for(Int16 i=0;i<Groups.Count;i++){
                FindAllElementsInGroup(i);
                Groups[i].Operations.Sort();
                DeleteAllRepeatedElements(i);
            }
        }   
            #region FindAllElementsInAllGroups Functions
            private void FindAllElementsInGroup(Int16 i)
        {
            for(Int16 j=0;j<Groups[i].Elements.Count;j++) {
                for (Int16 k = 0; k < operList[i].Length;k++)
                    Groups[i].Operations.Add(operList[Groups[i].Elements[j]][k]);
            }
        }
            private void DeleteAllRepeatedElements(Int16 i)
        {
            for (int j = 0; j < Groups[i].Operations.Count-1; j++) {
                if (Groups[i].Operations[j] == Groups[i].Operations[j + 1]) {
                    Groups[i].Operations.Remove(Groups[i].Operations[j + 1]);
                }
            }
        }
            #endregion 


        private void GetNewGroupsLocal() { 
            for(Int16 i=0;i<Groups.Count;i++){
                SortGroups(i);
                RefreshGroup(i);
            }
        }
            #region GetNewGroupsLocal
        //bubble sorting of Groups by uniaue elements
            private void SortGroups(Int16 group) {
            Group tempWar=new Group();
            for (Int16 i = group; i < Groups.Count; i++) {
                for (Int16 j = 0; j < Groups.Count; j++) {
                    if (Groups[i].Elements.Count < Groups[j].Elements.Count) {
                        tempWar = Groups[i];
                        Groups[i] = Groups[j];
                        Groups[j] = tempWar;
                    }
                }
            }
        }
            private void RefreshGroup(Int16 i) {
            Int16 groupsToCompare=0, MaxGroupNumber;
            for (Int16 j = (Int16)(i + 1); j < Groups.Count; j++) {
                for (Int16 k = i; k < i + groupsToCompare; k++) {
                    if (CompareGroups(k, j))
                    {
                        groupsToCompare++;
                        break;
                    }
                }
                MaxGroupNumber = FinMaxGroupe();
                DeleteGroups(MaxGroupNumber);
                DeleteOperationsAndElementsFromGroup(MaxGroupNumber);
            }
            }
                #region RefreshGroup functions
                private Boolean CompareGroups(Int16 k, Int16 j) {
                    List<List<String>> tempVar=new List<List<string>>();
                    Int16 tempKElem;
                    tempVar.Add(Groups[k].Operations);
                    tempVar.Add(Groups[j].Operations);
                    tempKElem = getKElem(tempVar);
                    if(tempKElem==Groups[k].Operations.Count){
                        if (tempKElem == Groups[j].Operations.Count) {
                            return true;
                        }
                        else{
                            GetElementsFull();
                            return false;
                        }
                    }
                    if (tempKElem == Groups[k].Operations.Count+Groups[j].Operations.Count) {
                        return false;
                    }
                    GetElements();
                    return false;
                }
                    #region CompareGroups functions
                    private void GetElementsFull();
                    private void GetElements();
                    #endregion
                private Int16 FinMaxGroupe() { return 0; }
                private void DeleteGroups(Int16 MaxGroup) { }
                private void DeleteOperationsAndElementsFromGroup(Int16 MaxGroup) { }
                
                #endregion

            #endregion

        #endregion

    }
}
