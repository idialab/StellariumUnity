using UnityEngine;
using System.Collections.Generic;

namespace Stellarium.Services {

    public class ObjectService : Service {

        public static event Got<string[]> OnFoundObjects;
        public static event Got<string> OnGotInfoHTML;
        public static event Got<ObjectTypeArray> OnGotObjectTypes;
        public static event Got<string[]> OnGotObjectsByType;

        public override string Identifier
        {
            get
            {
                return "Stellarium.ObjectService";
            }
        }

        public override string Path
        {
            get
            {
                return "objects";
            }
        }

        public ObjectService(StellariumServer stellarium) : base(stellarium) {
            Stellarium = stellarium;
        }

        public void FindObjects(string str) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("str", str);
            Stellarium.GET(Path, "find", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                string[] objectsArray = new string[json.Count];
                for(int i = 0; i < json.Count; i++) {
                    objectsArray[i] = json[i].str;
                }
                if(OnFoundObjects != null) {
                    OnFoundObjects(objectsArray);
                }
            });
        }

        public void GetInfoHTML(string name = default(string)) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if(name != default(string))parameters.Add("name", name);
            Stellarium.GET(Path, "info", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotInfoHTML != null) {
                    OnGotInfoHTML(result);
                }
            });
        }

        public void GetObjectTypes() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "listobjecttypes", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                ObjectTypeArray objectTypeArray = new ObjectTypeArray();
                objectTypeArray.objectTypes = new ObjectType[json.Count];
                for(int i = 0; i < json.Count; i++) {
                    objectTypeArray.objectTypes[i] = JsonUtility.FromJson<ObjectType>(json[i].ToString());
                }
                if(OnGotObjectTypes != null) {
                    OnGotObjectTypes(objectTypeArray);
                }
            });
        }

        public void GetObjectsByType(string type,bool english = default(bool)) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("type", type);
            if(english != default(bool))parameters.Add("english", english.ToString());
            Stellarium.GET(Path, "listobjectsbytype", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                string[] objectsByTypeArray = new string[json.Count];
                for(int i = 0; i < json.Count; i++) {
                    objectsByTypeArray[i] = json[i].str;
                }                   
                if(OnGotObjectsByType != null) {
                    OnGotObjectsByType(objectsByTypeArray);
                }
            });
        }

    }
}