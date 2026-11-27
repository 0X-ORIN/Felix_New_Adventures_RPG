using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FNA_RPG.Core;
using FNA_RPG.UI;

namespace FNA_RPG.Scenes
{
    public class Inventory : Scene
    {
        private readonly SceneManager _sceneManager;
        private Texture2D _world;
        private Texture2D _frame; 
        private Texture2D _rift;

        public Inventory(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public override void LoadContent(ContentManager content)
        {
            _world = content.Load<Texture2D>("Images/Backgrounds/Lobby");
            _frame = content.Load<Texture2D>("Images/Backgrounds/CrystalInteraction");
            _rift = content.Load<Texture2D>("Images/Backgrounds/Inventory");
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle full = new Rectangle(0, 0, ScreenScaler.VirtualWidth, ScreenScaler.VirtualHeight);
            int riftW = (int)(ScreenScaler.VirtualWidth * 1.1f);
            int riftH = riftW * _rift.Height / _rift.Width;
            Rectangle riftRect = new Rectangle(
                (ScreenScaler.VirtualWidth - riftW) / 2,
                (ScreenScaler.VirtualHeight - riftH) / 2,
                riftW, riftH);

            spriteBatch.Draw(_world, full, Color.White * 0.35f);
            spriteBatch.Draw(_rift, riftRect, Color.White);
            spriteBatch.Draw(_frame, full, Color.White * 0.88f);
        }
    }
}