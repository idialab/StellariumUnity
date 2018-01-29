using UnityEngine;

namespace Stellarium.Services {

    public abstract class Service {

        public delegate void TextCallback(string result,string error);
        public delegate void TextureCallback(Texture texture, string error);
        public delegate void Got<T>(T result);
        public delegate void Posted();
        public delegate void Posted<T>(T result);

        public StellariumServer Stellarium { get; set; }
        public abstract string Identifier { get; }
        public abstract string Path { get; }

        public Service(StellariumServer stellarium) {
            Stellarium = stellarium;
        }

    }



}
