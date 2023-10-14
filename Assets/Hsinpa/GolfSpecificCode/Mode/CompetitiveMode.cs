using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hsinpa.Golf
{
    public class CompetitiveMode : IMultiplayerMode
    {
        public GoldMultiStatic.MultiplayerMode multiplayerMode => _multiplayerMode;
        private GoldMultiStatic.MultiplayerMode _multiplayerMode = GoldMultiStatic.MultiplayerMode.Competitive;

        public GoldMultiStatic.MultiplayerStage multiplayerStage => _multiplayerStage;
        private GoldMultiStatic.MultiplayerStage _multiplayerStage = GoldMultiStatic.MultiplayerStage.Lobby;

        public CompetitiveMode() { 
        
        }

        public void GameStart() {
            _multiplayerStage = GoldMultiStatic.MultiplayerStage.InGame;
        }

        public void BallHit() {

        }

        public void BallStop() {

        }

        public void GameEnd() {

        }
    }
}