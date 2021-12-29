#if Linux
using System.Text.RegularExpressions;
using System.IO;
using OpenTK.Mathematics;

using FarmGame.Core;

using Gamepad;

namespace FarmGame.Model.Input
{
    public class GamepadInputLinux
    {
        private GamepadController _gamePad;

        private Regex _validJoystickPath = new Regex(@"/dev/input/js[0-9]+");

        private Vector2 _movement;

        private IMoving _player;

        public bool Success { get; private set; }

        public GamepadInputLinux(GameObject player)
        {
            _player = player.GetComponent<IMoving>();
            string[] inputFiles = Directory.GetFiles("/dev/input/");
            MatchCollection mc = _validJoystickPath.Matches(string.Join("\n", inputFiles));

        
            if (mc.Count > 0)
            {
                Success = true;
                _gamePad = new GamepadController(mc[0].Value);
                _gamePad.AxisChanged += (object sender, AxisEventArgs e) =>
                {
                    System.Console.WriteLine("Axis: " + e.Axis  + " Value: " + e.Value);
                    _movement = new Vector2();
                    if (e.Axis == 1)
                    {
                        _movement.Y = e.Value / 32767;
                    } 

                    if (e.Axis == 0)
                    {
                        _movement.X = e.Value / 32767;
                    }
                };
            } 
            
            else
            {
                Success = false;
            } 
        }

        public void Update(float elapsedTime)
        {
            if (_movement.X == 0 && _movement.Y == 0)
            {
                return;
            }
            _player.MovementVector = _movement.Normalized() * elapsedTime * _player.MovementSpeed;
        }
    }
}
#endif
