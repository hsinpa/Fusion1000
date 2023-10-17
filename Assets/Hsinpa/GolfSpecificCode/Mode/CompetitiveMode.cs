using Hsinpa.Nettwork;
using Hsinpa.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hsinpa.Utility.EventStatic;

namespace Hsinpa.Golf
{
    public class CompetitiveMode : IMultiplayerMode
    {
        public GoldMultiStatic.MultiplayerMode multiplayerMode => _multiplayerMode;
        private GoldMultiStatic.MultiplayerMode _multiplayerMode = GoldMultiStatic.MultiplayerMode.Competitive;

        public GoldMultiStatic.MultiplayerStage multiplayerStage => _multiplayerStage;
        private GoldMultiStatic.MultiplayerStage _multiplayerStage = GoldMultiStatic.MultiplayerStage.Lobby;

        private FusionEntryCode _fusionEntryCode;

        public CompetitiveMode(FusionEntryCode fusionEntryCode) {
            this._fusionEntryCode = fusionEntryCode;
        }

        public void GameStart(bool send_net_event = false) {
            _multiplayerStage = GoldMultiStatic.MultiplayerStage.InGame;

            if (send_net_event)
                FusionEntryCode.Rpc_IntantBroadcast(this._fusionEntryCode.networkRunner, EventStatic.MultiplayerNetEvent.GameStartEvent, "");
        }

        public void BallHit() {

        }

        public void BallStop() {

        }

        public void GameEnd(bool send_net_event = false) {
            if (send_net_event)
                FusionEntryCode.Rpc_IntantBroadcast(this._fusionEntryCode.networkRunner, EventStatic.MultiplayerNetEvent.GameEndEvent, "");
        }
    }
}