using FarmGame.Core;

using ManagedBass;

using OpenTK.Mathematics;

namespace FarmGame.Audio
{
    // TODO find out whz sometime two footseps are being played
    public class AudioSource : IComponent, IUpdatable, IAudioSource
    {
        private float _length;

        private float _pos = 0f;

        private bool _playing;

        public AudioSource(int handle, float duration)
        {
            Handle = handle;
            _length = duration;
            _playing = true;
        }

        public int Handle { get; private set; }

        public Vector2 Location { get; private set; }

        public void Update(float elapsedTime)
        {
            _pos += elapsedTime;
            if (_pos > _length)
            {
                _pos = 0f;
                _playing = false;
            }
        }

        public void Play(object sender, OnPlaySoundArgs e)
        {
            Location = e.Position;
            if (!_playing)
            {
                Bass.ChannelPlay(Handle);
                _playing = true;
            }
        }
    }
}