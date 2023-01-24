using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace GeoStorm.Core
{
    class Entity : ISystem
    {
        public bool isDead = false;
        public Vector2 position;
        public float rotation;
        public float collisionRadius;

        public Vector2 velocity = new Vector2();
        public bool isColliding;

        public Color color;
        public readonly float friction;
        public readonly float acceleration;

        public Sprite spriteData;

        public Entity(float _friction, float _acc, Color _color)
        {
            friction = _friction;
            acceleration = _acc * 100;
            color = _color;
        }

        public virtual void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {

        }
    }
}