#if Linux
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OpenTK.Mathematics;

using FarmGame.Core;

using Gamepad;

namespace FarmGame.Model
{
    public class GamepadLinux : IComponent, IUpdatable
    {
        private GamepadController _gamePad;

        private const float _gamePadDeadZone = 0.3f;

        private Regex _validJoystickPath = new Regex(@"/dev/input/js[0-9]+");

        private Vector2 _movement;

        private IMoving _player;

        public GamepadLinux(GameObject player)
        {
            _player = player.GetComponent<IMoving>();
            string[] inputFiles = Directory.GetFiles("/dev/input/");
            MatchCollection mc = _validJoystickPath.Matches(string.Join("\n", inputFiles));
            _movement = new Vector2(0, 0);
            if (mc.Count > 0)
            {
               _gamePad = new GamepadController(mc[0].Value);
            }
        }

        public void Update(float elapsedTime)
        {
            System.Console.WriteLine(_gamePad.Axis[0]);
            _movement.X = _gamePad.Axis[0] / 32767f;
            _movement.Y = _gamePad.Axis[1] / 32767f;
            if (_movement.X < _gamePadDeadZone && _movement.X > -_gamePadDeadZone && 
                _movement.Y < _gamePadDeadZone && _movement.Y > -_gamePadDeadZone)
            {
                return;
            }

            _player.MovementVector = _movement.Normalized() * elapsedTime * _player.MovementSpeed;
        }
    }
}
#endif