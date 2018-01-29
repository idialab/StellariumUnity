using UnityEngine;
using Stellarium.Services;

public class SimbadServiceTest : MonoBehaviour {

    private void OnEnable() {
        SimbadService.OnGotLookup += SimbadService_OnGotLookup;
    }

    private void SimbadService_OnGotLookup(Stellarium.Lookup result) {
        Debug.Log(result.results.names[0]);
    }

    void Start() {
        StellariumServer.Instance.SimbadService.Lookup("w");
    }


    private void OnDisable() {
        SimbadService.OnGotLookup -= SimbadService_OnGotLookup;
    }
}
