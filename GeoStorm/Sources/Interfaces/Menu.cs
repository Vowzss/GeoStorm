using System.Numerics;
using GeoStorm.Core;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using ImGuiDemo;
using GeoStorm.Render;

namespace GeoStorm.Interfaces
{
    class Menu : SceneManager
    {
        EntitiesManager entities = new EntitiesManager();

        Button playButton;
        Button quitButton;
        Button rulesButton;
        Button returnButton;

        bool isRulesMenu = false;

        public Menu()
        {
            playButton = new Button(
                new Vector2(GetMonitorWidth(0)/2 - 400, GetMonitorHeight(0)/2 - 200),
                new Vector2(350, 50),
                145,
                "PLAY",
                WHITE,
                GRAY,
                () =>  SceneManager.LoadScene(new Game()) 
            );
            quitButton = new Button(
                new Vector2(GetMonitorWidth(0)/2+50, GetMonitorHeight(0)/2 - 200),
                new Vector2(350, 50),
                145,
                "QUIT",
                WHITE,
                GRAY,
                () => Program.applicationToClose = true
            );
            rulesButton = new Button(
                new Vector2(GetMonitorWidth(0)/2 - 175, GetMonitorHeight(0)/2 - 100),
                new Vector2(350, 50),
                140,
                "RULES",
                WHITE,
                GRAY,
                () => {
                    isRulesMenu = true;
                }
            );
            returnButton = new Button(
                new Vector2(GetMonitorWidth(0) / 2 - 175, GetMonitorHeight(0) / 2 + 30),
                new Vector2(350, 50),
                130,
                "RETURN",
                WHITE,
                GRAY,
                () => {
                    isRulesMenu = false;
                }
            );
        }

        public override void Update(GameInputs inputs)
        {
            Vector2 pos = new Vector2(GetMonitorWidth(0) / 2 - 450, GetMonitorHeight(0) / 2 - 225), size = new Vector2(900, 200);

            if (isRulesMenu)
            {
                Renderer.DrawBackgroundHUD(pos, size, 1);
                Renderer.DrawTextHUD(1);
                
                returnButton.Update(inputs);
            }
            else
            {
                Renderer.DrawBackgroundHUD(pos, size, 2);
                Renderer.DrawTextHUD(0);

                playButton.Update(inputs);
                quitButton.Update(inputs);
                rulesButton.Update(inputs);
            }
        }
    }
}
