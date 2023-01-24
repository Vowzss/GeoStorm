using System.Collections.Generic;
using System.Numerics;
using System;
using GeoStorm.Render;

using static Raylib_cs.Color;

namespace GeoStorm.Core
{
    public enum BulletType { SIMPLE, MINE, LASER, ROCKET }

    public enum WeaponType { PISTOL, SHOTGUN, CATAPULTE, LASER, LAUNCHER }

    class Weapon
    {
        private readonly float frequency;
        private float timer;
        public float speed;
        public Player player;

        public BulletType bulletType;

        public int bulletperShoot = 1;

        public static bool canShoot;

        private Vector2 position;
        private float rotation;

        public Sprite spriteData;

        public int damage;

        public Weapon(WeaponStats stat)
        {
            frequency = stat.attackSpeed;
            speed = stat.bulletSpeed;
            damage = stat.damage;
            bulletperShoot = stat.bulletPerShoot;
            bulletType = stat.bulletType;
            spriteData = SpritesData.weaponSprite;
        }

        public void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            position = MathExtend.Maths.Rotate(new Vector2(player.collisionRadius * 1.2f, 0) + player.position, player.position, MathF.Atan2(inputs.shootTarget.Y - player.position.Y, inputs.shootTarget.X - player.position.X));
            rotation = MathF.Atan2(inputs.shootTarget.Y - player.position.Y, inputs.shootTarget.X - player.position.X);

            Renderer.DrawSprites(position, spriteData, 5, rotation, YELLOW);

            if (inputs.isShoot && timer <= 0 && canShoot)
            {   
                for(int i = 0; i < bulletperShoot; i++)
                {
                    switch (bulletType)
                    {
                        case BulletType.SIMPLE:
                            data.AddBulletDelayed(new SimpleBullet(this, position, rotation + (i - (bulletperShoot - 1) * 0.5f) * 0.1f, speed));
                            break;
                        case BulletType.MINE:
                            data.AddBulletDelayed(new MineBullet(this, position, rotation + (i - (bulletperShoot - 1) * 0.5f) * 0.1f, speed, 2));
                            break;
                        case BulletType.LASER:
                            data.AddBulletDelayed(new Laser(this, position, rotation + (i - (bulletperShoot - 1) * 0.5f) * 0.1f, speed, 1.0f / frequency));
                            break;
                        case BulletType.ROCKET:
                            data.AddBulletDelayed(new RocketBullet(this, position, rotation + (i - (bulletperShoot - 1) * 0.5f) * 0.1f, speed));
                            break;
                        default:
                            break;
                    }
                }
                timer = 1f / frequency;
            }
            if(timer > 0)
                timer -= GameInputs.deltaTime;
        }
    }
}        