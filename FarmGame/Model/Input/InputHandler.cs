using System.Collections.Generic;
using FarmGame.Core;

namespace FarmGame.Model.Input
{
    public class InputHandler : IComponent, IUpdatable
    {
        
        public InputHandler()
        {
            #if Linux

            #endif
        }
        public void Update(float elapsedTime)
        {

        }
    }
}