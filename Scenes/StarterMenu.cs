using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FNA_RPG.Core;

namespace FNA_RPG.Scenes
{
	public class StarterMenu : Scene
	{
		private readonly SceneManager _sceneManager;

		public StarterMenu(SceneManager sceneManager)
		{
			_sceneManager = sceneManager;
		}

		public override void LoadContent(ContentManager content)
		{
			Background = content.Load<Texture2D>("Images/Backgrounds/StarterMenu");
		}

		public override void Update(GameTime gameTime)
		{
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(Background, new Rectangle(0, 0, ScreenScaler.VirtualWidth, ScreenScaler.VirtualHeight), Color.White);
		}
	}
}