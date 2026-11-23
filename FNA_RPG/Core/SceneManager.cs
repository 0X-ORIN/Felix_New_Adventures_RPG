using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FNA_RPG.Core
{
	public class SceneManager
	{
		private readonly ContentManager _content;
		private Scene _currentScene;

		private readonly Action _exitGame;

		public SceneManager(ContentManager content, Action exitGame)
		{
			_content = content;
			_exitGame = exitGame;
		}

		public void ExitGame()
		{
			_exitGame?.Invoke();
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