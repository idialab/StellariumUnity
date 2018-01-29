using UnityEngine;
using Stellarium;
using Stellarium.Services;
using System.Collections.Generic;

public class ViewServiceTest : MonoBehaviour {

    private void OnEnable() {
        ViewService.OnGotLandscapeDescription += ViewService_OnGotLandscapeDescription;
        ViewService.OnGotLandscapeList += ViewService_OnGotLandscapeList;
        ViewService.OnGotProjectionDescription += ViewService_OnGotProjectionDescription;
        ViewService.OnGotProjectionList += ViewService_OnGotProjectionList;
        ViewService.OnGotSkyCultureDescription += ViewService_OnGotSkyCultureDescription;
        ViewService.OnGotSkyCultureList += ViewService_OnGotSkyCultureList;
    }

    private void ViewService_OnGotSkyCultureList(SkyCultureList result) {
        Debug.Log(result.skyCultures.Count);
    }

    private void ViewService_OnGotSkyCultureDescription(string result) {
        Debug.Log(result);
    }

    private void ViewService_OnGotProjectionList(ProjectionList result) {
        Debug.Log(result.projections.Count);
    }

    private void ViewService_OnGotProjectionDescription(string result) {
        Debug.Log(result);
    }

    private void ViewService_OnGotLandscapeList(LandscapeList result) {
        Debug.Log(result.landscapes.Count);
    }

    private void ViewService_OnGotLandscapeDescription(string result) {
        Debug.Log(result);
    }

    void Start() {
        StellariumServer.Instance.ViewService.ListLandscape();
        StellariumServer.Instance.ViewService.ListProjection();
        StellariumServer.Instance.ViewService.ListSkyCulture();
        StellariumServer.Instance.ViewService.GetLandscapeDescription();
        StellariumServer.Instance.ViewService.GetProjectionDescription();
        StellariumServer.Instance.ViewService.GetSkyCultureDescription();
    }


    private void OnDisable() {
        ViewService.OnGotLandscapeDescription -= ViewService_OnGotLandscapeDescription;
        ViewService.OnGotLandscapeList -= ViewService_OnGotLandscapeList;
        ViewService.OnGotProjectionDescription -= ViewService_OnGotProjectionDescription;
        ViewService.OnGotProjectionList -= ViewService_OnGotProjectionList;
        ViewService.OnGotSkyCultureDescription -= ViewService_OnGotSkyCultureDescription;
        ViewService.OnGotSkyCultureList -= ViewService_OnGotSkyCultureList;
    }
}
