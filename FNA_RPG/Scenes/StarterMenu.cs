using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FNA_RPG.Core;
using FNA_RPG.UI;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace FNA_RPG.Scenes
{
	public class StarterMenu : Scene
	{
		private readonly SceneManager _sceneManager;
		private SpriteFont _menuFont;
		private SpriteFont _titleFont;
		private readonly List<Button> _buttons = new();
		private Texture2D _buttonTexture;
		private const string GameTitle = "Felix  New Adventures";

		public StarterMenu(SceneManager sceneManager)
		{
			_sceneManager = sceneManager;
		}

		private void AddButton(string text, int index, Action onClick)
		{
			const int width = 320;
			int height = (int)(width * _buttonTexture.Height / (float)_buttonTexture.Width);
			const int spacing = 75;

			int x = ScreenScaler.VirtualWidth / 2 - width / 2;
			int y = 320 + index * spacing;

			_buttons.Add(new Button(_buttonTexture, _menuFont, text,
				new Rectangle(x, y, width, height), onClick));
		}

		public override void LoadContent(ContentManager content)
		{
			_menuFont = content.Load<SpriteFont>("Fonts/MenuFont");
			_titleFont = content.Load<SpriteFont>("Fonts/TitleFont");
			Background = content.Load<Texture2D>("Images/Backgrounds/StarterMenu");
			_buttonTexture = content.Load<Texture2D>("Images/Interface/BaseButton");
			Song music = content.Load<Song>("Audio/vampires_piano");
			AudioManager.PlayMusic(music);
			
			AddButton("New Game", 0, () => _sceneManager.ChangeScene(new Lobby(_sceneManager)));
			AddButton("Load Game", 1, () => { });
			AddButton("Settings", 2, () => { });
			AddButton("Credits", 3, () => { });
			AddButton("Exit", 4, () => _sceneManager.ExitGame());
		}

		public override void Update(GameTime gameTime)
		{
			foreach (Button button in _buttons)
				button.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, new Rectangle(0, 0, ScreenScaler.VirtualWidth, ScreenScaler.VirtualHeight), Color.White);

			Vector2 size = _titleFont.MeasureString(GameTitle);
			Vector2 pos = new Vector2(ScreenScaler.VirtualWidth / 2f - size.X / 2f, 90);
			spriteBatch.DrawString(_titleFont, GameTitle, pos + new Vector2(3, 3), Color.Black * 0.85f);
			spriteBatch.DrawString(_titleFont, GameTitle, pos, new Color(232, 214, 170));

			foreach (Button button in _buttons)
				button.Draw(spriteBatch);
		}
	}
}