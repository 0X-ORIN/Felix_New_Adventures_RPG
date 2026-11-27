using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FNA_RPG.Core;
using FNA_RPG.UI;

namespace FNA_RPG.Scenes
{
	public class Missions : Scene
	{
		private readonly SceneManager _sceneManager;
		private readonly List<Button> _buttons = new();

		public Missions(SceneManager sceneManager)
		{
            _sceneManager = sceneManager;
		}

        public override void LoadContent(ContentManager content)
        {
            Background = content.Load<Texture2D>("Images/Backgrounds/TrainingCamp");
        }

		public override void Update(GameTime gameTime)
		{

		}

		public override void Draw(SpriteBatch spriteBatch)
		{

		}
	}
}