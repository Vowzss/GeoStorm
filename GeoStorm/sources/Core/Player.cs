using System;
using System.Collections.Generic;
using System.Numerics;
using static GeoStorm.MathExtend.Maths;
using GeoStorm.Render;
using static Raylib_cs.Color;

namespace GeoStorm.Core
{
    class Player : Entity
    {
        public Weapon weapon;

        public int currentLife;
        public int maxLife;
        public static int score;
        public int money;

        public int enemyKilled;

        public bool isInvicible = false;

        public Player()
            : base(EntitiesStats.playerStats.friction, EntitiesStats.playerStats.speed, WHITE)
        {
            position = new Vector2(500, 500);
            spriteData = SpritesData.playerSprite;
            weapon = new Weapon(EntitiesStats.pistolWeaponStat);
            weapon.player = this;
            collisionRadius = EntitiesStats.playerStats.size;
            maxLife = EntitiesStats.starEnemyStats.life;
            currentLife = maxLife;
        }

        public sealed override void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            base.Update(inputs, data, events);

            Move(inputs);

            if(Game.timeScale != 0)
                weapon.Update(inputs, data, events);

            if (velocity.X != 0 || velocity.Y != 0)
            {
                if (MathF.Atan2(velocity.Y, velocity.X) - rotation > MathF.PI)
                    rotation += MathF.PI * 2;
                if (MathF.Atan2(velocity.Y, velocity.X) - rotation < -MathF.PI)
                    rotation -= MathF.PI * 2;

                rotation = Lerp(0.9f, MathF.Atan2(velocity.Y, velocity.X), rotation);
            }

            Renderer.DrawSprites(position, spriteData, 20f, rotation, color);
            
            if(currentLife <= 0)
            {
                isDead = true;
            }
        }

        public void TakeDamage(Enemy enemy, List<Event> events)
        {
            if (!isInvicible)
                currentLife -= 1;

            events.Add(new EnemyKilled(null, enemy));

            if (currentLife <= 0)
            {
                currentLife = 0;
                events.Add(new PlayerKilled(this, enemy));
            }
        }

        public static void Invicible(Player player)
        {
            player.currentLife = 50000000;
        }

        private void Move(GameInputs inputs)
        {
            velocity += inputs.moveAxis * acceleration * GameInputs.deltaTime;
            velocity *= (1 - friction * GameInputs.deltaTime);

            if (velocity.X < 0.02f && velocity.X > -0.02f)
                velocity.X = 0;
            if (velocity.Y < 0.02f && velocity.Y > -0.02f)
                velocity.Y = 0;

            position += velocity * GameInputs.deltaTime;

            if (velocity.X != 0 || velocity.Y != 0)
            {
                if (MathF.Atan2(velocity.Y, velocity.X) - rotation > MathF.PI)
                    rotation += MathF.PI * 2;
                if (MathF.Atan2(velocity.Y, velocity.X) - rotation < -MathF.PI)
                    rotation -= MathF.PI * 2;
                rotation = Lerp(0.8f, MathF.Atan2(velocity.Y, velocity.X), rotation);
            }
        }
    }
}