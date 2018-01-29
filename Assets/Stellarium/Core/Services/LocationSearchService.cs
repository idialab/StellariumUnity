using UnityEngine;
using System.Collections.Generic;

namespace Stellarium.Services {

    public class LocationSearchService : Service {

        public static event Got<string[]> OnGotSearch;
        public static event Got<string[]> OnGotNearby;

        public override string Identifier
        {
            get
            {
                return "Stellarium.LocationSearchService";
            }
        }

        public override string Path
        {
            get
            {
                return "locationsearch";
            }
        }

        public LocationSearchService(StellariumServer stellarium) : base(stellarium) {
            Stellarium = stellarium;
        }

        public void Search(string term) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("term", term);
            Stellarium.GET(Path, "search", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                string[] resultArray = new string[json.Count];
                for(int i = 0; i < json.Count; i++) {
                    resultArray[i] = json[i].str;
                }
                if(OnGotSearch != null) {
                    OnGotSearch(resultArray);
                }
            });
        }

        public void Nearby(string planet = default(string), float latitude = default(float),float longitude = default(float), float radius = default(float)) { //TODO: Find out why I am getting a 400 Bad request
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if(planet != default(string))parameters.Add("planet", planet);
            if(latitude != default(float)) parameters.Add("latitude", latitude.ToString());
            if(longitude != default(float)) parameters.Add("longitude", longitude.ToString());
            if(radius != default(float)) parameters.Add("radius", radius.ToString());
            Stellarium.GET(Path, "countrylist", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotNearby != null) {
                    OnGotNearby(JsonUtility.FromJson<string[]>(result));
                }
            });
        }

    }
}