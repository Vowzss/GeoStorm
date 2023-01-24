using System.Collections.Generic;
using System.Numerics;
using System;

using GeoStorm.Render;
using static Raylib_cs.Color;
using Raylib_cs;

namespace GeoStorm.Core
{
    class Bullet : Entity
    {
        public Weapon weapon;

        public Bullet(float friction, float acc, Color color)
            : base(friction, acc , color)
        {
        }
    }

    class SimpleBullet : Bullet
    {

        public SimpleBullet(Weapon _weapon, Vector2 _position, float _rotation, float _speed)
            : base(EntitiesStats.simpleBulletStat.friction, EntitiesStats.simpleBulletStat.speed*_speed, YELLOW)
        {
            weapon = _weapon;
            position = _position;
            rotation = _rotation;
            spriteData = SpritesData.bulletSprite;
            collisionRadius = EntitiesStats.simpleBulletStat.size;
            velocity = new Vector2(MathF.Cos(rotation), MathF.Sin(rotation)) * acceleration * GameInputs.deltaTime;
        }


        public override void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            velocity *= (1 - friction * GameInputs.deltaTime);

            if (velocity.X < 0.02f && velocity.X > -0.02f)
                velocity.X = 0;
            if (velocity.Y < 0.02f && velocity.Y > -0.02f)
                velocity.Y = 0;

            position += velocity * GameInputs.deltaTime;

            if (isColliding) { isDead = true; }

            Render.Renderer.DrawSprites(position, spriteData, 15, rotation, color);
        }
    }

    class MineBullet : Bullet
    {
        private float timer;

        public MineBullet(Weapon _weapon, Vector2 _position, float _rotation, float _speed, float _timer)
            : base(EntitiesStats.mineBulletStat.friction, EntitiesStats.mineBulletStat.speed*_speed, YELLOW)
        {
            weapon = _weapon;
            position = _position;
            rotation = _rotation;
            spriteData = SpritesData.bulletSprite;
            collisionRadius = EntitiesStats.mineBulletStat.size;
            timer = _timer;
            velocity = new Vector2(MathF.Cos(rotation), MathF.Sin(rotation)) * acceleration * GameInputs.deltaTime;
        }


        public override void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            velocity *= (1 - friction * GameInputs.deltaTime);

            if (velocity.X < 0.02f && velocity.X > -0.02f)
                velocity.X = 0;
            if (velocity.Y < 0.02f && velocity.Y > -0.02f)
                velocity.Y = 0;

            position += velocity * GameInputs.deltaTime;

            if (isColliding) { isDead = true; }

            Render.Renderer.DrawSprites(position, spriteData, 15, rotation, color);

            bool isEnemyClose = false;
            foreach (Enemy e in data.enemies)
            {
                if (Vector2.Distance(e.position, position) <= 50)
                {
                    isEnemyClose = true;
                    break;
                }

            }
            if (timer <= 0 || isEnemyClose)
            {
                events.Add(new Explosion(this));
                isDead = true;
            }
            else
            {
                timer -= GameInputs.deltaTime;
            }
        }
    }

    class RocketBullet : Bullet
    {
        Enemy enemyToFollow = null;

        public RocketBullet(Weapon _weapon, Vector2 _position, float _rotation, float _speed)
            : base(EntitiesStats.rocketBulletStat.friction, EntitiesStats.rocketBulletStat.speed*_speed, YELLOW)
        {
            weapon = _weapon;
            position = _position;
            rotation = _rotation;
            spriteData = SpritesData.bulletSprite;
            collisionRadius = EntitiesStats.rocketBulletStat.size;
            velocity = new Vector2(MathF.Cos(rotation), MathF.Sin(rotation)) * acceleration * 50 * GameInputs.deltaTime;
        }

        private void findNearestEnemy(EntitiesManager data)
        {
            float minDistance = 99999999;
            foreach (Enemy enemy in data.enemies)
            {
                if (Vector2.Distance(enemy.position, position) < minDistance)
                {
                    enemyToFollow = enemy;
                    minDistance = Vector2.Distance(enemy.position, position);
                }
            }
        }

        public override void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            if (enemyToFollow == null)
            {
                findNearestEnemy(data);
            }
            else if(enemyToFollow.isDead)
            {
                enemyToFollow = null;
            }
            if(enemyToFollow != null)
            {
                velocity += Vector2.Normalize(enemyToFollow.position - position) * acceleration * GameInputs.deltaTime;
            }
            velocity *= (1 - friction * GameInputs.deltaTime);
            position += velocity * GameInputs.deltaTime;
            Renderer.DrawSprites(position, spriteData, collisionRadius, MathF.Atan2(velocity.Y, velocity.X), color);
        }   
    }

    class Laser : Bullet
    {
        float lifeSpan;

        public Laser(Weapon _weapon, Vector2 _position, float _rotation, float _speed, float _lifeSpan)
            : base(1, _speed, RED)
        {
            weapon = _weapon;
            position = _position;
            rotation = _rotation;
            lifeSpan = _lifeSpan;
            velocity = new Vector2(MathF.Cos(rotation), MathF.Sin(rotation));
            //spriteData = SpritesData.bulletSprite;
            //collisionRadius = 10;
        }


        public override void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            Renderer.DrawLaser(this);
            if (lifeSpan > 0)
                lifeSpan -= GameInputs.deltaTime;
            else
                isDead = true;

        }
    }
}