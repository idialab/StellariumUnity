using UnityEngine;

namespace Stellarium {

    [CreateAssetMenu(fileName = "NewStellariumConfiguration.asset", menuName = "Stellarium Configuration", order = 0)]
    public class Configuration : ScriptableObject {

        public string host = "localhost";
        public int port = 8090;
        public string APIPATH = "api";

    }

}
