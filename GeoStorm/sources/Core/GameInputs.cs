using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace GeoStorm.Core
{
    class GameInputs
    {
        public string keyPressed;
        public static float deltaTime { get; private set; }
        public static float time = 0;
        public bool isShoot { get; private set; }
        public static Vector2 screenSize { get; private set; }
        public Vector2 shootTarget { get; private set; }
        public Vector2 moveAxis { get; private set; }

        public GameInputs()
        {
            screenSize = new Vector2(GetScreenWidth(), GetScreenHeight());
        }
        public void UpdateInputs(KeyboardKey _forward, KeyboardKey _backward, KeyboardKey _left, KeyboardKey _right, MouseButton _isShoot)
        {
            deltaTime = GetFrameTime() * Game.timeScale;
            time += deltaTime;

            screenSize = new Vector2(GetScreenWidth(), GetScreenHeight());

            shootTarget = GetMousePosition();
            isShoot = IsMouseButtonDown(_isShoot);

            moveAxis = new Vector2(0, 0);
            Vector2 move = new Vector2();

            if (IsKeyDown(_forward))
            {
                keyPressed = _forward.ToString();
                move.Y = 1;
            }
            if (IsKeyDown(_backward))
            {
                keyPressed = _backward.ToString();
                move.Y = -1;
            }
            if (IsKeyDown(_left))
            {
                keyPressed = _left.ToString();
                move.X = -1;
            }
            if (IsKeyDown(_right))
            {
                keyPressed = _right.ToString();
                move.X = 1;
            }

            if(move == Vector2.Zero) { return; }
            moveAxis = Vector2.Normalize(move);
        }
    }
}