using System;
using System.Collections.Generic;
using GeoStorm.Core;
using System.Numerics;
using GeoStorm.MathExtend;
using Raylib_cs;

namespace GeoStorm.Render
{
    enum ParticleShape { Circle, Line, Square }
    class Particle
    {
        public Vector2 position;
        public float rotation;
        public float size;
        public Raylib_cs.Color color;
        public Vector2 velocity;
        public float acc;
        public float friction;
        public ParticleShape shape;

        public Particle(Vector2 pos, float rot, float _size, Raylib_cs.Color col, Vector2 vel, float _acc, float fric, ParticleShape _shape)
        {
            position = pos;
            rotation = rot;
            size = _size;
            color = col;
            velocity = vel;
            acc = _acc;
            friction = fric;
            velocity *= acc;
            shape = _shape;
        }
    }


    class ParticlesSystem : IGameEventListener, ISystem
    {
        private List<Particle> particles = new List<Particle>();

        public void HandleEvent(List<Event> events, EntitiesManager data)
        {
            foreach (Event e in events)
            {
                if (e is EnemyKilled killed)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        float rotation = (float)Maths.rand.Next(360) * MathF.PI / 180.0f;

                        particles.Add(new Particle(killed.enemy.position, rotation, 20, killed.enemy.color, new Vector2(MathF.Cos(rotation), MathF.Sin(rotation)), Maths.rand.Next(50, 250), 1.5f, ParticleShape.Line));
                        //Console.WriteLine(killed.enemy.position);
                    }
                }

                if (e is Explosion explosion)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        float rotation = (float)Maths.rand.Next(360) * MathF.PI / 180.0f;
                        float dir = (float)Maths.rand.Next(360) * MathF.PI / 180.0f;


                        particles.Add(new Particle(explosion.mineBullet.position, rotation, 20, Maths.ColorLerp(i / 100.0f, Color.YELLOW, Color.RED)
                            , new Vector2(MathF.Cos(dir), MathF.Sin(dir)), Maths.rand.Next(180, 550), 4.4f, ParticleShape.Circle));
                        //Console.WriteLine(killed.enemy.position);
                    }

                }

            }
        }
        public void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            Renderer.DrawParticles(particles);

            if(Vector2.Distance(new Vector2(), inputs.moveAxis) > 0.2f)
            {
                
                float rot = (float)Maths.rand.Next((int)(data.player.rotation * 180.0f / MathF.PI) - 100, (int)(data.player.rotation * 180.0f / MathF.PI) + 100) * MathF.PI / 180.0f;
                float colorLerp = (float)Maths.rand.Next(10) / 10.0f;
                particles.Add(new Particle(data.player.position - Vector2.Normalize(data.player.velocity) * 15, rot, 12, Maths.ColorLerp(colorLerp, Color.ORANGE, Color.RED),
                    new Vector2(MathF.Cos(rot), MathF.Sin(rot)), Maths.rand.Next(30, 150), 3.0f, ParticleShape.Square));
                
            }
        }
    }
}

