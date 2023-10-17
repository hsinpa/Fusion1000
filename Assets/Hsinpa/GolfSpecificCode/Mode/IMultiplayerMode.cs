using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GoldMultiStatic;

namespace Hsinpa.Golf {
    public interface IMultiplayerMode
    {
        MultiplayerMode multiplayerMode { get; }
        MultiplayerStage multiplayerStage { get; }

        void GameStart(bool send_net_event = false);

        void BallHit();
        void BallStop();

        void GameEnd(bool send_net_event = false);

    }
}
