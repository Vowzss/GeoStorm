using System.Collections.Generic;

namespace GeoStorm.Core
{
    interface ISystem : IGameEventListener
    {
        public virtual void Update(GameInputs inputs, EntitiesManager data, List<Event> events) 
        { 
            
        }
    }
}
