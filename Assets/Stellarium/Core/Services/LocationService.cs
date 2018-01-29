using UnityEngine;
using System.Collections.Generic;

namespace Stellarium.Services {

    public class LocationService : Service {

        public static event Got<string[]> OnGotList;
        public static event Got<Country[]> OnGotCountryList;
        public static event Got<Planet[]> OnGotPlanetList;
        public static event Got<Texture> OnGotPlanetImage;

        public static event Posted OnSetLocationFields;

        public override string Identifier
        {
            get
            {
                return "Stellarium.LocationService";
            }
        }

        public override string Path
        {
            get
            {
                return "location";
            }
        }

        public LocationService(StellariumServer stellarium) : base(stellarium) {
            Stellarium = stellarium;
        }

        public void GetList() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "list", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                string[] locationArray = new string[json.Count];
                for(int i = 0; i < json.Count; i++) {
                    locationArray[i] = json[i].str;
                }
                if(OnGotList != null) {
                    OnGotList(locationArray);
                }
            });
        }

        public void GetCountryList() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "countrylist", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                Country[] countriesArray = new Country[json.Count];
                for(int i = 0; i < json.Count; i++) {
                    countriesArray[i] = JsonUtility.FromJson<Country>(json[i].ToString());
                }
                if(OnGotCountryList != null) {
                    OnGotCountryList(countriesArray);
                }
            });
        }

        public void GetPlanetList() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "planetlist", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                JSONObject json = new JSONObject(result);
                Planet[] planetsArray = new Planet[json.Count];
                for(int i = 0; i < json.Count; i++) {
                    planetsArray[i] = JsonUtility.FromJson<Planet>(json[i].ToString());
                }
                if(OnGotPlanetList != null) {
                    OnGotPlanetList(planetsArray);
                }
            });
        }

        public void GetPlanetImage(string planet) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("planet", planet);
            Stellarium.GETTexture(Path, "planetimage", parameters, (texture, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotPlanetImage != null) {
                    OnGotPlanetImage(texture);
                }
            });
        }

        public void SetLocationFields(string id) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            SetLocationFields(parameters);
        }

        public void SetLocationFields(float latitude = default(float),float longitude = default(float), int altitude = default(int), string name = default(string), string country = default(string), string planet = default(string)) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if(latitude != default(float))parameters.Add("latitude", latitude.ToString());
            if(longitude != default(float)) parameters.Add("longitude", longitude.ToString());
            if(altitude != default(float)) parameters.Add("altitude", altitude.ToString());
            if(name != default(string)) parameters.Add("name", name);
            if(country != default(string)) parameters.Add("country", country);
            if(planet != default(string)) parameters.Add("planet", planet);
            SetLocationFields(parameters);
        }

        void SetLocationFields(Dictionary<string,string> parameters) {
            Stellarium.POST(Path, "setlocationfields", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnSetLocationFields != null) {
                    OnSetLocationFields();
                }
            });
        }

    }
}