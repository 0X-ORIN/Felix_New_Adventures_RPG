using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FNA_RPG.Core
{
    public class ScreenScaler
    {
        public const int VirtualWidth = 1280;
        public const int VirtualHeight = 720;

        private readonly GraphicsDevice _graphicsDevice;
        private readonly RenderTarget2D _target;

        private Rectangle _destination;
		private float _scale;

        public ScreenScaler(GraphicsDevice graphicsDevice)
		{
			_graphicsDevice = graphicsDevice;
			_target = new RenderTarget2D(graphicsDevice, VirtualWidth, VirtualHeight);
		}
        public void Update()
        {
            int winW = _graphicsDevice.Viewport.Width;
			int winH = _graphicsDevice.Viewport.Height;

			if (winW == 0 || winH == 0)
				return;

			_scale = Math.Min(winW / (float)VirtualWidth, winH / (float)VirtualHeight);

			int width = (int)(VirtualWidth * _scale);
			int height = (int)(VirtualHeight * _scale);
			int x = (winW - width) / 2;
			int y = (winH - height) / 2;

			_destination = new Rectangle(x, y, width, height);
        }
        public void BeginDraw()
        {
            _graphicsDevice.SetRenderTarget(_target);
			_graphicsDevice.Clear(Color.Black);
        }
        public void EndDraw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.SetRenderTarget(null);
			_graphicsDevice.Clear(Color.Black);

			spriteBatch.Begin(samplerState: SamplerState.LinearClamp);
			spriteBatch.Draw(_target, _destination, Color.White);
			spriteBatch.End();
        }
    }
}