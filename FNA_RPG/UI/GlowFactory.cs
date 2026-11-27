using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FNA_RPG.UI
{
    public static class GlowFactory
    {
		public static int PaddingFor(Texture2D source) => Math.Max(4, source.Width / 18);
        private static readonly Dictionary<Texture2D, Texture2D> _cache = new();
        
		private static float[] Blur(float[] data, int w, int h, int radius)
		{
			float[] temp = new float[data.Length];
			float[] result = new float[data.Length];
			float size = radius * 2 + 1;
			
			for (int y = 0; y < h; y++)
				for (int x = 0; x < w; x++)
				{
					float sum = 0;
					for (int k = -radius; k <= radius; k++)
						sum += data[y * w + Math.Clamp(x + k, 0, w - 1)];
					temp[y * w + x] = sum / size;
				}
			
			for (int y = 0; y < h; y++)
				for (int x = 0; x < w; x++)
				{
					float sum = 0;
					for (int k = -radius; k <= radius; k++)
						sum += temp[Math.Clamp(y + k, 0, h - 1) * w + x];
					result[y * w + x] = sum / size;
				}

			return result;
		}
        
		public static Texture2D Create(Texture2D source)
		{
            if (_cache.TryGetValue(source, out Texture2D cached))
				return cached;

			int padding = PaddingFor(source);
			int rTight = Math.Max(1, source.Width / 200);
			int rWide = Math.Max(2, source.Width / 54);

			int w = source.Width + padding * 2;
			int h = source.Height + padding * 2;

			Color[] pixels = new Color[source.Width * source.Height];
			source.GetData(pixels);

			float[] alpha = new float[w * h];
			for (int y = 0; y < source.Height; y++)
				for (int x = 0; x < source.Width; x++)
					alpha[(y + padding) * w + (x + padding)] = pixels[y * source.Width + x].A / 255f;

			float[] tight = Blur(Blur(alpha, w, h, rTight), w, h, rTight);
			float[] wide = Blur(Blur(alpha, w, h, rWide), w, h, rWide);

			Color[] result = new Color[w * h];
			Vector3 gold = new Vector3(1f, 0.65f, 0f);
			Vector3 light = new Vector3(1f, 0.95f, 0.7f);

			for (int i = 0; i < result.Length; i++)
			{
				float a = Math.Min(1f, wide[i] * 0.9f + tight[i] * 0.8f);
				float t = tight[i] / (tight[i] + wide[i] + 0.0001f);
				Vector3 c = Vector3.Lerp(gold, light, t);
				result[i] = new Color(c.X * a, c.Y * a, c.Z * a, a);
			}

			Texture2D glow = new Texture2D(source.GraphicsDevice, w, h);
			glow.SetData(result);

			_cache[source] = glow;
			return glow;
		}
    }
}