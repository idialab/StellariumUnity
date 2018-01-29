using UnityEngine;
using Stellarium;

public class MoonController : MonoBehaviour {

    public float northOffset = -90f;

    void OnEnable() {
        SettingsManager.OnSettingsGenerated += OnSettingsGenerated;
    }

    void OnSettingsGenerated(Settings s) {
        transform.rotation = Quaternion.Euler(s.moon.position.altitude, s.moon.position.azimuth + northOffset, 0f);
    }

    void OnDisable() {
        SettingsManager.OnSettingsGenerated -= OnSettingsGenerated;
    }

}
