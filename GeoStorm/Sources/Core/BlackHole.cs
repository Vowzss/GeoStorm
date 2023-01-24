using System.Collections.Generic;
using System.Numerics;
using static Raylib_cs.Color;
using GeoStorm.Render;


namespace GeoStorm.Core
{
    class BlackHole : Entity
    {
        public BlackHole(Vector2 _position)
            : base(0, 0, BLACK)
        {
            position = _position;
        }

        public sealed override void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            base.Update(inputs, data, events);
            Renderer.DrawBlackHole(position);
        }

        /*public void Draw()
        {

        }*/
    }
}
