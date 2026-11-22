using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FNA_RPG.Core;
using Microsoft.Xna.Framework.Input;

namespace FNA_RPG.UI
{
    public class Button
    {
        private readonly Texture2D _normal;
		private readonly SpriteFont _font;
		private readonly string _text;
		private readonly Rectangle _bounds;
		private readonly Action _onClick;

        private bool _isHovered;

        public Button(Texture2D normal, SpriteFont font,
			string text, Rectangle bounds, Action onClick)
		{
			_normal = normal;
            _font = font;
            _text = text;
            _bounds = bounds;
            _onClick = onClick;
		}

		public void Update(MouseCursor cursor)
		{
            _isHovered = _bounds.Contains(Input.MousePosition);

            if (_isHovered && Input.LeftClicked)
                _onClick?.Invoke();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// 1. escolher a textura (_isHovered ? _hover : _normal)
			// 2. desenhar no _bounds
			// 3. desenhar o texto centralizado por cima
		}
    }
}