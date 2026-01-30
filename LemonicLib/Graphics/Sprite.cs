using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LemonicLib.Graphics;

public class Sprite
{
    public Texture2D Texture { get; set; }

    public int Width => Texture.Width;
    public int Height => Texture.Height;

    public Sprite()
    {
        
    }

    public Sprite(Texture2D texture)
    {
        Texture = texture;
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        Rectangle sourceRect = new Rectangle(0, 0, Width, Height);

        spriteBatch.Draw(Texture, position, sourceRect, color);
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, int layerDepth)
    {
        Rectangle sourceRect = new Rectangle(0, 0, Width, Height);

        spriteBatch.Draw(Texture, position, sourceRect, color, rotation, origin, scale, spriteEffects, layerDepth);
    }

    public void Dispose()
    {
        Texture.Dispose();
    }
}