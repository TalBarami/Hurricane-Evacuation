using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Settings
{
    sealed class SettingsSingleton
    {
        public static ISettings Instance { get; set; } = null;
    }
}
