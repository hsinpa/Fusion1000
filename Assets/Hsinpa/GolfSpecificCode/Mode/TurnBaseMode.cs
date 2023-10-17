using Hsinpa.Golf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hsinpa.Golf
{
    public class TurnbaseMode : IMultiplayerMode
    {
        public GoldMultiStatic.MultiplayerMode multiplayerMode => throw new System.NotImplementedException();

        public GoldMultiStatic.MultiplayerStage multiplayerStage => throw new System.NotImplementedException();

        public void BallHit() {
        }

        public void BallStop() {
        }

        public void GameEnd(bool send_net_event = false) {
        }

        public void GameStart(bool send_net_event = false) {
        }
    }
}