using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FNA_RPG.Core;
using FNA_RPG.Scenes;

namespace FNA_RPG;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SceneManager _sceneManager;
    private ScreenScaler _scaler;

    public Game1()
    {
        Window.Title = "Félix New Adventures";
        _graphics = new GraphicsDeviceManager(this);
        Window.AllowUserResizing = true;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
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
        _sceneManager = new SceneManager(Content, Exit);
        _sceneManager.ChangeScene(new StarterMenu(_sceneManager));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _scaler.Update();
        Input.Update(_scaler);
        _sceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _scaler.BeginDraw();
        _spriteBatch.Begin();
        _sceneManager.Draw(_spriteBatch);
        _spriteBatch.End();
        _scaler.EndDraw(_spriteBatch);
        base.Draw(gameTime);
    }
}
