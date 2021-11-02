using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
namespace FarmGame {
    // internal class Control {
    //     private readonly Model _model;
    //     private readonly View _view;
    //     private bool _spaceDownLastFrame = false;
    //     public Control(Model model, View view)
    //     {
    //         _model = model;
    //         _view = view;
    //     }

    //     public void Update(float elapsedTime, KeyboardState keyboard)
    //     {
    //         MovePlayer(elapsedTime, keyboard);
    //         itemInteraction(keyboard);
    //     }

    //     private void MovePlayer(float elapsedTime, KeyboardState keyboard)
    //     {
    //         Vector2 moveDirection = new ();
    //         moveDirection.X = (keyboard.IsKeyDown(Keys.Right) ? 1 : 0) - (keyboard.IsKeyDown(Keys.Left) ? 1 : 0);
    //         moveDirection.Y = (keyboard.IsKeyDown(Keys.Down) ? 1 : 0) - (keyboard.IsKeyDown(Keys.Up) ? 1 : 0);
    //         if (moveDirection.X == 0 && moveDirection.Y == 0)
    //         {
    //             return;
    //         }

    //         moveDirection.Normalize();
    //         _model.Player.Position += moveDirection * elapsedTime * _model.Player.MovementSpeed;
    //     }


    //     private void itemInteraction(KeyboardState keyboard)
    //     {
    //         if (keyboard.IsKeyDown(Keys.Space) && !_spaceDownLastFrame)
    //         {
    //             int xPos = (int)_model.Player.Position.X;
    //             int yPos = (int)_model.Player.Position.Y;

    //             bool isCellEmpty = _model.Grid[xPos, yPos].PlacedItem.Type == ItemType.EMPTY;
    //             bool isHandEmpty = _model.Player.ItemInHand.Type == ItemType.EMPTY;

    //             bool cellHasSeed = _model.Grid[xPos, yPos].PlacedItem.Type == ItemType.SEED;
    //             bool cellisEmpty = _model.Grid[xPos, yPos].PlacedItem.Type == ItemType.EMPTY;
    //             bool handHasWaterbucket = _model.Player.ItemInHand.Type == ItemType.WATERBUCKET;   

    //             if (isHandEmpty && !isCellEmpty ) 
    //             {
    //                 _model.Player.TakeItem(_model.Grid[xPos, yPos].TakeItem());
    //             } 
    //             else if (!isHandEmpty && isCellEmpty ) 
    //             {
    //                 _model.Grid[xPos, yPos].PlaceItem(_model.Player.GiveItem()); 
    //             } 
    //             // water can be placed on seed, if seed is removed water will be preserved
    //             else if ((cellHasSeed || cellisEmpty) && handHasWaterbucket) 
    //             {
    //                 _model.Player.GiveItem(); // bucket is taken from player, but not placed as an object
    //                 _model.Grid[xPos, yPos].WaterTheCell(); // water counter goes up
    //             }

    //             _spaceDownLastFrame = true;
    //         }
    //         if (keyboard.IsKeyReleased(Keys.Space))
    //         {
    //             _spaceDownLastFrame = false;
    //         }
    //     }
    // }
}