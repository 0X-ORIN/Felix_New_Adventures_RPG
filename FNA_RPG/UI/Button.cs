using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FNA_RPG.Core;
using Microsoft.Xna.Framework.Input;

namespace FNA_RPG.UI
{
    public class Button
    {
        private readonly Texture2D _texture;
		private readonly SpriteFont _font;
		private readonly string _text;
		private readonly Rectangle _bounds;
		private readonly Action _onClick;
        private bool _isHovered;
		private readonly Texture2D _glow;
		private float _hoverAmount;
		private readonly bool _useGlow;
		private const float GrowAmount = 0.05f;
		private const float GlowRadius = 250f;
		private const float BaseGlow = 0.55f;
		private const int TileSize = 16;          

        public Button(Texture2D texture, SpriteFont font,
			string text, Rectangle bounds, Action onClick, bool useGlow = true)
		{
			_texture = texture;
            _font = font;
            _text = text;
            _bounds = bounds;
            _onClick = onClick;
			_useGlow = useGlow;

			if (useGlow)
				_glow = GlowFactory.Create(texture);
		}

		public void Update(GameTime gameTime)
		{
            _isHovered = _bounds.Contains(Input.MousePosition);
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			float target = _isHovered ? 1f : 0f;
			float speed = _isHovered ? 14f : 9f;
			_hoverAmount = MathHelper.Lerp(_hoverAmount, target, Math.Min(1f, speed * dt));

            if (_isHovered && Input.LeftClicked)
                _onClick?.Invoke();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			float scale = 1f + GrowAmount * _hoverAmount;
			int w = (int)(_bounds.Width * scale);
			int h = (int)(_bounds.Height * scale);
			Rectangle drawRect = new Rectangle(_bounds.Center.X - w / 2, _bounds.Center.Y - h / 2, w, h);

			if (_useGlow && _hoverAmount > 0.01f)
				DrawGlow(spriteBatch, drawRect);

			spriteBatch.Draw(_texture, drawRect, Color.White);

			if (!string.IsNullOrEmpty(_text))
			{
				Vector2 tamanho = _font.MeasureString(_text);
				Color textColor = Color.Lerp(Color.White, new Color(255, 240, 200), _hoverAmount);
				spriteBatch.DrawString(_font, _text, drawRect.Center.ToVector2(), textColor,
					0f, tamanho / 2f, scale, SpriteEffects.None, 0f);
			}
		}

		private void DrawGlow(SpriteBatch spriteBatch, Rectangle drawRect)
		{
			float sx = drawRect.Width / (float)_texture.Width;
			float sy = drawRect.Height / (float)_texture.Height;
			int originX = drawRect.X - (int)(GlowFactory.Padding * sx);
			int originY = drawRect.Y - (int)(GlowFactory.Padding * sy);

			Vector2 mouse = Input.MousePosition.ToVector2();

			for (int ty = 0; ty < _glow.Height; ty += TileSize)
			{
				for (int tx = 0; tx < _glow.Width; tx += TileSize)
				{
					int tw = Math.Min(TileSize, _glow.Width - tx);
					int th = Math.Min(TileSize, _glow.Height - ty);
					Rectangle source = new Rectangle(tx, ty, tw, th);

					int x0 = originX + (int)MathF.Round(tx * sx);
					int y0 = originY + (int)MathF.Round(ty * sy);
					int x1 = originX + (int)MathF.Round((tx + tw) * sx);
					int y1 = originY + (int)MathF.Round((ty + th) * sy);
					Rectangle dest = new Rectangle(x0, y0, x1 - x0, y1 - y0);

					float dist = Vector2.Distance(dest.Center.ToVector2(), mouse);
					float near = MathHelper.Clamp(1f - dist / GlowRadius, 0f, 1f);
					float intensity = BaseGlow + (1f - BaseGlow) * near;

					spriteBatch.Draw(_glow, dest, source, Color.White * (intensity * _hoverAmount));
				}
			}
		}
	}
}