using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FNA_RPG.Core;
using FNA_RPG.UI;

namespace FNA_RPG.Scenes
{
	public class StarterMenu : Scene
	{
		private readonly SceneManager _sceneManager;
		private SpriteFont _menuFont;

		private Button _testButton;

		public StarterMenu(SceneManager sceneManager)
		{
			_sceneManager = sceneManager;
		}

		public override void LoadContent(ContentManager content)
		{
			_menuFont = content.Load<SpriteFont>("Fonts/MenuFont");
			Background = content.Load<Texture2D>("Images/Backgrounds/StarterMenu");
			Texture2D baseButton = content.Load<Texture2D>("Images/Interface/BaseButton");
			_testButton = new Button(
				baseButton, _menuFont, "New Game", new Rectangle(640 - 200, 400, 400, 68), () => Console.WriteLine("clicou!")
			);
		}

		public override void Update(GameTime gameTime)
		{
			_testButton.Update();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(Background, new Rectangle(0, 0, ScreenScaler.VirtualWidth, ScreenScaler.VirtualHeight), Color.White);
			_testButton.Draw(spriteBatch);
		}
	}
}