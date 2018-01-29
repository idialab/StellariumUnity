using UnityEngine;
using System.Collections.Generic;

namespace Stellarium.Services {

    public class MainService : Service {

        public static event Got<Status> OnGotStatus;
        public static event Got<PluginList> OnGotPlugins;

        public static event Posted OnSetTime;
        public static event Posted OnSetFocus;
        public static event Posted OnSetMove;
        public static event Posted OnSetFOV;

        public override string Identifier
        {
            get
            {
                return "Stellarium.MainService";
            }
        }

        public override string Path
        {
            get
            {
                return "main";
            }
        }

        JSONObject statusJSON; //TODO: Automate state synchronization

        public MainService(StellariumServer stellarium) : base(stellarium) {
            Stellarium = stellarium;
        }

        public void GetStatus(int actionId = default(int), int propId = default(int)) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if(actionId != default(int))parameters.Add("actionId", actionId.ToString());
            if(propId != default(int)) parameters.Add("propId", propId.ToString());
            Stellarium.GET(Path, "status", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                if(statusJSON) {
                    statusJSON.Merge(json);
                } else {
                    statusJSON = json;
                }
                Status status = JsonUtility.FromJson<Status>(result);
                if(statusJSON.HasField("actionChanges")) {
                    Dictionary<string, string> jsonActionChanges = statusJSON.GetField("actionChanges").GetField("changes").ToDictionary();
                    status.actionChanges.changes = new Changes();
                    foreach(KeyValuePair<string, string> change in jsonActionChanges) {
                        bool changed;
                        if(bool.TryParse(change.Value, out changed)) {
                            status.actionChanges.changes.Add(change.Key, changed);
                        }
                    }
                }else {
                    status.actionChanges.changes = new Changes();
                }
                if(statusJSON.HasField("propertyChanges")) {
                    Dictionary<string, string> jsonPropertyChanges = statusJSON.GetField("propertyChanges").GetField("changes").ToDictionary();
                    status.propertyChanges.changes = new Changes();
                    foreach(KeyValuePair<string, string> change in jsonPropertyChanges) {
                        bool changed;
                        if(bool.TryParse(change.Value, out changed)) {
                            status.propertyChanges.changes.Add(change.Key, changed);
                        }
                    }
                }else {
                    status.propertyChanges.changes = new Changes();
                }
                if(OnGotStatus != null) {
                    OnGotStatus(status);
                }
            });
        }

        public void GetPlugins() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "plugins", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                PluginList pluginList = new PluginList();
                pluginList.plugins = new Plugins();
                foreach(string pluginName in json.keys) {
                    pluginList.plugins.Add(pluginName, JsonUtility.FromJson<Plugin>(json.GetField(pluginName).ToString()));
                }
                if(OnGotPlugins != null) {
                    OnGotPlugins(pluginList);
                }
            });
        }

        public void SetTime(double time, double timerate) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("time", time.ToString());
            parameters.Add("timerate", timerate.ToString());
            Stellarium.POST(Path, "time", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnSetTime != null) {
                    OnSetTime();
                }
            });
        }

        public void SetFocus() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            SetFocus(parameters);
        }

        public void SetFocus(string target = default(string)) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if(target!=default(string))parameters.Add("target", target);
            SetFocus(parameters);
        }

        public void SetFocus(Vector3 position = default(Vector3)) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if(position != default(Vector3))parameters.Add("position", position.ToString());
            SetFocus(parameters);
        }

        void SetFocus(Dictionary<string,string> parameters) {
            Stellarium.POST(Path, "focus", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnSetFocus != null) {
                    OnSetFocus();
                }
            });
        }

        public void SetMove(float x, float y) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("x", x.ToString());
            parameters.Add("y", y.ToString());
            Stellarium.POST(Path, "move", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnSetMove != null) {
                    OnSetMove();
                }
            });
        }

        public void SetFOV(float fov) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("fov", fov.ToString());
            Stellarium.POST(Path, "fov", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error)); return;
                }
                if(OnSetFOV != null) {
                    OnSetFOV();
                }
            });
        }

    }

}
