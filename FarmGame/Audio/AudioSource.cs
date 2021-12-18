using FarmGame.Core;

using ManagedBass;

namespace FarmGame.Audio
{
    // TODO find out whz sometime two footseps are being played
    public class AudioSource : IComponent, IUpdatable
    {
        private int _audioHandle;

        // TODO find way to make it dynamic depending on file
        private  float _length = 0.5f;

        private float _pos = 0f;

        private bool _playing;

        public AudioSource(int handle)
        {
            _audioHandle = handle;
            _playing = true;
        }

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
            // var pos = new Vector3D(e.Position.X, e.Position.Y, 0);
            // var speed = new Vector3D(e.Speed.X, e.Speed.Y, 0);
            // Bass.ChannelSet3DPosition(_audioHandle, pos, null, speed);
            if (!_playing)
            {
                Bass.ChannelPlay(_audioHandle);
                _playing = true;
            }
        }
    }
}