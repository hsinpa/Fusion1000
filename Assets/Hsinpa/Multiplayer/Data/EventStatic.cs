using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hsinpa.Utility
{
    public class EventStatic
    {
        public class MultiplayerInGameEvent {
            public const int ObjectSpawnEvent = 105;
            public const int UIEventGameStartEvent = 106;
        }

        public class MultiplayerNetEvent
        {
            public const int GameStartEvent = 300;
            public const int GameEndEvent = 301;
        }

    }
}