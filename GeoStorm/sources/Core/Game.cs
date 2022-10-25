using System.Collections.Generic;
using GeoStorm.Render;
using GeoStorm.Scenes;

namespace GeoStorm.Core
{
    class Game : SceneManager, IGameEventListener
    {
        static public float timeScale = 1;

        private List<IGameEventListener> eventListeners = new List<IGameEventListener>();
        List<ISystem> allSystems = new List<ISystem>();

        public EntitiesManager gameData = new EntitiesManager();
        public List<Event> events = new List<Event>();

        private DebugMode debugMode = new DebugMode();
        private PauseMode pauseMode = new PauseMode();

        public override void Start()
        {
            Render.Renderer.Start();

            pauseMode.Start();
            allSystems.Add(new EnemySpawnSystem());
            allSystems.Add(new ShoppingSystem(gameData));

            CollisionSystem colS = new CollisionSystem();
            ParticlesSystem partS = new ParticlesSystem();
            allSystems.Add(partS);
            allSystems.Add(colS);

            AddListener(partS);
            AddListener(this);
            AddListener(colS);
        }

        public override void Update(GameInputs inputs)
        {
            Weapon.canShoot = true;

            foreach (IGameEventListener eventListener in eventListeners)
            {
                eventListener.HandleEvent(events, gameData);
            }

            Renderer.DrawLifeHUD(gameData.player.currentLife);
            events.Clear();
            gameData.Synchronize();

            Renderer.DrawBackground();

            foreach (ISystem system in allSystems)
            {
                system.Update(inputs, gameData, events);
            }

            pauseMode.Update(inputs);
            debugMode.Update(inputs, gameData, events);
        }
        public void AddListener(IGameEventListener newEvent)
        {
            eventListeners.Add(newEvent);
        }

        public void HandleEvent(List<Event> events, EntitiesManager data)
        {
            foreach (Event e in events)
            {
                if(e is EnemyKilled killedEvent)
                {
                    killedEvent.enemy.isDead = true;
                    if (killedEvent.bullet != null)
                    {
                        killedEvent.bullet.isDead = true;
                        Player.score += killedEvent.enemy.scoreGiven;
                        killedEvent.bullet.weapon.player.money += killedEvent.enemy.moneyGiven;
                        killedEvent.bullet.weapon.player.enemyKilled++;
                    }
                }

                if (e is PlayerKilled gameOverEvent)
                {
                    //TODO
                    gameOverEvent.player.isDead = true;
                    SceneManager.LoadScene(new GameOver(Player.score));
                }
            }
        }
    }
}