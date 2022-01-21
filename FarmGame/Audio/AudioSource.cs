using FarmGame.Core;

using ManagedBass;

using OpenTK.Mathematics;

namespace FarmGame.Audio
{
    public class AudioSource : IComponent, IUpdatable, IAudioSource
    {
        private float _length;

        private float _trackPos = 0f;

        private bool _playing;

        private IPosition _postion;

        public AudioSource(int handle, float duration, IPosition pos)
        {
            Handle = handle;
            _length = duration;
            _playing = true;
            _postion = pos;
        }

        public int Handle { get; private set; }

        public Vector2 Location { get; private set; }

        public void Update(float elapsedTime)
        {
            _trackPos += elapsedTime;
            if (_trackPos > _length)
            {
                _trackPos = 0f;
                _playing = false;
            }

            Play();
        }

        private void Play()
        {
            if (Location != _postion.Position)
            {
                Location = _postion.Position;
                if (!_playing)
                {
                    Bass.ChannelPlay(Handle);
                    _playing = true;
                }
            }
        }
    }
}