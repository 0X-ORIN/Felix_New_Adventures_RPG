using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FNA_RPG.Core;
using FNA_RPG.Scenes;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FNA_RPG;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SceneManager _sceneManager;
    private ScreenScaler _scaler;
    private CrashScreen _crash;


    public Game1()
    {
        Window.Title = "Felix New Adventures";
        _graphics = new GraphicsDeviceManager(this);
        Window.AllowUserResizing = true;
        _graphics.PreferredBackBufferWidth = 960;
        _graphics.PreferredBackBufferHeight = 540;
        Content.RootDirectory = "Content";
        _sceneManager = new SceneManager(Content, Exit);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void OnExiting(object sender, ExitingEventArgs args)
    {
        // futuro auto save
        // SaveSystem.AutoSave();
        base.OnExiting(sender, args);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _scaler = new ScreenScaler(GraphicsDevice);
        _crash = new CrashScreen(GraphicsDevice, Content.Load<SpriteFont>("Fonts/MenuFont"));
        StartGame();
    }

    private void StartGame()
    {
        try
        {
            _sceneManager = new SceneManager(Content, Exit);
            _sceneManager.ChangeScene(new StarterMenu(_sceneManager));
        }
        catch (Exception e)
        {
            _crash.Show(e);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (_crash.IsActive)
        {
            switch (_crash.Update())
            {
                case CrashScreen.Action.Restart:
                    _crash.Clear();
                    _spriteBatch = new SpriteBatch(GraphicsDevice);
                    StartGame();
                    break;
                case CrashScreen.Action.Quit:
                    Exit();
                    break;
            }
            return;
        }

        try
        {
            _scaler.Update();
            Input.Update(_scaler);
            _sceneManager.Update(gameTime);
        }
        catch (Exception e)
        {
            _crash.Show(e);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (_crash.IsActive)
        {
            _crash.Draw();
            return;
        }

        try
        {
            _scaler.BeginDraw();
            _spriteBatch.Begin();
            _sceneManager.Draw(_spriteBatch);
            _spriteBatch.End();
            _scaler.EndDraw(_spriteBatch);
        }
        catch (Exception e)
        {
            GraphicsDevice.SetRenderTarget(null);
            _crash.Show(e);
        }

        base.Draw(gameTime);
    }
}
