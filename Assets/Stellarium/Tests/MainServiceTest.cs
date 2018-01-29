using UnityEngine;
using Stellarium.Services;

public class MainServiceTest : MonoBehaviour {

    private void OnEnable() {
        MainService.OnGotPlugins += MainService_OnGotPlugins;
        MainService.OnGotStatus += MainService_OnGotStatus;
        MainService.OnSetFocus += MainService_OnSetFocus;
        MainService.OnSetFOV += MainService_OnSetFOV;
        MainService.OnSetMove += MainService_OnSetMove;
        MainService.OnSetTime += MainService_OnSetTime;
    }

    private void MainService_OnSetTime() {
        Debug.Log("Set time");
    }

    private void MainService_OnSetMove() {
        Debug.Log("Set Move");
    }

    private void MainService_OnSetFOV() {
        Debug.Log("Set fov");
    }

    private void MainService_OnSetFocus() {
        Debug.Log("Set focus");
    }

    private void MainService_OnGotStatus(Stellarium.Status result) {
        Debug.Log(result.location.latitude);
    }

    private void MainService_OnGotPlugins(Stellarium.PluginList result) {
        Debug.Log(result.plugins.Count);
    }

    void Start() {
        StellariumServer.Instance.MainService.GetPlugins();
        StellariumServer.Instance.MainService.GetStatus();
        StellariumServer.Instance.MainService.SetFocus();
        StellariumServer.Instance.MainService.SetFOV(30f);
        StellariumServer.Instance.MainService.SetMove(1f, 1f);
        StellariumServer.Instance.MainService.SetTime(0f, 0f);
    }


    private void OnDisable() {
        MainService.OnGotPlugins -= MainService_OnGotPlugins;
        MainService.OnGotStatus -= MainService_OnGotStatus;
        MainService.OnSetFocus -= MainService_OnSetFocus;
        MainService.OnSetFOV -= MainService_OnSetFOV;
        MainService.OnSetMove -= MainService_OnSetMove;
        MainService.OnSetTime -= MainService_OnSetTime;
    }
}
