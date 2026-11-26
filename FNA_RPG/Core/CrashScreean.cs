using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FNA_RPG.Core
{
    public class CrashScreen
    {
        private const float Scale = 0.5f;

        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _batch;
        private readonly SpriteFont _font;
        private string _text;

        public bool IsActive => _text != null;

        public CrashScreen(GraphicsDevice graphicsDevice, SpriteFont font)
        {
            _graphicsDevice = graphicsDevice;
            _font = font;
            _batch = new SpriteBatch(graphicsDevice);
        }

        public void Clear() => _text = null;

        public void Show(Exception e)
        {
            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "crash.txt"), e.ToString());

            StringBuilder sb = new();
            sb.AppendLine($"ERRO: {e.GetType().Name}");
            sb.AppendLine(e.Message);

            if (e.InnerException != null)
                sb.AppendLine($"CAUSE: {e.InnerException.GetType().Name}: {e.InnerException.Message}");

            sb.AppendLine();
            sb.AppendLine("Were:");

            foreach (StackFrame frame in new StackTrace(e, true).GetFrames())
            {
                string file = frame.GetFileName();
                if (file == null) continue;

                var method = frame.GetMethod();
                sb.AppendLine($"{Path.GetFileName(file)}:{frame.GetFileLineNumber()}  {method?.DeclaringType?.Name}.{method?.Name}");
            }

            sb.AppendLine();
            sb.AppendLine("[R] Return to menu    [Esc] Exit");

            _text = sb.ToString();
        }
        public enum Action { None, Restart, Quit }

        public Action Update()
        {
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.R)) return Action.Restart;
            if (kb.IsKeyDown(Keys.Escape)) return Action.Quit;
            return Action.None;
        }

        public void Draw()
        {
            _graphicsDevice.SetRenderTarget(null);
            _graphicsDevice.Clear(Color.Black);

            float maxWidth = (_graphicsDevice.Viewport.Width - 40) / Scale;
            float y = 20;

            _batch.Begin();
            foreach (string raw in _text.Replace("\r", "").Split('\n'))
            {
                foreach (string line in Wrap(Sanitize(raw), maxWidth))
                {
                    _batch.DrawString(_font, line, new Vector2(20, y), Color.OrangeRed,
                        0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                    y += _font.LineSpacing * Scale;
                }
            }
            _batch.End();
        }

        private string Sanitize(string text)
        {
            StringBuilder sb = new();
            foreach (char c in text)
                sb.Append(_font.Characters.Contains(c) ? c : '?');
            return sb.ToString();
        }

        private IEnumerable<string> Wrap(string text, float maxWidth)
        {
            string line = "";
            foreach (string word in text.Split(' '))
            {
                string test = line.Length == 0 ? word : line + " " + word;
                if (line.Length > 0 && _font.MeasureString(test).X > maxWidth)
                {
                    yield return line;
                    line = word;
                }
                else
                {
                    line = test;
                }
            }
            yield return line;
        }
    }
}