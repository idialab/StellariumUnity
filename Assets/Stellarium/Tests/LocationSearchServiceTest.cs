using UnityEngine;
using LocationService = Stellarium.Services.LocationService;
using Stellarium.Services;

public class LocationSearchServiceTest : MonoBehaviour {

    private void OnEnable() {
        LocationSearchService.OnGotNearby += LocationSearchService_OnGotNearby;
        LocationSearchService.OnGotSearch += LocationSearchService_OnGotSearch;
        LocationService.OnGotList += LocationService_OnGotList;
        MainService.OnGotStatus += MainService_OnGotStatus;
    }

    private void MainService_OnGotStatus(Stellarium.Status result) {
        StellariumServer.Instance.LocationSearchService.Nearby(null,45f,35f,100f); // 400 Bad Request
    }

    private void LocationService_OnGotList(string[] result) {
        StellariumServer.Instance.LocationSearchService.Search("Indi");
    }

    private void LocationSearchService_OnGotSearch(string[] result) {
        Debug.Log(result[0]);
    }

    private void LocationSearchService_OnGotNearby(string[] result) {
        Debug.Log(result[0]);
    }

    private void OnDisable() {
        LocationSearchService.OnGotNearby -= LocationSearchService_OnGotNearby;
        LocationSearchService.OnGotSearch -= LocationSearchService_OnGotSearch;
        LocationService.OnGotList -= LocationService_OnGotList;
        MainService.OnGotStatus -= MainService_OnGotStatus;
    }
}
