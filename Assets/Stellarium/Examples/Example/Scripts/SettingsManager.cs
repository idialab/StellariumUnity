using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using Stellarium;

public class SettingsManager : MonoBehaviour {

    public delegate void SettingsGenerated(Settings s);
    public static event SettingsGenerated OnSettingsGenerated;

    public string settingsDirectory;
    public string outputFileName = "unityData.txt";

    FileSystemWatcher settingsWatcher = new FileSystemWatcher();
    readonly Queue<System.Action> actionQueue = new Queue<System.Action>();

    void OnEnable() {
        settingsWatcher.Path = settingsDirectory;
        settingsWatcher.NotifyFilter = NotifyFilters.LastWrite;
        settingsWatcher.Filter = outputFileName;
        settingsWatcher.Changed += OnSettingsChanged;
        settingsWatcher.Created += OnSettingsChanged;
        settingsWatcher.EnableRaisingEvents = true;
    }

    void Start() {
        StartCoroutine(DoGetSettings(settingsDirectory));
    }

    void Update() {
        while(actionQueue.Count > 0) {
            actionQueue.Dequeue().Invoke();
        }
    }

    void OnSettingsChanged(object source, FileSystemEventArgs e) {
        actionQueue.Enqueue(() => {
            StartCoroutine(DoGetSettings(settingsDirectory));
        });
    }

    void OnDisable() {
        settingsWatcher.Changed -= OnSettingsChanged;
        settingsWatcher.Created -= OnSettingsChanged;
        settingsWatcher.EnableRaisingEvents = false;
    }

    IEnumerator DoGetSettings(string directory) {
        WWW www = new WWW("file://" + directory + outputFileName);
        yield return www;
        if(OnSettingsGenerated != null) {
            OnSettingsGenerated(ParseSettings(www.text));
        }
    }

    Settings ParseSettings(string text) {
        Settings tempSettings = new Settings();
        string[] lines = text.Trim().Split('\n');
        Dictionary<string, string> pairs = new Dictionary<string, string>();
        foreach(string line in lines) {
            string[] pair = line.Trim().Split(new char[] { ':' }, 2);
            pairs.Add(pair[0].Trim(), pair[1].Trim());
        }
        tempSettings.currentLandscapeName = pairs["Current Landscape Name"];
        float.TryParse(pairs["North panel Atm.luminance"], out tempSettings.panels.northPanel.atmosphereLuminance);
        float.TryParse(pairs["North panel luminance"], out tempSettings.panels.northPanel.luminance);
        float.TryParse(pairs["East panel Atm.luminance"], out tempSettings.panels.eastPanel.atmosphereLuminance);
        float.TryParse(pairs["East panel luminance"], out tempSettings.panels.eastPanel.luminance);
        float.TryParse(pairs["South panel Atm.luminance"], out tempSettings.panels.southPanel.atmosphereLuminance);
        float.TryParse(pairs["South panel luminance"], out tempSettings.panels.southPanel.luminance);
        float.TryParse(pairs["West panel Atm.luminance"], out tempSettings.panels.westPanel.atmosphereLuminance);
        float.TryParse(pairs["West panel luminance"], out tempSettings.panels.westPanel.luminance);
        float.TryParse(pairs["Top panel Atm.luminance"], out tempSettings.panels.topPanel.atmosphereLuminance);
        float.TryParse(pairs["Top panel luminance"], out tempSettings.panels.topPanel.luminance);
        float.TryParse(pairs["Bottom panel Atm.luminance"], out tempSettings.panels.bottomPanel.atmosphereLuminance);
        float.TryParse(pairs["Bottom panel luminance"], out tempSettings.panels.bottomPanel.luminance);
        float.TryParse(pairs["Vertical FoV"], out tempSettings.fieldOfView.vertical);
        float.TryParse(pairs["Horizontal FoV"], out tempSettings.fieldOfView.horizontal);
        DateTime.TryParse(pairs["Date"], out tempSettings.dateTime);
        float.TryParse(pairs["JD"], out tempSettings.julianDay);
        float.TryParse(pairs["Sun Azimuth"], out tempSettings.sun.position.azimuth);
        float.TryParse(pairs["Sun Altitude"], out tempSettings.sun.position.altitude);
        float.TryParse(pairs["Moon Azimuth"], out tempSettings.moon.position.azimuth);
        float.TryParse(pairs["Moon Altitude"], out tempSettings.moon.position.altitude);
        float.TryParse(pairs["Moon Phase"], out tempSettings.moon.phase);
        tempSettings.moon.phaseAngle = pairs["Moon Phase angle"];
        float.TryParse(pairs["Moon illumination"], out tempSettings.moon.illumination);
        return tempSettings;
    }

}

