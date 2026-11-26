using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FNA_RPG.Core;

namespace FNA_RPG.UI
{
	public class Hud
	{
		private readonly SceneManager _sceneManager;
		private readonly List<Button> _buttons = new();

		public Hud(SceneManager sceneManager, ContentManager content)
		{
			_sceneManager = sceneManager;

			Texture2D menuIcon = content.Load<Texture2D>("Images/Interface/MenuButton");
			Texture2D mapIcon = content.Load<Texture2D>("Images/Interface/MapButton");
			Texture2D invIcon = content.Load<Texture2D>("Images/Interface/InvButton");
			Texture2D settingsIcon = content.Load<Texture2D>("Images/Interface/ConfigButton");

			_buttons.Add(new Button(mapIcon,      null, null, new Rectangle(796,  20, 100, 100), () => { }, false));
			_buttons.Add(new Button(menuIcon,     null, null, new Rectangle(912,  20, 100, 100), () => { }, false));
			_buttons.Add(new Button(invIcon,      null, null, new Rectangle(1028, 20, 100, 100), () => { }, false));
			_buttons.Add(new Button(settingsIcon, null, null, new Rectangle(1144, 20, 100, 100), () => { }, false));
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