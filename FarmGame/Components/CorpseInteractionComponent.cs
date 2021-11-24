#if false // to be removed
using OpenTK.Windowing.Common;

namespace FarmGame
{
    public class CorpseInteractionComponent
    {
        public void OnKeyPress(KeyboardKeyEventArgs args, ref World world)
        {
            if (!world.Corpse.IsPlaced)
            {
                world.Corpse.IsPlaced = true;
                return;
            }

            var playerPos = world.Player.Position;
            var corpsePos = world.Corpse.Position;
            var distance = (corpsePos - playerPos).Length;
            if (distance < 1.0f)
            {
                world.Corpse.IsPlaced = false;
            }
        }
    }
}
#endif