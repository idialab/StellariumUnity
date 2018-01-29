using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

public class SkyboxManager : MonoBehaviour {

    public delegate void SkyboxGenerated(Material skybox);
    public static event SkyboxGenerated OnSkyboxGenerated;

    [SerializeField]
    string skyboxDirectory;
    [SerializeField]
    Material skyboxMaterial;

    Dictionary<string, Texture2D> sides = new Dictionary<string, Texture2D>();
    FileSystemWatcher skyboxWatcher = new FileSystemWatcher();
    readonly Queue<Action> actionQueue = new Queue<Action>();

    void OnEnable() {
        skyboxWatcher.Path = skyboxDirectory;
        skyboxWatcher.NotifyFilter = NotifyFilters.LastWrite;
        skyboxWatcher.Filter = "Unity6-bottom.png";
        skyboxWatcher.Changed += OnSkyboxChanged;
        skyboxWatcher.Created += OnSkyboxChanged;
        skyboxWatcher.EnableRaisingEvents = true;
    }

    void Start() {
        sides.Add("Unity1-north.png", null);
        sides.Add("Unity2-east.png", null);
        sides.Add("Unity3-south.png", null);
        sides.Add("Unity4-west.png", null);
        sides.Add("Unity5-top.png", null);
        sides.Add("Unity6-bottom.png", null);
        StartCoroutine(DoGetImages(skyboxDirectory));
    }

    void Update() {
        // dispatch stuff on main thread
        while(actionQueue.Count > 0) {
            actionQueue.Dequeue().Invoke();
        }
    }

    void OnSkyboxChanged(object source, FileSystemEventArgs e) {
        actionQueue.Enqueue(() => {
            StartCoroutine(DoGetImages(skyboxDirectory));
        });
    }

    Material CreateSkyboxMaterial(Dictionary<string, Texture2D> sides) {

        skyboxMaterial.SetTexture("_FrontTex", sides["Unity1-north.png"]);
        skyboxMaterial.SetTexture("_BackTex", sides["Unity3-south.png"]);
        skyboxMaterial.SetTexture("_LeftTex", sides["Unity2-east.png"]);
        skyboxMaterial.SetTexture("_RightTex", sides["Unity4-west.png"]);
        skyboxMaterial.SetTexture("_UpTex", sides["Unity5-top.png"]);
        skyboxMaterial.SetTexture("_DownTex", sides["Unity6-bottom.png"]);
        if(OnSkyboxGenerated != null) {
            OnSkyboxGenerated(skyboxMaterial);
        }
        return skyboxMaterial;
    }

    void OnDisable() {
        skyboxWatcher.Changed -= OnSkyboxChanged;
        skyboxWatcher.Created -= OnSkyboxChanged;
        skyboxWatcher.EnableRaisingEvents = false;
    }

    IEnumerator DoGetImages(string directory) {
        List<string> filenames = new List<string>(sides.Keys);
        foreach(string filename in filenames) {
            WWW www = new WWW("file://" + directory + filename);
            yield return www;
            sides[filename] = CropTexture(www.texture, new Rect((www.texture.width * .5f) - (www.texture.height * .5f), 0f, www.texture.height, www.texture.height));
        }
        CreateSkyboxMaterial(sides);
    }

    Texture2D CropTexture(Texture2D originalTexture, Rect cropRect) {
        // Make sure the crop rectangle stays within the original Texture dimensions
        cropRect.x = Mathf.Clamp(cropRect.x, 0, originalTexture.width);
        cropRect.width = Mathf.Clamp(cropRect.width, 0, originalTexture.width - cropRect.x);
        cropRect.y = Mathf.Clamp(cropRect.y, 0, originalTexture.height);
        cropRect.height = Mathf.Clamp(cropRect.height, 0, originalTexture.height - cropRect.y);
        if(cropRect.height <= 0 || cropRect.width <= 0)
            return null; // dont create a Texture with size 0

        Texture2D newTexture = new Texture2D((int)cropRect.width, (int)cropRect.height, TextureFormat.RGBA32, false);
        Color[] pixels = originalTexture.GetPixels((int)cropRect.x, (int)cropRect.y, (int)cropRect.width, (int)cropRect.height, 0);
        newTexture.SetPixels(pixels);
        newTexture.Apply();
        newTexture.wrapMode = TextureWrapMode.Clamp;
        return newTexture;
    }

}

