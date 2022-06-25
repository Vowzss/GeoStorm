using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using GeoStorm.Core;

namespace GeoStorm.Interfaces
{
    class Button
    {
        public Vector2 position;
        public Vector2 size;
        public float offset;
        public string text;
        public Color colorOut;
        public Color colorIn;

        bool mouseInButtonBox = false;

        public delegate void ButtonOnClick();

        ButtonOnClick handler;

        public Button(Vector2 _position, Vector2 _size, float _offset, string _text, Color _colorOut, Color _colorIn, ButtonOnClick onClick)
        {
            position = _position;
            size = _size;
            offset = _offset;
            text = _text;
            colorOut = _colorOut;
            colorIn = _colorIn;

            handler = onClick;
        }

        public void Update(GameInputs inputs)
        {
            Render.Renderer.DrawButton(this);

            Vector2 mousePos = GetMousePosition();
            if (mousePos.X > position.X && mousePos.X < position.X + size.X &&
                mousePos.Y > position.Y && mousePos.Y < position.Y + size.Y)
            {
                mouseInButtonBox = true;
                Weapon.canShoot = false;
            }
            else
            {
                mouseInButtonBox = false;
            }

            if (mouseInButtonBox && IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_LEFT))
            {
                handler();
            }
        }
    }
}
