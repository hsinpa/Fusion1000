using Fusion;
using Hsinpa.Nettwork;
using Hsinpa.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hsinpa.Utility.EventStatic;

namespace Hsinpa.Demo
{
    public class GolfDemoCharacter : NetBasePlayerBehaivor
    {
        private async void Start() {
            await Utility.UtilityFunc.WaitUntil(() => !string.IsNullOrEmpty(player_id), 100, 5000);

            this.gameObject.name = player_id;
            SimpleEventSystem.Send(MultiplayerInGameEvent.ObjectSpawnEvent, this);
        }
    }
}