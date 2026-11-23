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

			// sem texto e sem fonte: null, null
			// mantenha a proporção de cada imagem (chute inicial, ajuste olhando)
			_buttons.Add(new Button(menuIcon, null, null, new Rectangle(1190, 20, 65, 72), () => { }));
			_buttons.Add(new Button(mapIcon, null, null, new Rectangle(1110, 20, 69, 72), () => { }));
		}

		public void Update(GameTime gameTime)
		{
			// foreach: cada botão do HUD faz o Update
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// foreach: cada botão do HUD faz o Draw
			// depois: barras de HP e MP
		}
	}
}