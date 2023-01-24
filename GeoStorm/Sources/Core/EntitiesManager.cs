using System.Collections.Generic;

namespace GeoStorm.Core
{
    class EntitiesManager : ISystem
    {
        public Player player = new Player();

        public IEnumerable<Entity> entities { get; private set; }
        public IEnumerable<Enemy> enemies { get; private set; }
        public IEnumerable<Bullet> bullets { get; private set; }
        public IEnumerable<BlackHole> blackHoles { get; private set; }

        private List<Enemy> enemiesAdded = new List<Enemy>();
        private List<Bullet> bulletAdded = new List<Bullet>();
        private List<BlackHole> blackHoleAdded = new List<BlackHole>();

        public void AddEnemyDelayed(Enemy enemy)
        {
            enemiesAdded.Add(enemy);
        }

        public void AddBulletDelayed(Bullet bullet)
        {
            bulletAdded.Add(bullet);
        }

        public void AddBlackHoleDelayed(BlackHole blackHole)
        {
            blackHoleAdded.Add(blackHole);
        }

        public void Synchronize()
        {
            enemiesAdded.RemoveAll(e => e.isDead);
            bulletAdded.RemoveAll(b => b.isDead);
            blackHoleAdded.RemoveAll(bh => bh.isDead);
            enemies = enemiesAdded;
            bullets = bulletAdded;
            blackHoles = blackHoleAdded;

            List<Entity> entitiesAdded = new List<Entity>();

            entitiesAdded.Add(player);

            entitiesAdded.AddRange(enemies);
            entitiesAdded.AddRange(bullets);
            entitiesAdded.AddRange(blackHoles);

            entities = entitiesAdded;
        }
    }
}
