using UnityEngine;

public class SkyboxController : MonoBehaviour {

    Material defaultSkybox, stellariumSkybox;

    private void OnEnable() {
        SkyboxManager.OnSkyboxGenerated += SkyboxManager_OnSkyboxGenerated;
    }

    private void Awake() {
        defaultSkybox = RenderSettings.skybox;
    }

    public void ToggleStellariumSkybox(bool on) {
        RenderSettings.skybox = on ? stellariumSkybox : defaultSkybox;
    }

    private void SkyboxManager_OnSkyboxGenerated(Material skybox) {
        stellariumSkybox = skybox;
    }

    private void OnDisable() {
        SkyboxManager.OnSkyboxGenerated -= SkyboxManager_OnSkyboxGenerated;
    }

}
