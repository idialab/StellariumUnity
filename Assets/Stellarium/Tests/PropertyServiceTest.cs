using UnityEngine;
using Stellarium;
using Stellarium.Services;
using System.Collections.Generic;

public class PropertyServiceTest : MonoBehaviour {

    private void OnEnable() {
        PropertyService.OnGotList += PropertyService_OnGotList;
        PropertyService.OnSetProperty += PropertyService_OnSetProperty;
    }

    private void PropertyService_OnSetProperty() {
        Debug.Log("Property set");
    }

    private void PropertyService_OnGotList(PropertyList result) {
        foreach(KeyValuePair<string, PropertyInfo> propertyList in result.properties) {
            Debug.Log(propertyList.Key);
            StellariumServer.Instance.PropertyService.Set(propertyList.Key,false.ToString());
            return;
        }
    }

    void Start() {
        StellariumServer.Instance.PropertyService.List();
    }


    private void OnDisable() {
        PropertyService.OnGotList -= PropertyService_OnGotList;
        PropertyService.OnSetProperty -= PropertyService_OnSetProperty;
    }
}
