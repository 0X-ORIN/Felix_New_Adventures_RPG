using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace FNA_RPG.Core
{
    public static class AudioManager
	{
		private static Song _currentSong;
        private static float _musicVolume = 0.5f;

        public static float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = MathHelper.Clamp(value, 0f, 1f);
                MediaPlayer.Volume = _musicVolume;
            }
        }
		public static void PlayMusic(Song song)
		{   
            if (song == _currentSong)
            {
                return;
            }
            _currentSong = song;
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(song);
		}

		public static void Stop()
		{
			MediaPlayer.Stop();
            _currentSong = null;
		}
	}
}