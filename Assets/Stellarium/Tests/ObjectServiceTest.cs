using UnityEngine;
using Stellarium.Services;

public class ObjectServiceTest : MonoBehaviour {

    private void OnEnable() {
        ObjectService.OnFoundObjects += ObjectService_OnFoundObjects;
        ObjectService.OnGotInfoHTML += ObjectService_OnGotInfoHTML;
        ObjectService.OnGotObjectsByType += ObjectService_OnGotObjectsByType;
        ObjectService.OnGotObjectTypes += ObjectService_OnGotObjectTypes;
    }

    private void ObjectService_OnGotObjectTypes(Stellarium.ObjectTypeArray result) {
        StellariumServer.Instance.ObjectService.GetObjectsByType(result.objectTypes[0].key);
    }

    private void ObjectService_OnGotObjectsByType(string[] result) {
        StellariumServer.Instance.ObjectService.FindObjects(result[0]);
        StellariumServer.Instance.ObjectService.GetInfoHTML(result[0]);
    }

    private void ObjectService_OnGotInfoHTML(string result) {
        Debug.Log(result);
    }

    private void ObjectService_OnFoundObjects(string[] result) {
        Debug.Log(result[0]);
    }

    void Start() {
        StellariumServer.Instance.ObjectService.GetObjectTypes();
    }


    private void OnDisable() {
        ObjectService.OnFoundObjects -= ObjectService_OnFoundObjects;
        ObjectService.OnGotInfoHTML -= ObjectService_OnGotInfoHTML;
        ObjectService.OnGotObjectsByType -= ObjectService_OnGotObjectsByType;
        ObjectService.OnGotObjectTypes -= ObjectService_OnGotObjectTypes;
    }
}
