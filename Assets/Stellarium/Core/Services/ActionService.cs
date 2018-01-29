using UnityEngine;
using System.Collections.Generic;

namespace Stellarium.Services {

    public class ActionService : Service {

        public static event Got<ActionGroupList> OnGotList;
        public static event Posted<bool> OnDidAction;

        public override string Identifier
        {
            get
            {
                return "Stellarium.ActionService";
            }
        }

        public override string Path
        {
            get
            {
                return "stelaction";
            }
        }

        public ActionService(StellariumServer stellarium) : base(stellarium) {
            Stellarium = stellarium;
        }

        public void List() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "list", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                ActionGroupList actionGroupList = new ActionGroupList();
                actionGroupList.actionGroups = new ActionGroup();
                foreach(string actionGroupName in json.keys) {
                    int numberOfActions = json.GetField(actionGroupName).Count;
                    ActionList actionList = new ActionList();
                    actionList.actions = new Action[numberOfActions];
                    for(int i=0;i<numberOfActions;i++){
                        actionList.actions[i] = JsonUtility.FromJson<Action>(json.GetField(actionGroupName)[i].ToString());
                    }
                    actionGroupList.actionGroups.Add(actionGroupName, actionList);
                }
                if(OnGotList != null) {
                    OnGotList(actionGroupList);
                }
            });
        }

        public void Do(string id) { //TODO: Find out what constitutes a valid parameter
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            Stellarium.POST(Path, "do", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                bool b = false;
                if(!bool.TryParse(result, out b)) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, result)); return;
                }
                if(OnDidAction != null) {
                    OnDidAction(b);
                }
            });
        }

    }
}