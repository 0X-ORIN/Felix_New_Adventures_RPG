using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FNA_RPG.Core
{
	public static class Input
	{
		private static MouseState _current;
		private static MouseState _previous;

		public static Point MousePosition { get; private set; }
		public static bool LeftClicked { get; private set; }

		public static void Update(ScreenScaler scaler)
		{
            _previous = _current;
            _current = Mouse.GetState();
            MousePosition = scaler.ToVirtual(_current.Position);
            LeftClicked = (_current.LeftButton == ButtonState.Pressed) && (_previous.LeftButton == ButtonState.Released);
		}
	}
}