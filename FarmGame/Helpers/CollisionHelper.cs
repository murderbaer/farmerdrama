using System.Collections.Generic;
using FarmGame.Core;
using OpenTK.Mathematics;

namespace FarmGame.Helpers
{
    public static class CollisionHelper
    {
        public static bool BoxCollide(Box2 a, Box2 b)
        {
            bool noXoverlap = a.Max.X <= b.Min.X || a.Min.X >= b.Max.X;
            bool noYoverlap = a.Max.Y <= b.Min.Y || a.Min.Y >= b.Max.Y;
            return !(noXoverlap || noYoverlap);
        }

        public static Box2 GetCollisionBox(float positionX, float positionY, float size = 1, bool centered = false)
        {
            if (centered)
            {
                positionX -= size / 2;
                positionY -= size / 2;
            }

            return new Box2(positionX, positionY, positionX + size, positionY + size);
        }

        public static Box2 GetCollisionBox(Vector2 position, float size = 1, bool centered = false)
        {
            return GetCollisionBox(position.X, position.Y, size: size, centered: centered);
        }

        public static List<Box2> GetTileCollisionBoxesAround(Vector2 position, IReadOnlyGrid colGrid)
        {
            int positionXFloor = (int)MathHelper.Floor(position.X) - 1;
            int positionYFloor = (int)MathHelper.Floor(position.Y) - 1;
            int positionXCeil = (int)MathHelper.Ceiling(position.X);
            int positionYCeil = (int)MathHelper.Ceiling(position.Y);
            List<Box2> collisionBoxes = new List<Box2>();

            // Optional performance boost
            if (positionXFloor > colGrid.Column ||
                positionYFloor > colGrid.Row ||
                positionXCeil < 0 ||
                positionYCeil < 0)
            {
                return collisionBoxes;
            }

            for (int cellPosX = positionXFloor; cellPosX <= positionXCeil; cellPosX++)
            {
                for (int cellPosY = positionYFloor; cellPosY <= positionYCeil; cellPosY++)
                {
                    if (cellPosX >= 0 &&
                        cellPosX < colGrid.Column &&
                        cellPosY >= 0 &&
                        cellPosY < colGrid.Row &&
                        colGrid[cellPosX, cellPosY].HasCollision)
                    {
                        collisionBoxes.Add(GetCollisionBox(cellPosX, cellPosY));
                    }
                }
            }

            return collisionBoxes;
        }

        public static bool BoxCollide(Box2 box, List<Box2> boxes)
        {
            foreach (Box2 b in boxes)
            {
                if (BoxCollide(box, b))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Collides(ICollidable a, ICollidable b)
        {
            return (a.Position - b.Position).LengthSquared < (a.CollisionRadius + b.CollisionRadius) * (a.CollisionRadius + b.CollisionRadius);
        }

        public static Vector2 GetCollisionResponse(ICollidable a, ICollidable b)
        {
            Vector2 aToB = b.Position - a.Position;
            float distance = aToB.Length;
            float overlap = a.CollisionRadius + b.CollisionRadius - distance;
            if (overlap > 0 && distance > 0)
            {
                return aToB * (overlap / distance);
            }
            else
            {
                return Vector2.Zero;
            }
        }
    }
}