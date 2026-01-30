using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LemonicLib.Graphics;

public class SpriteRegion : Sprite
{
    public Rectangle Region { get; set; }

    public SpriteRegion() : base()
    {
        
    }

    public SpriteRegion(Texture2D texture, Rectangle region) : base (texture)
    {
        Region = region;
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        spriteBatch.Draw(Texture, position, Region, color);
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, int layerDepth)
    {
        spriteBatch.Draw(Texture, position, Region, color, rotation, origin, scale, spriteEffects, layerDepth);
    }
}