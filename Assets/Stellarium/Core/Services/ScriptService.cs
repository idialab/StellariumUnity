using UnityEngine;
using System.Collections.Generic;

namespace Stellarium.Services {

    public class ScriptService : Service {

        public static event Got<string[]> OnGotScriptList;
        public static event Got<ScriptInfo> OnGotInfo;
        public static event Got<string> OnGotInfoHTML;
        public static event Got<ScriptStatus> OnGotStatus;

        public static event Posted OnStartedScript;
        public static event Posted OnRanCode;
        public static event Posted OnStoppedScript;

        public override string Identifier
        {
            get
            {
                return "Stellarium.ScriptService";
            }
        }

        public override string Path
        {
            get
            {
                return "scripts";
            }
        }

        public ScriptService(StellariumServer stellarium) : base(stellarium) {
            Stellarium = stellarium;
        }

        public void GetScriptList() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "list", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                string[] scriptArray = new string[json.Count];
                for(int i = 0; i < json.Count; i++) {
                    scriptArray[i] = json[i].str;
                }
                if(OnGotScriptList != null) {
                    OnGotScriptList(scriptArray);
                }
            });
        }

        public void GetInfo(string id) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            GetInfo(parameters);
        }

        public void GetInfoHTML(string id) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            parameters.Add("html","true");
            GetInfoHTML(parameters);
        }

        void GetInfo(Dictionary<string,string> parameters) {
            Stellarium.GET(Path, "info", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotInfoHTML != null) {
                    OnGotInfo(JsonUtility.FromJson<ScriptInfo>(result));
                }
            });
        }

        void GetInfoHTML(Dictionary<string, string> parameters) {
            Stellarium.GET(Path, "info", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotInfo != null) {
                    OnGotInfoHTML(result);
                }
            });
        }

        public void GetStatus() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "status", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotStatus != null) {
                    OnGotStatus(JsonUtility.FromJson<ScriptStatus>(result));
                }
            });
        }

        public void Run(string id) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            Stellarium.POST(Path, "run", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnStartedScript != null) {
                    OnStartedScript();
                }
            });
        }

        public void DirectRun(string code,bool useIncludes = default(bool)) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("code", code);
            if(useIncludes != default(bool)) parameters.Add("useIncludes", useIncludes.ToString());
            parameters.Add("useIncludes", useIncludes.ToString());
            Stellarium.POST(Path, "direct", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnRanCode != null) {
                    OnRanCode();
                }
            });
        }

        public void Stop() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.POST(Path, "stop", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnStoppedScript != null) {
                    OnStoppedScript();
                }
            });
        }

    }
}