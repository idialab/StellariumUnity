using UnityEngine;
using System.Collections.Generic;

namespace Stellarium.Services {

    public class ViewService : Service {

        public static event Got<LandscapeList> OnGotLandscapeList;
        public static event Got<string> OnGotLandscapeDescription;
        public static event Got<SkyCultureList> OnGotSkyCultureList;
        public static event Got<string> OnGotSkyCultureDescription;
        public static event Got<ProjectionList> OnGotProjectionList;
        public static event Got<string> OnGotProjectionDescription;

        public override string Identifier
        {
            get
            {
                return "Stellarium.ViewService";
            }
        }

        public override string Path
        {
            get
            {
                return "view";
            }
        }

        public ViewService(StellariumServer stellarium) : base(stellarium) {
            Stellarium = stellarium;
        }

        public void ListLandscape() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "listlandscape", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                LandscapeList landscapeList = new LandscapeList();
                landscapeList.landscapes = new Landscape();
                foreach(KeyValuePair<string, string> landscape in (new JSONObject(result).ToDictionary())) {
                    landscapeList.landscapes.Add(landscape.Key, landscape.Value);
                }
                if(OnGotLandscapeList != null) {
                    OnGotLandscapeList(landscapeList);
                }
            });
        }

        public void GetLandscapeDescription() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "landscapedescription/", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotLandscapeDescription != null) {
                    OnGotLandscapeDescription(result);
                }
            });
        }

        public void ListSkyCulture() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "listskyculture", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                SkyCultureList skyCultureList = new SkyCultureList();
                skyCultureList.skyCultures = new SkyCulture();
                foreach(KeyValuePair<string, string> skyCulture in (new JSONObject(result).ToDictionary())) {
                    skyCultureList.skyCultures.Add(skyCulture.Key, skyCulture.Value);
                }
                if(OnGotSkyCultureList != null) {
                    OnGotSkyCultureList(skyCultureList);
                }
            });
        }

        public void GetSkyCultureDescription() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "skyculturedescription/", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotSkyCultureDescription != null) {
                    OnGotSkyCultureDescription(result);
                }
            });
        }

        public void ListProjection() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "listprojection", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                ProjectionList projectionList = new ProjectionList();
                projectionList.projections = new Projection();
                foreach(KeyValuePair<string, string> projection in (new JSONObject(result).ToDictionary())) {
                    projectionList.projections.Add(projection.Key, projection.Value);
                }
                if(OnGotProjectionList != null) {
                    OnGotProjectionList(projectionList);
                }
            });
        }

        public void GetProjectionDescription() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Stellarium.GET(Path, "projectiondescription", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotProjectionDescription != null) {
                    OnGotProjectionDescription(result);
                }
            });
        }
    }
}