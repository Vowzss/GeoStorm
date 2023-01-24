using System.Collections.Generic;
using System.Numerics;

using GeoStorm.MathExtend;
using static Raylib_cs.Raylib;

namespace GeoStorm.Core
{
    class EnemySpawnSystem : ISystem
    {
        static public float timeUntilNextWave = 3;
        static public int waveCount = 0;
        public void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            if (timeUntilNextWave > 0)
                timeUntilNextWave -= GameInputs.deltaTime;

            if(timeUntilNextWave <= 0)
            {
                waveCount++;
                for (int i = 0; i < waveCount*2+2; i++)
                {
                    Vector2 randomPos = Maths.RandomVector(GetScreenWidth(), GetScreenHeight());
                    Vector2 randomPos2 = Maths.RandomVector(GetScreenWidth(), GetScreenHeight());
                    Vector2 randomPos3 = Maths.RandomVector(GetScreenWidth(), GetScreenHeight());

                    data.AddEnemyDelayed(new Grunt(randomPos, 2));
                    data.AddEnemyDelayed(new Shuriken(randomPos2, 2));
                    data.AddEnemyDelayed(new Star(randomPos3, 2));
                }
                timeUntilNextWave = 30-waveCount*0.2f;
                //data.AddBlackHoleDelayed(new BlackHole(randomPos));
            }
;       }
    }
}
