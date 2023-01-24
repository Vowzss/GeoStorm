using System;
using GeoStorm.Core;
using Raylib_cs;
using static Raylib_cs.Raylib;
using GeoStorm.Interfaces;
using GeoStorm.Render;


namespace ImGuiDemo
{
    class Program
    {
        public static bool applicationToClose = false;
        static unsafe void Main(string[] args)
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            SetTraceLogCallback(&Logging.LogConsole);
            SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
            InitWindow(GetMonitorWidth(0), GetMonitorHeight(0), "GeoStorm");
            InitAudioDevice();
            Console.WriteLine(GetMonitorWidth(0));
            Console.WriteLine(GetMonitorHeight(0));
            //ToggleFullscreen();
            SetTargetFPS(60);

            ImguiController controller = new ImguiController();

            SceneManager currentScene = new SceneManager();
            SceneManager.LoadScene(new Menu());
            
            GameInputs inputs = new GameInputs();

            controller.Load(GetMonitorWidth(0), GetMonitorHeight(0));

            //RenderTexture2D target = LoadRenderTexture(GetMonitorWidth(0), GetMonitorHeight(0));


            string s = "../../../resources/bloom.fs";
            Shader shaders = LoadShader(null, s);
            //--------------------------------------------------------------------------------------
            Renderer.InitTexture();

            // Main game loop
            while (!WindowShouldClose() && !applicationToClose)
            {
                if (SceneManager.sceneActivated)
                { 
                    currentScene = SceneManager.currentScene;
                    currentScene.Start();
                    SceneManager.sceneActivated = false;
                }

                float dt = GetFrameTime();
                controller.Update(dt);

                inputs.UpdateInputs(KeyboardKey.KEY_S, KeyboardKey.KEY_W, KeyboardKey.KEY_A, KeyboardKey.KEY_D, MouseButton.MOUSE_BUTTON_LEFT);
                currentScene.Update(inputs);


                BeginDrawing();
                    Renderer.ApplyShader(shaders);
                    controller.Draw();
                EndDrawing();
            }
            // De-Initialization
            //--------------------------------------------------------------------------------------
            controller.Dispose();
            CloseAudioDevice();
            CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}