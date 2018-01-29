using System;

namespace Stellarium {

    /**************************
     * Main Service
     **************************/

    [Serializable]
    public struct Location {
        public string name;
        public char role;
        public string landscapeKey;
        public float latitude;
        public float longitude;
        public int altitude;
        public string state;
        public string country;
        public string planet;
    }

    [Serializable]
    public struct Time {
        public double jday;
        public double deltaT;
        public double gmtShift;
        public string timeZone;
        public string utc;
        public string local;
        public bool isTimeNow;
        public double timerate;
    }

    [Serializable]
    public struct View {
        public int fov;
    }

    [Serializable]
    public class Changes : SerializableDictionary<string, bool> { }

    [Serializable]
    public class ChangeList {
        public int id;
        public Changes changes;
    }

    [Serializable]
    public class Status {
        public Time time;
        public Location location;
        public View view;
        public string selectioninfo;
        public ChangeList actionChanges;
        public ChangeList propertyChanges;
    }

    [Serializable]
    public struct PluginInfo {
        public string authors;
        public string contact;
        public string description;
        public string displayedName;
        public bool startByDefault;
        public string version;
    }

    [Serializable]
    public struct Plugin {
        public PluginInfo info;
        public bool loadAtStartup;
        public bool loaded;
    }

    [Serializable]
    public class Plugins : SerializableDictionary<string, Plugin> { }

    [Serializable]
    public struct PluginList {
        public Plugins plugins;
    }

    /**************************
     * Object Service
     **************************/

    [Serializable]
    public class ObjectType {
        public string key;
        public string name;
        public string name_i18n;
    }

    [Serializable]
    public class ObjectTypeArray {
        public ObjectType[] objectTypes;
    }

    /**************************
     * Script Service
     **************************/

    [Serializable]
    public struct ScriptInfo {
        public int id;
        public string name;
        public string name_localized;
        public string description;
        public string description_localized;
        public string author;
        public string license;
    }

    [Serializable]
    public struct ScriptStatus {
        public bool scriptIsRunning;
        public int runningScriptId;
    }

    /**************************
     * Simbad Service
     **************************/

    [Serializable]
    public struct LookupResults {
        public string[] names;
        public int[][] positions;
    }

    [Serializable]
    public struct Lookup {
        public string status;
        public string status_i18n;
        public string errorString;
        public LookupResults results;
    }

    /**************************
     * StelAction Service
     **************************/

    [Serializable]
    public class Action {
        public int id;
        public bool isCheckable;
        public bool isChecked;
        public string text;
    }

    [Serializable]
    public class ActionList {
        public Action[] actions;
    };

    [Serializable]
    public class ActionGroup : SerializableDictionary<string, ActionList> { };

    [Serializable]
    public class ActionGroupList {
        public ActionGroup actionGroups;
    }

    /**************************
     * StelProperty Service
     **************************/

    [Serializable]
    public class PropertyInfo {
        public string value;
        public string variantType;
        public string typeString;
        public string typeEnum;
    }

    [Serializable]
    public class Property : SerializableDictionary<string,PropertyInfo> { };

    public class PropertyList {
        public Property properties;
    }

    /**************************
     * Location Service
     **************************/

    [Serializable]
    public struct Country {
        public string name;
        public string name_i18n;
    }

    [Serializable]
    public struct Planet {
        public string name;
        public string name_i18n;
    }

    /**************************
     * View Service
     **************************/

    [Serializable]
    public class Landscape : SerializableDictionary<string,string> { };

    [Serializable]
    public class LandscapeList {
        public Landscape landscapes;
    }

    [Serializable]
    public class SkyCulture : SerializableDictionary<string, string> { };

    [Serializable]
    public class SkyCultureList {
        public SkyCulture skyCultures;
    }

    [Serializable]
    public class Projection : SerializableDictionary<string, string> { };

    [Serializable]
    public class ProjectionList {
        public Projection projections;
    }

    /**************************
     * Settings
     **************************/

    [Serializable]
    public class FieldOfView {
        public float vertical;
        public float horizontal;
    }
    [Serializable]
    public class Panel {
        public float atmosphereLuminance;
        public float luminance;
    }
    [Serializable]
    public class PanelGroup {
        public Panel northPanel = new Panel();
        public Panel eastPanel = new Panel();
        public Panel southPanel = new Panel();
        public Panel westPanel = new Panel();
        public Panel topPanel = new Panel();
        public Panel bottomPanel = new Panel();
    }
    [Serializable]
    public class CelestialPosition {
        public float azimuth;
        public float altitude;
    }
    [Serializable]
    public class Sun {
        public CelestialPosition position = new CelestialPosition();
    }
    [Serializable]
    public class Moon {
        public CelestialPosition position = new CelestialPosition();
        public float phase;
        public string phaseAngle;
        public float illumination;
    }
    [Serializable]
    public class Settings {
        public string currentLandscapeName;
        public PanelGroup panels = new PanelGroup();
        public FieldOfView fieldOfView = new FieldOfView();
        public DateTime dateTime = new DateTime();
        public float julianDay;
        public Sun sun = new Sun();
        public Moon moon = new Moon();
    }

}