using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FNA_RPG.Core;
using System;

namespace FNA_RPG.UI
{
	public class Hud
	{
		private readonly SceneManager _sceneManager;
		private readonly List<Button> _buttons = new();

		private const int IconHeight = 100;
		private const int Gap = 16;
		private int _nextRight = ScreenScaler.VirtualWidth - 36;

		private void AddIcon(Texture2D texture, Action onClick)
		{
			int width = (int)(IconHeight * texture.Width / (float)texture.Height);

			_buttons.Add(new Button(texture, null, null,
				new Rectangle(_nextRight - width, 20, width, IconHeight), onClick, false));

			_nextRight -= width + Gap;
		}

		public Hud(SceneManager sceneManager, ContentManager content)
		{
			_sceneManager = sceneManager;

			Texture2D menuIcon = content.Load<Texture2D>("Images/Interface/MenuButton");
			Texture2D mapIcon = content.Load<Texture2D>("Images/Interface/MapButton");
			Texture2D invIcon = content.Load<Texture2D>("Images/Interface/InvButton");
			Texture2D settingsIcon = content.Load<Texture2D>("Images/Interface/ConfigButton");

			AddIcon(settingsIcon, () => { });
			AddIcon(invIcon, () => { });
			AddIcon(menuIcon, () => { });
			AddIcon(mapIcon, () => { });
		}

		public void Update(GameTime gameTime)
		{
			foreach (Button button in _buttons)
			{
				button.Update(gameTime);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Button button in _buttons)
			{
				button.Draw(spriteBatch);
			}
		}
	}
}