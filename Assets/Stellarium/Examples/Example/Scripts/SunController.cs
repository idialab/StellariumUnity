using UnityEngine;
using Stellarium;

public class SunController : MonoBehaviour {

    public float northOffset = -90f;

    void OnEnable() {
        SettingsManager.OnSettingsGenerated += OnSettingsGenerated;
    }

    void OnSettingsGenerated(Settings s) {
        transform.rotation = Quaternion.Euler(s.sun.position.altitude,s.sun.position.azimuth+northOffset,0f);
    }

    void OnDisable() {
        SettingsManager.OnSettingsGenerated -= OnSettingsGenerated;
    }

}
