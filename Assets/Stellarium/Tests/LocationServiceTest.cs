using UnityEngine;
using LocationService = Stellarium.Services.LocationService;

public class LocationServiceTest : MonoBehaviour {

    private void OnEnable() {
        LocationService.OnGotCountryList += LocationService_OnGotCountryList;
        LocationService.OnGotList += LocationService_OnGotList;
        LocationService.OnGotPlanetImage += LocationService_OnGotPlanetImage;
        LocationService.OnGotPlanetList += LocationService_OnGotPlanetList;
        LocationService.OnSetLocationFields += LocationService_OnSetLocationFields;
    }

    private void LocationService_OnSetLocationFields() {
        Debug.Log("Set location fields");
    }

    private void LocationService_OnGotPlanetList(Stellarium.Planet[] result) {
        Debug.Log(result[0].name);
        StellariumServer.Instance.LocationService.GetPlanetImage("Mars");
    }

    private void LocationService_OnGotPlanetImage(Texture result) {
        Debug.Log(result.width);
    }

    private void LocationService_OnGotList(string[] result) {
        StellariumServer.Instance.LocationService.SetLocationFields(result[0]);
    }

    private void LocationService_OnGotCountryList(Stellarium.Country[] result) {
        Debug.Log(result[0].name);
    }

    void Start() {
        StellariumServer.Instance.LocationService.GetCountryList();
        StellariumServer.Instance.LocationService.GetPlanetList();
        StellariumServer.Instance.LocationService.GetList();
    }


    private void OnDisable() {
        LocationService.OnGotCountryList -= LocationService_OnGotCountryList;
        LocationService.OnGotList -= LocationService_OnGotList;
        LocationService.OnGotPlanetImage -= LocationService_OnGotPlanetImage;
        LocationService.OnGotPlanetList -= LocationService_OnGotPlanetList;
        LocationService.OnSetLocationFields -= LocationService_OnSetLocationFields;
    }
}
