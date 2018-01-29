using UnityEngine;
using System.Collections.Generic;

namespace Stellarium.Services {

    public class SimbadService : Service {

        public static event Got<Lookup> OnGotLookup;

        public override string Identifier
        {
            get
            {
                return "Stellarium.SimbadService";
            }
        }

        public override string Path
        {
            get
            {
                return "simbad";
            }
        }

        public SimbadService(StellariumServer stellarium) : base(stellarium) {
            Stellarium = stellarium;
        }

        public void Lookup(string str) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("str", str);
            Stellarium.GET(Path, "lookup", parameters, (result, error) => {
                if(error != null) {
                    Debug.LogError(string.Format("[{0}] {1}", Identifier, error));return;
                }
                if(OnGotLookup != null) {
                    OnGotLookup(JsonUtility.FromJson<Lookup>(result));
                }
            });
        }

    }
}