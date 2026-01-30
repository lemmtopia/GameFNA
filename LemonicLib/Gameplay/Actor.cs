using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using LemonicLib;
using LemonicLib.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace LemonicLib.Gameplay;

internal class Actor
{
    public Vector2 Position { get; set; }

    public float X
    {
        get => Position.X;
        set => Position = new Vector2(value, Position.Y);
    }

    public float Y
    {
        get => Position.Y;
        set => Position = new Vector2(Position.X, value);
    }

    public Color Color { get; set; }

    public float Rotation { get; set; }
    public Vector2 Origin { get; set; }
    public float Scale { get; set; }
    public SpriteEffects SpriteEffects { get; set; }
    public int LayerDepth { get; set; }



    private AnimatedSprite _sprite;

    public Actor(ContentManager content, string spriteFileName, Vector2 position)
    {
        Position = position;
        Color = Color.White;
        Scale = 1.0f;
        Rotation = 0.0f;
        Origin = Vector2.Zero;
        SpriteEffects = SpriteEffects.None;
        LayerDepth = 0;

        // Load the sprite from .xml file
        _sprite = AnimatedSprite.FromFile(content, spriteFileName);
    }

    public Actor(ContentManager content, string spriteFileName, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, int layerDepth)
    {
        Position = position;
        Color = color;
        Rotation = rotation;
        Origin = origin;
        Scale = scale;
        SpriteEffects = spriteEffects;
        LayerDepth = layerDepth;

        // Load the sprite from .xml file
        _sprite = AnimatedSprite.FromFile(content, spriteFileName);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int width = _sprite.Width / _sprite.Columns;
        int height = _sprite.Height / _sprite.Rows;

        _sprite.Draw(spriteBatch, Position, Color, Rotation, Origin, Scale, SpriteEffects, LayerDepth);
    }

    public void Update(float dt)
    {
        _sprite.Update(dt);
    }

    public void Dispose()
    {
        _sprite.Dispose();
    }
}
