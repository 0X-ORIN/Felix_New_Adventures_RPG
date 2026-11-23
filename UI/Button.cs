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
		private float _glowAmount;

        public Button(Texture2D texture, SpriteFont font,
			string text, Rectangle bounds, Action onClick)
		{
			_texture = texture;
            _font = font;
            _text = text;
            _bounds = bounds;
            _onClick = onClick;
			_glow = GlowFactory.Create(texture);
		}

		public void Update()
		{
            _isHovered = _bounds.Contains(Input.MousePosition);
			_glowAmount = _isHovered ? 1f : 0f;

            if (_isHovered && Input.LeftClicked)
                _onClick?.Invoke();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// 1. brilho (atrás de tudo)
			if (_glowAmount > 0f)
			{
				float s = _bounds.Width / (float)_texture.Width;
				int pad = (int)(GlowFactory.Padding * s);
				Rectangle glowRect = new Rectangle(_bounds.X - pad, _bounds.Y - pad,
					_bounds.Width + pad * 2, _bounds.Height + pad * 2);
				spriteBatch.Draw(_glow, glowRect, Color.White * _glowAmount);
			}

			// 2. o botão
			spriteBatch.Draw(_texture, _bounds, Color.White);

			// 3. o texto
			if (!string.IsNullOrEmpty(_text))
			{
				Vector2 tamanho = _font.MeasureString(_text);
				Vector2 posicao = _bounds.Center.ToVector2() - tamanho / 2f;
				spriteBatch.DrawString(_font, _text, posicao, Color.White);
			}
				}
			}
}