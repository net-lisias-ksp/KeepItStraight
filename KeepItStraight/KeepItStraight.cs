using UnityEngine;
using KSP.IO;

namespace KeepItStraight
{
    /// <summary>
    /// Preserver selected camera between scene changes
    /// </summary>
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class KeepItStraight : MonoBehaviour
    {
        public static KeepItStraight instance;
        private PluginConfiguration config;
        FlightCamera camera;

        /// <summary>
        /// Constructor
        /// </summary>
        void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            if (config == null)
            {
                config = PluginConfiguration.CreateForType<KeepItStraight>();
            }
            config.load();
            camera = FlightCamera.fetch;
            GameEvents.onFlightReady.Add(FlightReady);
            GameEvents.onPartCouple.Add(PartCouple);
        }


        /// <summary>
        /// Cleanup after ourself
        /// </summary>
        void OnDestroy()
        {
            GameEvents.onFlightReady.Remove(FlightReady);
            GameEvents.onPartCouple.Remove(PartCouple);
            config.SetValue("CameraMode", FlightCamera.CamMode);
            config.save();
        }


        /// <summary>
        /// Set camera when changing to flight scene
        /// </summary>
        void FlightReady()
        {
            int CamMode = config.GetValue<int>("CameraMode");
            camera.setModeImmediate((FlightCamera.Modes)CamMode);
        }


        /// <summary>
        /// Set camera on part couple
        /// </summary>
        /// <param name="action"></param>
        void PartCouple(GameEvents.FromToAction<Part, Part> action)
        {
            int CamMode = config.GetValue<int>("CameraMode");
            camera.setModeImmediate((FlightCamera.Modes)CamMode);
        }
    }
}
