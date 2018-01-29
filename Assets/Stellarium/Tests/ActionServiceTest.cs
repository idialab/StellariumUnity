using UnityEngine;
using Stellarium;
using Stellarium.Services;
using System.Collections.Generic;

public class ActionServiceTest : MonoBehaviour {

    private void OnEnable() {
        ActionService.OnDidAction += ActionService_OnDidAction;
        ActionService.OnGotList += ActionService_OnGotList;
    }

    private void ActionService_OnGotList(ActionGroupList result) {
        foreach(KeyValuePair<string,ActionList> actionList in result.actionGroups) {
            StellariumServer.Instance.ActionService.Do(actionList.Value.actions[0].text); //Invalid id
            return;
        }
    }

    private void ActionService_OnDidAction(bool result) {
        Debug.Log(result);
    }

    void Start() {
        StellariumServer.Instance.ActionService.List();
    }


    private void OnDisable() {
        ActionService.OnDidAction -= ActionService_OnDidAction;
        ActionService.OnGotList -= ActionService_OnGotList;
    }
}
