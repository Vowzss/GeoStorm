using System.Collections.Generic;
using System.Numerics;

namespace GeoStorm.Core
{
    class CollisionSystem : ISystem, IGameEventListener
    {
        public void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            Player player = new Player();
            foreach (Entity entity in data.entities)
            {
                entity.isColliding = false;
                
                if (entity.position.X + entity.collisionRadius >= GameInputs.screenSize.X)
                {
                    entity.position.X -= entity.position.X + entity.collisionRadius - GameInputs.screenSize.X;
                    entity.isColliding = true;
                    entity.velocity.X *= -1;
                }
                if (entity.position.Y + entity.collisionRadius >= GameInputs.screenSize.Y)
                {
                    entity.position.Y -= entity.position.Y + entity.collisionRadius - GameInputs.screenSize.Y;
                    entity.isColliding = true;
                    entity.velocity.Y *= -1;
                }
                if (entity.position.X - entity.collisionRadius <= 0)
                {
                    entity.position.X -= entity.position.X - entity.collisionRadius;
                    entity.isColliding = true;
                    entity.velocity.X *= -1;
                }
                if (entity. position.Y - entity.collisionRadius <= 0)
                {
                    entity. position.Y -= entity. position.Y - entity.collisionRadius;
                    entity.isColliding = true;
                    entity.velocity.Y *= -1;
                }

                if(entity is Bullet bullet)
                {
                    if(bullet is Laser laser)
                    {
                        foreach (Enemy enemy in data.enemies)
                        {
                            Vector2 proj = MathExtend.Maths.Projection(enemy.position, laser.position,
                                MathExtend.Maths.Rotate(new Vector2(10000, 0) + laser.position, laser.position, laser.rotation));
                            if(Vector2.Distance(enemy.position, proj) <= enemy.collisionRadius + 4 && Vector2.Dot(enemy.position - laser.position,
                                MathExtend.Maths.Rotate(new Vector2(10000, 0) + laser.position, laser.position, laser.rotation)) >= 0)
                            {
                                enemy.TakeDamage(bullet, events);
                            }
                        }
                    }
                    else
                    {
                        foreach (Enemy enemy in data.enemies)
                        {
                            if(Vector2.Distance(bullet.position, enemy.position) <= bullet.collisionRadius + enemy.collisionRadius)
                            {
                                enemy.TakeDamage(bullet, events);
                            }
                        }
                    }
                }

                if(entity is Player p)
                {
                    foreach (Enemy enemy in data.enemies)
                    {
                        if (Vector2.Distance(p.position, enemy.position) <= p.collisionRadius + enemy.collisionRadius && enemy.spawnTime <= 0)
                        {
                            p.TakeDamage(enemy, events);
                        }
                    }
                }  
            }
        }

        public void HandleEvent(List<Event> events, EntitiesManager data)
        {
            foreach (Event e in events.ToArray())
            {
                if(e is Explosion exp)
                {
                    foreach (Enemy enemy in data.enemies)
                    {
                        if(enemy.spawnTime <= 0)
                        {
                            if(Vector2.Distance(enemy.position, exp.mineBullet.position) < EntitiesStats.mineBulletStat.explosionRadius)
                            {
                                enemy.TakeDamage(exp.mineBullet, events);
                                enemy.velocity += Vector2.Normalize(enemy.position - exp.mineBullet.position) * 500;
                                //Raylib_cs.Raylib.DrawCircleV(exp.mineBullet.position, 100, Raylib_cs.Color.ORANGE);
                            }
                        }
                    }
                }
            }
            
        }
        

    }
}
