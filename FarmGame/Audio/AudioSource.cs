using FarmGame.Core;

using ManagedBass;

namespace FarmGame.Audio
{
    public class AudioSource : IComponent
    {
        private int _audioHandle;

        public AudioSource(int handle)
        {
            _audioHandle = handle;
        }

        public void Play(object sender, OnPlaySoundArgs e)
        {
            // var pos = new Vector3D(e.Position.X, e.Position.Y, 0);
            // var speed = new Vector3D(e.Speed.X, e.Speed.Y, 0);
            // Bass.ChannelSet3DPosition(_audioHandle, pos, null, speed);

            var temp = Bass.ChannelGetLength(_audioHandle) - Bass.ChannelGetPosition(_audioHandle);

            if (Bass.ChannelIsActive(_audioHandle) == PlaybackState.Playing)
            {
                if (temp == 0)
                {
                    Bass.ChannelStop(_audioHandle);
                }
            }
            else
            {
                Bass.ChannelPlay(_audioHandle);
            }
        }
    }
}