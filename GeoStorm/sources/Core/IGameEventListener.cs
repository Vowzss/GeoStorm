using System.Collections.Generic;

namespace GeoStorm.Core
{
    interface IGameEventListener
    {
        public void HandleEvent(List<Event> events, EntitiesManager data)
        {
           
        }
    }
}
