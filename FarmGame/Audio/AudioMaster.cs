using System;
using System.Collections.Generic;
using System.IO;

using FarmGame.Core;

using ManagedBass;

using OpenTK.Mathematics;

namespace FarmGame.Audio
{
    public class AudioMaster : IComponent, IUpdatable
    {
        private static AudioMaster _instance = null;

        private static GameObject _goAudio;

        private HashSet<AudioSource> _playBuffer;

        private float _cutoffDistance = 9f;

        private IPosition _cameraPos;

        private AudioMaster()
        {
            if (_instance != null)
            {
                throw new UnauthorizedAccessException("No second object is allowed to be created!");
            }

            Bass.Init();
            _playBuffer = new HashSet<AudioSource>();
            _cameraPos = _goAudio.GetComponent<IPosition>();
        }

        public static AudioMaster Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AudioMaster();
                }

                return _instance;
            }
        }

        public static void Init(GameObject go)
        {
            _goAudio = go;
        }

        public void Update(float elapsedTime)
        {
            Listen();
        }

        public void Listen()
        {
            foreach (AudioSource src in _playBuffer)
            {
                float distance = EukledDistance(_cameraPos.Position, src.Location);
                if (distance > _cutoffDistance || distance == 0f)
                {
                    Bass.ChannelSetAttribute(src.Handle, ChannelAttribute.Volume, 0);
                }
                else if (distance < 1f)
                {
                    Bass.ChannelSetAttribute(src.Handle, ChannelAttribute.Volume, 1);
                }
                else
                {
                    Bass.ChannelSetAttribute(src.Handle, ChannelAttribute.Volume, 1f - (distance / _cutoffDistance));
                }
            }
        }

        public AudioSource GetStepsHanlde(IMoving pos)
        {
            string path = System.Reflection.Assembly.GetEntryAssembly().Location;
            string pathFolder = System.IO.Path.GetDirectoryName(path);

            AudioSource src = new AudioSource(Bass.CreateStream(pathFolder + "/Resources/Sounds/step.wav"), 0.5f, pos);
            _playBuffer.Add(src);
            return src;
        }

        public AudioSource GetPigSnortHanlde(IMoving pos)
        {
            string path = System.Reflection.Assembly.GetEntryAssembly().Location;
            string pathFolder = System.IO.Path.GetDirectoryName(path);
            AudioSource src = new AudioSource(Bass.CreateStream(pathFolder + "/Resources/Sounds/pig-snort.wav"), 1.6f, pos);
            _playBuffer.Add(src);
            return src;
        }

        private float EukledDistance(Vector2 a, Vector2 b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}