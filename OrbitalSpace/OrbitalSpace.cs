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

    private const float AltitudeMin = 2200f; // Le ciel s'active quand Y > 2000 (ciel haut / espace)
    private const float FadeDistance = 600f; // Distance de fondu

    private int messageTimer = 0;
    private bool messageShown = false;

    public void UpdateUI(GameTime gameTime)
    {
        if (!messageShown)
        {
            Main.NewText("Bienvenue en orbite !", 100, 200, 255);
            messageShown = true;
            messageTimer = 60; // 60 frames = 1 seconde
        }

        if (messageTimer > 0)
        {
            messageTimer--;
            if (messageTimer == 0)
            {
                Main.NewText("1 seconde plus tard...", 255, 255, 0);
                messageShown = false;
                messageTimer = 60;
            }
        }
    }

    public override void OnLoad()
    {
        // Charge la texture une seule fois
        skyTexture = ModContent.Request<Texture2D>("OrbitalSpace/Assets/Night").Value;
    }

    public override void Update(GameTime gameTime)
    {
        float playerY = Main.LocalPlayer?.position.Y ?? float.MaxValue;
        // Active le ciel si le joueur est en très haute altitude (Y < AltitudeMin)
            isActive = playerY < (AltitudeMin + FadeDistance);
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
        try
        {
            var asset = ModContent.Request<Texture2D>("OrbitalSpace/Assets/Night");
            skyTexture = asset.Value;
        }
        catch (Exception)
        {
            // Nécessaire pour éviter les crashs liés au chargement async
            return;
        }

        if (maxDepth >= 0 && minDepth < 1000.0 && skyTexture != null)
        {
            float altitude = Main.LocalPlayer?.position.Y ?? 1f;

            // Calcul de l'alpha pour un fondu progressif (0 -> 1) entre (AltitudeMin + FadeDistance) et AltitudeMin
            float alpha = MathHelper.Clamp((AltitudeMin + FadeDistance - altitude) / FadeDistance, 0f, 1f);

            // Affiche l'image avec opacité variable selon l'altitude
            if (alpha > 0f)
            {
                spriteBatch.Draw(
                    skyTexture,
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),
                    Color.White * alpha
                );
            }
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
