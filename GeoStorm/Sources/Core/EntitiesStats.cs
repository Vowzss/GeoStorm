namespace GeoStorm.Core
{
    struct BulletStats
    {
        public int size;
        public float speed;
        public float friction;
        public float explosionRadius;

        public BulletStats(int _size, float _speed, float _friction, float _explosionRadius)
        {
            size = _size;
            speed = _speed;
            friction = _friction;
            explosionRadius = _explosionRadius;
        }
    }

    struct PlayerStats
    {
        public int size;
        public float speed;
        public float friction;
        public int life;

        public PlayerStats(int _size, float _speed, float _friction, int _life)
        {
            size = _size;
            speed = _speed;
            friction = _friction;
            life = _life;
        }
    }

    struct EnemyStats
    {
        public int size;
        public float speed;
        public float friction;
        public int life;
        public int scoreGiven;
        public int moneyGiven;

        public EnemyStats(int _size, float _speed, float _friction, int _life, int _scoreGiven, int _moneyGiven)
        {
            size = _size;
            speed = _speed;
            friction = _friction;
            life = _life;
            scoreGiven = _scoreGiven;
            moneyGiven = _moneyGiven;
        }
    }

    struct WeaponStats
    {
        public int damage;
        public int bulletPerShoot;
        public BulletType bulletType;
        public float bulletSpeed;
        public float attackSpeed;

        public WeaponStats(int _damage, int _bulletPerShoot, BulletType _bulletType, float _bulletSpeed, float _attackSpeed)
        {
            damage = _damage;
            bulletPerShoot = _bulletPerShoot;
            bulletType = _bulletType;
            bulletSpeed = _bulletSpeed;
            attackSpeed = _attackSpeed;
        }
    }

    static class EntitiesStats
    {
        public static BulletStats simpleBulletStat = new BulletStats(15, 500, 0.1f, 0);
        public static BulletStats mineBulletStat = new BulletStats(15, 500, 1, 120);
        public static BulletStats laserBulletStat = new BulletStats(1, 1, 1, 1);
        public static BulletStats rocketBulletStat = new BulletStats(15, 3.5f, 0.1f, 10);

        public static PlayerStats playerStats = new PlayerStats(25, 30, 4, 5);

        public static EnemyStats gruntEnemyStats = new EnemyStats(22, 1, 0, 10, 50, 5);
        public static EnemyStats shurikentEnemyStats = new EnemyStats(22, 1000, 1, 15, 100, 25);
        public static EnemyStats starEnemyStats = new EnemyStats(22, 2.5f, 0.2f, 10, 200, 10);

        public static WeaponStats pistolWeaponStat = new WeaponStats(5, 1, BulletType.SIMPLE, 1, 5);
        public static WeaponStats catapulteWeaponStat = new WeaponStats(3, 6, BulletType.MINE, 1, 2);
        public static WeaponStats shotgunWeaponStat = new WeaponStats(3, 6, BulletType.SIMPLE, 1, 2);
        public static WeaponStats laserWeaponStat = new WeaponStats(100, 1, BulletType.LASER, 1, 120);
        public static WeaponStats launcherWeaponStat = new WeaponStats(10, 1, BulletType.ROCKET, 1, 1.5f);
    }
}
