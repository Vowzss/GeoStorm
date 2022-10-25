using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using GeoStorm.Interfaces;
using GeoStorm.Render;

namespace GeoStorm.Core
{
    class PauseMode
    {
        Button resumeButton;
        Button restartButton;
        Button menuButton;
        public bool isGamePaused = false;

        public void Start()
        {
            isGamePaused = false;

            resumeButton = new Button(
                new Vector2(GetMonitorWidth(0) / 2 - 400, GetMonitorHeight(0) / 2 - 90),
                new Vector2(350, 50),
                130,
                "RESUME",
                WHITE,
                GRAY,
                () => isGamePaused = false
            );
            menuButton = new Button(
                new Vector2(GetMonitorWidth(0) / 2 + 50, GetMonitorHeight(0) / 2 - 90),
                new Vector2(350, 50),
                145,
                "MENU",
                WHITE,
                GRAY,
                () => SceneManager.LoadScene(new Menu())
            );
            restartButton = new Button(
                new Vector2(GetMonitorWidth(0) / 2 - 175, GetMonitorHeight(0) / 2),
                new Vector2(350, 50),
                125,
                "RESTART",
                WHITE,
                GRAY,
                () => SceneManager.LoadScene(new Game())
            );
        }
        public void Update(GameInputs inputs)
        {
            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                isGamePaused = !isGamePaused;
            }

            if (isGamePaused)
            {
                Game.timeScale = 0;
                Vector2 pos = new Vector2(GetMonitorWidth(0) / 2 - 450, GetMonitorHeight(0) / 2 - 225), size = new Vector2(900, 200);

                Renderer.DrawBackgroundHUD(pos, size, 3);
                Renderer.DrawTextHUD(3);

                resumeButton.Update(inputs);
                restartButton.Update(inputs);
                menuButton.Update(inputs);
            }
            else
                Game.timeScale = 1;
        }
    }
}
