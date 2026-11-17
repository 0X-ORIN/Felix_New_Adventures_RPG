using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FNA_RPG.Core
{
	public class SceneManager
	{
		private readonly ContentManager _content;
		private Scene _currentScene;

		public SceneManager(ContentManager content)
		{
			_content = content;
		}

		public void ChangeScene(Scene newScene)
		{
			_currentScene = newScene;
			_currentScene.LoadContent(_content);
		}

		public void Update(GameTime gameTime)
		{
			_currentScene?.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			_currentScene?.Draw(spriteBatch);
		}
	}
}