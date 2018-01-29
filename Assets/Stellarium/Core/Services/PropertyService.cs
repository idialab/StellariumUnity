using UnityEngine;
using System.Collections.Generic;

namespace Stellarium.Services {

    public class PropertyService : Service {

        public static event Got<PropertyList> OnGotList;
        public static event Posted OnSetProperty;

        public override string Identifier
        {
            get
            {
                return "Stellarium.PropertyService";
            }
        }

        public override string Path
        {
            get
            {
                return "stelproperty";
            }
        }

        public PropertyService(StellariumServer stellarium) : base(stellarium) {
            Stellarium = stellarium;
        }

        public void List() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "list", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                PropertyList propertyList = new PropertyList();
                propertyList.properties = new Property();
                foreach(string propertyName in json.keys) {
                    propertyList.properties.Add(propertyName, JsonUtility.FromJson<PropertyInfo>(json.GetField(propertyName).ToString()));
                }
                if(OnGotList != null) {
                    OnGotList(propertyList);
                }
            });
        }

        public void Set(string id,string value) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            parameters.Add("value", value);
            Stellarium.POST(Path, "set", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnSetProperty != null) {
                    OnSetProperty();
                }
            });
        }

    }
}