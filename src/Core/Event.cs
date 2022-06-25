namespace GeoStorm.Core
{
    class Event
    {

    }

    class EnemyKilled : Event
    {
        public Bullet bullet;
        public Enemy enemy;

        public EnemyKilled(Bullet _bullet, Enemy _enemy)
        {
            bullet = _bullet;
            enemy = _enemy;
        }
    }

    class PlayerKilled : Event
    {
        public Player player;
        public Enemy enemy;

        public PlayerKilled(Player _player, Enemy _enemy)
        {
            player = _player;
            enemy = _enemy;
        }
    }

    class Explosion : Event
    {
        public MineBullet mineBullet;
        public Explosion(MineBullet mine)
        {
            mineBullet = mine;
        }
    }

}