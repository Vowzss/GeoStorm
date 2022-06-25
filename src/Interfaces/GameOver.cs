using GeoStorm.Core;
using System.Numerics;

using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using GeoStorm.Interfaces;
using GeoStorm.Render;

namespace GeoStorm.Scenes
{
    class GameOver : SceneManager
    {
        Button restartButton;
        private int score;

        public GameOver(int _score)
        {
            score = _score;

             restartButton = new Button(
                new Vector2(GetMonitorWidth(0) / 2 - 175, GetMonitorHeight(0) / 2 + 30),
                new Vector2(350, 50),
                130,
                "RETURN",
                WHITE,
                GRAY,
                () =>  SceneManager.LoadScene(new Game()) 
            ); 
        }

        public override void Update(GameInputs inputs)
        {
            Vector2 pos = new Vector2(GetMonitorWidth(0) / 2 - 450, GetMonitorHeight(0) / 2 - 225), size = new Vector2(900, 200);

            Renderer.DrawBackgroundHUD(pos, size, 3);
            Renderer.DrawTextHUD(2);

            restartButton.Update(inputs);
        }
    }
}