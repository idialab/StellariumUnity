# StellariumUnity
A C# library for Unity to interface with Stellarium's Remote Control HTTP API.

## Getting Started
1. Download and install [Unity 2019.4](https://download.unity3d.com/download_unity/0af376155913/UnityDownloadAssistant-2019.4.0f1.exe) (any version after the addition of UnityWebRequest will likely work, but this is what I'm testing on)
2. Download and open the [StellariumUnity](https://github.com/idialab/StellariumUnity/archive/master.zip) project in Unity.
3. Download and install [Stellarium v0.20.2](https://github.com/Stellarium/stellarium/releases/download/v0.20.2/stellarium-0.20.2-win64.exe). (any version after about v0.15 should work, but this is what I'm testing on)
4. Open Stellarium.
5. Click Configuration [F2].
6. Click the Plugins tab.
7. Click the Remote Control plugin in the left-hand list.
8. Check Load at startup.
9. Close and re-open Stellarium.
10. Repeat steps 5-7.
11. Click configure.
12. Check Server enabled and Enable automatically at startup. (Allow access if required)
13. Set the port to 8090.
14. Save settings as default.
15. Close the configure window.
16. Click the Scripts tab.
17. Select skybox.ssc script in the left-hand list.
18. Check Close window when script runs.
19. Run the script. (You should see the window close and the camera point to the cardinal directions, up, and down sequentially)
20. Verify that screenshots have been captured in Users\[USERNAME]\Pictures\Stellarium.
21. In the Unity editor, open the example scene in Assets/Stellarium/Examples/Example/Scenes/Example.
22. Press play.
23. In the game window, check Use Stellarium Skybox.
24. Experiment changing the time and date and click Update to see the skybox change in the game window.