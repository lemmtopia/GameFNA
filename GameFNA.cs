using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using LemonicLib;
using LemonicLib.Graphics;
using LemonicLib.Input;

namespace GameFNA;

internal class GameFNA : Core
{
    [STAThread]
    static void Main(string[] args)
    {
        using (GameFNA g = new GameFNA())
        {
            g.Run();
        }
    }

    private SpriteFont _fontMedium;
    private SpriteFont _fontSmall;

    private Texture2D _skyTexture;
    private AnimatedSprite _tileset;

    private float _smileySpeed = 240f;
    private Actor _smiley;

    private Song _linkinParkInTheEnd;
    private SoundEffect _soundEffect;

    private bool _mouseControl = false;
    private string _controlText = "Keyboard/GamePad Control";
    private string _inputActionsText = "WASD/Arrow Keys/DPad/LeftStick - Move\nSpace/A (GamePad) - Fire\nX (GamePad) - Test Vibration";

    private float _framerate;

    private GameFNA() : base("Untitled FNA Game", 800, 480, false)
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();

        MakeInputMap();
    }

    protected override void LoadContent()
    {
        _fontMedium = Content.Load<SpriteFont>("message_font");
        _fontSmall = Content.Load<SpriteFont>("message_font_small");

        _linkinParkInTheEnd = Content.Load<Song>("Audio/linkin-park_in-the-end");
        _soundEffect = Content.Load<SoundEffect>("Audio/tx0_fire1");

        _skyTexture = Content.Load<Texture2D>("Textures/Sky");
        _tileset = AnimatedSprite.FromFile(Content, "SpriteData/TileSet.xml");

        _smiley = new Actor(Content, "SpriteData/SmileyWalk.xml", new Vector2(400, 300));
    }

    protected override void UnloadContent()
    {
        _skyTexture.Dispose();
        _smiley.Dispose();
    }

    protected override void Update(GameTime gameTime)
    {
        float dt = GetDeltaTime(gameTime);
        _framerate = 1 / dt;

        // Play Linkin Park
        if (MediaPlayer.State != MediaState.Playing)
        {
            MediaPlayer.Play(_linkinParkInTheEnd);
            MediaPlayer.Volume = 0.6f;
        }

        _tileset.Update(dt);
        _smiley.Update(dt);

        GamePadInfo gamePadOne = Input.GamePads[(int)PlayerIndex.One];

        if (Input.IsActionPressed(PlayerIndex.One, "ToggleMouseControl"))
        {
            _mouseControl = !_mouseControl;

            if (_mouseControl)
            {
                _controlText = "Mouse Control";
                _inputActionsText = "Left Click - Fire";
            }
            else
            {
                _controlText = "Keyboard/GamePad Control";
                _inputActionsText = "WASD/Arrow Keys/DPad/LeftStick - Move\nSpace/A (GamePad) - Fire\nX (GamePad) - Test Vibration";
            }
        }

        if (_mouseControl)
        {
            _smiley.Position = new Vector2(Input.Mouse.X, Input.Mouse.Y);

            if (Input.IsActionPressed(PlayerIndex.One, "MouseFire"))
            {
                _soundEffect.Play();
            }
        }
        else
        {
            if (Input.IsActionPressed(PlayerIndex.One, "GamePadVibrationTest"))
            {
                gamePadOne.SetVibration(0.5f, TimeSpan.FromSeconds(1));
            }

            if (Input.IsActionPressed(PlayerIndex.One, "Fire"))
            {
                _soundEffect.Play();
            }

            if (gamePadOne.LeftThumbStick != Vector2.Zero)
            {
                _smiley.Position += new Vector2(gamePadOne.LeftThumbStick.X, -gamePadOne.LeftThumbStick.Y) * _smileySpeed * dt;
            }
            else
            {
                if (Input.IsActionDown(PlayerIndex.One, "Left"))
                {
                    _smiley.X -= _smileySpeed * dt;
                }
                if (Input.IsActionDown(PlayerIndex.One, "Right"))
                {
                    _smiley.X += _smileySpeed * dt;
                }
                if (Input.IsActionDown(PlayerIndex.One, "Up"))
                {
                    _smiley.Y -= _smileySpeed * dt;
                }
                if (Input.IsActionDown(PlayerIndex.One, "Down"))
                {
                    _smiley.Y += _smileySpeed * dt;
                }
            }
        }

        _smiley.X = MathHelper.Clamp(_smiley.X, 0, 800 - 64);
        _smiley.Y = MathHelper.Clamp(_smiley.Y, 0, 480 - 64);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        base.Draw(gameTime);

        SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        SpriteBatch.Draw(_skyTexture, new Rectangle(0, 0, 800, 480), Color.White);

        SpriteBatch.DrawString(_fontMedium, _controlText, new Vector2(12, 360), Color.Black);
        SpriteBatch.DrawString(_fontSmall, _inputActionsText, new Vector2(12, 400), Color.Black);
        SpriteBatch.DrawString(_fontSmall, _framerate.ToString("0") + " FPS", new Vector2(12, 12), Color.Black);

        _tileset.Draw(SpriteBatch, new Vector2(64, 64), Color.White);
        _smiley.Draw(SpriteBatch);

        SpriteBatch.End();
    }

    private void MakeInputMap()
    {
        Input.AddAction("Left", [Keys.Left, Keys.A], [Buttons.DPadLeft], []);
        Input.AddAction("Right", [Keys.Right, Keys.D], [Buttons.DPadRight], []);
        Input.AddAction("Up", [Keys.Up, Keys.W], [Buttons.DPadUp], []);
        Input.AddAction("Down", [Keys.Down, Keys.S], [Buttons.DPadDown], []);
        Input.AddAction("Fire", [Keys.Space], [Buttons.A], []);
        Input.AddAction("MouseFire", [], [], [MouseButton.Left]);
        Input.AddAction("GamePadVibrationTest", [], [Buttons.X], []);
        Input.AddAction("ToggleMouseControl", [Keys.V], [], []);
    }
}