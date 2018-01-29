using System.Collections;
using System.Collections.Generic;
using Stellarium;
using Stellarium.Services;
using UnityEngine.Networking;

public class StellariumServer : UnityEngine.MonoBehaviour {

    public delegate void StellariumInitialized();
    public static event StellariumInitialized OnStellariumInitialized;

    public static StellariumServer Instance = null;

    [UnityEngine.SerializeField]
    Configuration configuration;
    public Configuration Configuration
    {
        get
        {
            return configuration;
        }
    }

    public MainService MainService { get; private set; }
    public ObjectService ObjectService { get; private set; }
    public ScriptService ScriptService { get; private set; }
    public SimbadService SimbadService { get; private set; }
    public ActionService ActionService { get; private set; }
    public PropertyService PropertyService { get; private set; }
    public LocationService LocationService { get; private set; }
    public LocationSearchService LocationSearchService { get; private set; }
    public ViewService ViewService { get; private set; }

    void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
        Initialize();
    }

    void Initialize() {
        MainService = new MainService(this);
        ObjectService = new ObjectService(this);
        ScriptService = new ScriptService(this);
        SimbadService = new SimbadService(this);
        ActionService = new ActionService(this);
        PropertyService = new PropertyService(this);
        LocationService = new LocationService(this);
        LocationSearchService = new LocationSearchService(this);
        ViewService = new ViewService(this);
        if(OnStellariumInitialized != null) {
            OnStellariumInitialized();
        }
    }

    public void GET(string service, string operation, Dictionary<string, string> parameters, Service.TextCallback callback) {
        StartCoroutine(DoGET(service, operation, parameters, (result, error) => {
            callback(result, error);
        }));
    }

    public void GETTexture(string service, string operation, Dictionary<string, string> parameters, Service.TextureCallback callback) {
        StartCoroutine(DoGETTexture(service, operation, parameters, (texture, error) => {
            callback(texture, error);
        }));
    }

    public void POST(string service, string operation, Dictionary<string, string> parameters, Service.TextCallback callback) {
        StartCoroutine(DoPOST(service, operation, parameters, (result, error) => {
            callback(result, error);
        }));
    }

    IEnumerator DoGET(string service, string operation, Dictionary<string, string> parameters, Service.TextCallback callback) {
        if(Configuration == null) {
            UnityEngine.Debug.LogError("[Stellarium] Configuration file not set");
            yield break;
        }
        string requestURL = string.Format("{0}:{1}/{2}/{3}/{4}", Configuration.host, Configuration.port, Configuration.APIPATH, service, operation);
        int i = 0;
        foreach(KeyValuePair<string, string> parameter in parameters) {
            requestURL += i == 0 ? "?" : "&";
            requestURL += parameter.Key + "=" + parameter.Value;
            i++;
        }
        UnityWebRequest uwr = UnityWebRequest.Get(requestURL);
        uwr.chunkedTransfer = false;
        yield return uwr.SendWebRequest();
        callback(uwr.downloadHandler.text, uwr.error);
    }

    IEnumerator DoGETTexture(string service, string operation, Dictionary<string, string> parameters, Service.TextureCallback callback) {
        if (Configuration == null) {
            UnityEngine.Debug.LogError("[Stellarium] Configuration file not set");
            yield break;
        }
        string requestURL = string.Format("{0}:{1}/{2}/{3}/{4}", Configuration.host, Configuration.port, Configuration.APIPATH, service, operation);
        int i = 0;
        foreach (KeyValuePair<string, string> parameter in parameters) {
            requestURL += i == 0 ? "?" : "&";
            requestURL += parameter.Key + "=" + parameter.Value;
            i++;
        }
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(requestURL);
        uwr.chunkedTransfer = false;
        yield return uwr.SendWebRequest();
        callback(((DownloadHandlerTexture)uwr.downloadHandler).texture, uwr.error);
    }

    IEnumerator DoPOST(string service, string operation, Dictionary<string, string> parameters, Service.TextCallback callback) {
        if(Configuration == null) {
            UnityEngine.Debug.LogError("[Stellarium] Configuration file not set");
            yield break;
        }
        string requestURL = string.Format("{0}:{1}/{2}/{3}/{4}", Configuration.host, Configuration.port, Configuration.APIPATH, service, operation);
        UnityWebRequest uwr = UnityWebRequest.Post(requestURL, parameters);
        uwr.chunkedTransfer = false;
        yield return uwr.SendWebRequest();
        callback(uwr.downloadHandler.text, uwr.error);
    }

}


