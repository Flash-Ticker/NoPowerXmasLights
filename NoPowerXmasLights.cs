using Oxide.Core;
using Oxide.Core.Plugins;
using System.Collections.Generic;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("NoPowerXmasLights", "RustFlash", "1.0.0")]
    [Description("Allows you to use Christmas lights without a power connection")]
    public class NoPowerXmasLights : RustPlugin
    {
        void OnServerInitialized()
        {
            UpdateAllLights(999999);
        }

        void OnEntitySpawned(AdvancedChristmasLights lightString)
        {
            if (lightString == null) return;
            
            NextTick(() => {
                lightString.UpdateHasPower(999999, 1);
                lightString.currentEnergy = 999999;
                lightString.SetFlag(BaseEntity.Flags.On, true);
                lightString.SetFlag(BaseEntity.Flags.Reserved8, true);
                lightString.SendNetworkUpdateImmediate();
            });
        }

        private void UpdateAllLights(int power)
        {
            foreach (var lightString in UnityEngine.Object.FindObjectsOfType<AdvancedChristmasLights>())
            {
                if (lightString != null)
                {
                    lightString.UpdateHasPower(power, 1);
                    lightString.currentEnergy = power;
                    lightString.SetFlag(BaseEntity.Flags.On, power > 0);
                    lightString.SetFlag(BaseEntity.Flags.Reserved8, power > 0);
                    lightString.SendNetworkUpdateImmediate();
                }
            }
        }

        void Unload()
        {
            UpdateAllLights(0);
        }

        object OnConsumptionAmount(AdvancedChristmasLights lightString)
        {
            return 0;
        }

        object OnRequiresPower(AdvancedChristmasLights lightString) 
        {
            return false;
        }
    }
}