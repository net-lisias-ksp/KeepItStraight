using System;
using UnityEngine;
using KSP.IO;

namespace KeepItStraight
{
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class KeepItStraight: MonoBehaviour
	{
		private PluginConfiguration config;
		FlightCamera camera;
		void Awake() {
			if (config == null) {
				config = PluginConfiguration.CreateForType<KeepItStraight> ();
			}
			config.load ();
			camera = FlightCamera.fetch;
			GameEvents.onFlightReady.Add (FlightReady);
		}

		void OnDestroy()
		{
			GameEvents.onFlightReady.Remove (FlightReady);
			config.SetValue ("CameraMode", FlightCamera.CamMode);
			config.save ();
		}

		void FlightReady() {
			int CamMode = config.GetValue<int>("CameraMode");
			camera.setModeImmediate((FlightCamera.Modes)CamMode);
		}
	}
}
