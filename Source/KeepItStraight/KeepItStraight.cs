/*
	This file is part of Keep It Straight /L
		© 2024 LisiasT : http://lisias.net <support@lisias.net>
		© 2018-2024 Maja
		© 2016-2018 RealGecko

	Who Am I? /L is licensed as follows:
		* GPL 3.0 : https://www.gnu.org/licenses/gpl-3.0.txt

	Keep It Straight /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the GNU General Public License 3.0
	along with Keep It Straight /L. If not, see <https://www.gnu.org/licenses/>.

*/
using UnityEngine;
using KSP.IO;

namespace KeepItStraight
{
    /// <summary>
    /// Preserve selected camera between scene changes
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
