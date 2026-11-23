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
    public class Lobby : Scene
    {
        private readonly SceneManager _sceneManager;

        public Lobby(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public override void LoadContent(ContentManager content)
        {
            Background = content.Load<Texture2D>("Images/Backgrounds/Lobby");
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