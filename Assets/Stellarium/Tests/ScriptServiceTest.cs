using UnityEngine;
using Stellarium.Services;

public class ScriptServiceTest : MonoBehaviour {

    private void OnEnable() {
        ScriptService.OnGotInfo += ScriptService_OnGotInfo;
        ScriptService.OnGotInfoHTML += ScriptService_OnGotInfoHTML;
        ScriptService.OnGotScriptList += ScriptService_OnGotScriptList;
        ScriptService.OnGotStatus += ScriptService_OnGotStatus;
        ScriptService.OnRanCode += ScriptService_OnRanCode;
        ScriptService.OnStartedScript += ScriptService_OnStartedScript;
        ScriptService.OnStoppedScript += ScriptService_OnStoppedScript;
    }

    private void ScriptService_OnStoppedScript() {
        Debug.Log("Stopped script");
    }

    private void ScriptService_OnStartedScript() {
        Debug.Log("Started script");
    }

    private void ScriptService_OnRanCode() {
        Debug.Log("Ran code");
    }

    private void ScriptService_OnGotStatus(Stellarium.ScriptStatus result) {
        Debug.Log(result.runningScriptId);
    }

    private void ScriptService_OnGotScriptList(string[] result) {
        Debug.Log(result[0]);
    }

    private void ScriptService_OnGotInfoHTML(string result) {
        Debug.Log(result);
    }

    private void ScriptService_OnGotInfo(Stellarium.ScriptInfo result) {
        Debug.Log(result.author);
    }

    void Start() {
        StellariumServer.Instance.ScriptService.GetInfo("GZ_UnitySkybox_V015_setLuminance.ssc");
        StellariumServer.Instance.ScriptService.GetInfoHTML("GZ_UnitySkybox_V015_setLuminance.ssc");
        StellariumServer.Instance.ScriptService.GetScriptList();
        StellariumServer.Instance.ScriptService.GetStatus();
        StellariumServer.Instance.ScriptService.Run("GZ_UnitySkybox_V015_setLuminance.ssc");
        StellariumServer.Instance.ScriptService.Stop();
        StellariumServer.Instance.ScriptService.DirectRun("core.setGuiVisible(false);");
    }


    private void OnDisable() {
        ScriptService.OnGotInfo -= ScriptService_OnGotInfo;
        ScriptService.OnGotInfoHTML -= ScriptService_OnGotInfoHTML;
        ScriptService.OnGotScriptList -= ScriptService_OnGotScriptList;
        ScriptService.OnGotStatus -= ScriptService_OnGotStatus;
        ScriptService.OnRanCode -= ScriptService_OnRanCode;
        ScriptService.OnStartedScript -= ScriptService_OnStartedScript;
        ScriptService.OnStoppedScript -= ScriptService_OnStoppedScript;
    }
}
