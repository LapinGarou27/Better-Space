using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace OrbitalSpace;

public class OrbitalSky : CustomSky
{
    private bool isActive;
    private Texture2D skyTexture;

    public override void OnLoad()
    {
        Main.NewText("OnLoad appelé !", Color.Green);
        skyTexture = ModContent.Request<Texture2D>("OrbitalSpace/Assets/test").Value;
    }

    public override void Update(GameTime gameTime) {}

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
        try
        {

            var asset = ModContent.Request<Texture2D>("OrbitalSpace/Assets/test");
            skyTexture = asset.Value;
        }
        catch (Exception ex)
        {
            // I don't know the purpose of this catch but it make the mod works, So I leave it here...
        }
        if (maxDepth >= 0 && minDepth < 1000.0)
        {
            spriteBatch.Draw(
                skyTexture,
                new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),
                Color.White * 0.8f
            );
        }
    }

    public override bool IsActive() => isActive;

    public override void Activate(Vector2 position, params object[] args)
    {
        isActive = true;
    }

    public override void Deactivate(params object[] args)
    {
        isActive = false;
    }

    public override float GetCloudAlpha() => 0f;

    public override void Reset()
    {
        isActive = false;
    }
}