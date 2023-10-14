using Hsinpa.Golf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hsinpa.Golf
{
    public class SequenceMode : IMultiplayerMode
    {
        public GoldMultiStatic.MultiplayerMode multiplayerMode => throw new System.NotImplementedException();

        public GoldMultiStatic.MultiplayerStage multiplayerStage => throw new System.NotImplementedException();

        public void BallHit() {
            throw new System.NotImplementedException();
        }

        public void BallStop() {
            throw new System.NotImplementedException();
        }

        public void GameEnd() {
            throw new System.NotImplementedException();
        }

        public void GameStart() {
            throw new System.NotImplementedException();
        }

        public void OnPlayerEnter(string player_id) {
            throw new System.NotImplementedException();
        }

        public void OnPlayerLeave(string player_id) {
            throw new System.NotImplementedException();
        }
    }
}