using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria;
using Microsoft.Xna.Framework;

namespace OrbitalSpace;

public class OrbitalSkySystem : ModSystem
{
    public override void Load()
    {
        if (!Main.dedServ)
        {
            SkyManager.Instance["OrbitalSpace:OrbitalSky"] = new OrbitalSky();
        }
    }

    public override void OnWorldUnload()
    {
        {
            if (SkyManager.Instance["OrbitalSpace:OrbitalSky"] is OrbitalSky sky)
            {
                sky.Reset();
            }
        }
    }
    public override void PostUpdateWorld()
    {
        if (!SkyManager.Instance["OrbitalSpace:OrbitalSky"].IsActive())
        {
            SkyManager.Instance.Activate("OrbitalSpace:OrbitalSky");
        }
    }
}
