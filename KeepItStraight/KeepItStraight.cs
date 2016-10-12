using System;
using UnityEngine;
using KSP.IO;

namespace KeepItStraight
{
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class KeepItStraight: MonoBehaviour
	{
		public static KeepItStraight instance;
		private PluginConfiguration config;
		FlightCamera camera;

		void Awake() {
			if (instance != null) {
				Destroy (this);
				return;
			}

			if (config == null) {
				config = PluginConfiguration.CreateForType<KeepItStraight> ();
			}
			config.load ();
			camera = FlightCamera.fetch;
			GameEvents.onFlightReady.Add (FlightReady);
			GameEvents.onPartCouple.Add (onPartCouple);
		}

		void OnDestroy()
		{
			GameEvents.onFlightReady.Remove (FlightReady);
			GameEvents.onPartCouple.Remove (onPartCouple);
			config.SetValue ("CameraMode", FlightCamera.CamMode);
			config.save ();
		}

		void FlightReady() {
			int CamMode = config.GetValue<int>("CameraMode");
			camera.setModeImmediate((FlightCamera.Modes)CamMode);
		}

		void onPartCouple(GameEvents.FromToAction<Part,Part> action) {
			int CamMode = config.GetValue<int>("CameraMode");
			camera.setModeImmediate((FlightCamera.Modes)CamMode);
		}
	}
}
