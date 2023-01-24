using System.Collections.Generic;
using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Color;
using GeoStorm.MathExtend;

namespace GeoStorm.Core
{
    class Enemy : Entity
    {
        public float spawnTime = 0;
        protected int maxLife, currentLife;

        public int scoreGiven;
        public int moneyGiven;

        public Enemy(Vector2 mposition, Color color, float spawnT, float acc, float friction)
            :base(friction, acc, color)
        {
            position = mposition;
            rotation = 5;
            spawnTime = spawnT;
        }

        public sealed override void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            base.Update(inputs, data, events);

            if (spawnTime <= 0)
            {
                DoUpdate(inputs, data, events);
                Render.Renderer.DrawSprites(position, spriteData, collisionRadius, rotation, color, $"{currentLife}");
            }
            else
            {
                spawnTime -= GameInputs.deltaTime;
                Render.Renderer.DrawSprites(position, spriteData, collisionRadius, rotation, new Color(color.r, color.g, color.b, (250 - (int)(spawnTime * 126))), $"{currentLife}");
            }
               
        }

        public virtual void TakeDamage(Bullet bullet, List<Event> events)
        {
            currentLife -= bullet.weapon.damage;
            bullet.isDead = true;
            if (currentLife <= 0)
            {
                currentLife = 0;
                events.Add(new EnemyKilled(bullet, this));
            }
        }

        protected virtual void DoUpdate(GameInputs inputs, EntitiesManager data, List<Event> events) { }

        public virtual void OnKilled(Bullet bullet) { isDead = true; }
        
    }

    class Grunt : Enemy
    {
        public Grunt(Vector2 pos, float spawnT) : base(pos, PURPLE, spawnT, EntitiesStats.gruntEnemyStats.speed, EntitiesStats.gruntEnemyStats.friction)
        {
            collisionRadius = EntitiesStats.gruntEnemyStats.size;
            maxLife = EntitiesStats.gruntEnemyStats.life;
            currentLife = EntitiesStats.gruntEnemyStats.life;
            spriteData = SpritesData.gruntSprite;
            scoreGiven = EntitiesStats.gruntEnemyStats.scoreGiven;
            moneyGiven = EntitiesStats.gruntEnemyStats.moneyGiven;
            float rot = (float)Maths.rand.Next(360) * MathF.PI / 180.0f;
            velocity = new Vector2(MathF.Cos(rot), MathF.Sin(rot)) * acceleration;
        }

        protected override void DoUpdate(GameInputs inputs, EntitiesManager data, List<Event> events)
        {

            foreach (Enemy enemy in data.enemies)
            {
                if (enemy == this)
                    continue;
                float dist = Vector2.Distance(position, enemy.position);
                if (dist <= 50)
                {
                    velocity += Vector2.Normalize(position - enemy.position) * ((55 - dist) / 15.0f) * acceleration * GameInputs.deltaTime;
                }
            }
            rotation += 0.08f * Game.timeScale;
            //velocity += Vector2.Normalize(data.player.position - position) * acceleration * GameInputs.deltaTime;
            velocity *= (1 - friction * GameInputs.deltaTime);
            position += velocity * GameInputs.deltaTime;
        }

        public override void OnKilled(Bullet bullet)
        {
            base.OnKilled(bullet);
            Console.WriteLine("Grunt Killed");
        }
    }

    class Shuriken : Enemy
    {
        float timer = 4;

        public Shuriken(Vector2 pos, float spawnT) : base(pos, PINK, spawnT, EntitiesStats.shurikentEnemyStats.speed, EntitiesStats.shurikentEnemyStats.friction)
        {
            collisionRadius = EntitiesStats.shurikentEnemyStats.size;
            maxLife = EntitiesStats.shurikentEnemyStats.life;
            currentLife = EntitiesStats.shurikentEnemyStats.life;
            spriteData = SpritesData.shurikenSprite;
            scoreGiven = EntitiesStats.shurikentEnemyStats.scoreGiven;
            moneyGiven = EntitiesStats.shurikentEnemyStats.moneyGiven;
        }

        protected override void DoUpdate(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            if (timer > 4)
                rotation += (timer - 4) * GameInputs.deltaTime * 4 * Game.timeScale;
            else
                rotation += (4 - timer) * GameInputs.deltaTime * 3 * Game.timeScale;

            if(timer <= 0)
            {
                velocity += Vector2.Normalize(data.player.position - position) * acceleration * GameInputs.deltaTime;
                timer = 7;
            }else
            {
                timer -= GameInputs.deltaTime;
            }
            velocity *= (1 - friction * GameInputs.deltaTime);
            position += velocity * GameInputs.deltaTime;
        }

        public override void OnKilled(Bullet bullet)
        {
            base.OnKilled(bullet);
            Console.WriteLine("Shuriken Killed");
        }
    }

    class Star : Enemy
    {
        public Star(Vector2 pos, float spawnT) : base(pos, RED, spawnT, EntitiesStats.starEnemyStats.speed, EntitiesStats.starEnemyStats.friction)
        {
            collisionRadius = EntitiesStats.starEnemyStats.size;
            maxLife = EntitiesStats.starEnemyStats.life;
            currentLife = EntitiesStats.starEnemyStats.life;
            spriteData = SpritesData.starSprite;
            scoreGiven = EntitiesStats.starEnemyStats.scoreGiven;
            moneyGiven = EntitiesStats.starEnemyStats.moneyGiven;
        }

        protected override void DoUpdate(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            foreach (Enemy enemy in data.enemies)
            {
                if (enemy == this)
                    continue;
                float dist = Vector2.Distance(position, enemy.position);
                if (dist <= 50)
                {
                    velocity += Vector2.Normalize(position - enemy.position) * ((55 - dist) / 15.0f) * acceleration * GameInputs.deltaTime;
                }
            }
            rotation += 0.08f * Game.timeScale;
            velocity += Vector2.Normalize(data.player.position - position) * acceleration * GameInputs.deltaTime;
            velocity *= (1 - friction * GameInputs.deltaTime);
            position += velocity * GameInputs.deltaTime;
        }

        public override void OnKilled(Bullet bullet)
        {
            base.OnKilled(bullet);
            Console.WriteLine("Star Killed");
        }
    
    }
}