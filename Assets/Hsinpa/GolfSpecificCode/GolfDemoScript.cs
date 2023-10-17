using Fusion;
using Hsinpa.Golf;
using Hsinpa.Nettwork;
using Hsinpa.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GoldMultiStatic;
using static Hsinpa.Utility.EventStatic;

namespace Hsinpa.Demo
{
    public class GolfDemoScript : MonoBehaviour
    {
        [SerializeField]
        private NetworkObject netBasePlayerBehaivorPrefab;

        [SerializeField]
        Transform characterHolder;

        FusionEntryCode fusionEntryCode;
        private string player_id;

        private Dictionary<int, NetBasePlayerBehaivor> playerRefDict = new Dictionary<int, NetBasePlayerBehaivor>();
        private Dictionary<string, NetBasePlayerBehaivor> playerIDDict = new Dictionary<string, NetBasePlayerBehaivor>();

        private IMultiplayerMode _multiplayerMode;

        public void Start() {
            
            string room_id = "demo_room_1";
            this.player_id = Hsinpa.Utility.UtilityFunc.RandomString(8);
            Debug.Log("player_id " + player_id);

            fusionEntryCode = GameObject.FindObjectOfType<FusionEntryCode>();
            fusionEntryCode.OnServerConnected += OnServerConnectedEvent;
            fusionEntryCode.OnPlayerJoinEvent += OnPlayerJoinEvent;
            fusionEntryCode.OnPlayerLeaveEvent += OnPlayerLeaveEvent;

            SetGameMode(MultiplayerMode.Competitive);

            fusionEntryCode.JoinRoom(room_id, player_id);

            FusionEntryCode.OnDataMessage += OnNetEvent;
            SimpleEventSystem.CustomEventListener += OnSimpleEvent;
        }

        private void SetGameMode(MultiplayerMode multiplayerMode) {
            if (multiplayerMode == MultiplayerMode.TurnBase)
                _multiplayerMode = new TurnbaseMode();

            if (multiplayerMode == MultiplayerMode.Competitive)
                _multiplayerMode = new CompetitiveMode(fusionEntryCode);
        }

        #region Event
        private void OnServerConnectedEvent() {
            Debug.Log("OnServerConnectedEvent");

            fusionEntryCode.SpawnObject(netBasePlayerBehaivorPrefab, Vector2.zero, Quaternion.identity, OnBeforeSpawned);
        }

        public void OnBeforeSpawned(NetworkRunner runner, NetworkObject obj) {
           var baseBehavior = obj.GetComponent<NetBasePlayerBehaivor>();
            baseBehavior.SetUp(this.player_id, runner.LocalPlayer);
        }

        private void OnPlayerJoinEvent(PlayerRef playerRef) {
            Debug.Log("OnPlayerJoinEvent " + playerRef.PlayerId);
        }

        private async void OnPlayerLeaveEvent(PlayerRef playerRef) {
            Debug.Log("OnPlayerLeaveEvent " + playerRef.PlayerId);

            if (playerRefDict.TryGetValue(playerRef.PlayerId, out NetBasePlayerBehaivor behaviorObject)) {
                if (behaviorObject == null) return;

                playerRefDict.Remove(playerRef.PlayerId);
                playerIDDict.Remove(behaviorObject.player_id);

                behaviorObject.gameObject.SetActive(false);
                _ = Hsinpa.Utility.UtilityFunc.DoDelayWork(0.5f, () => {
                    fusionEntryCode.Despawn(behaviorObject.GetComponent<NetworkObject>());
                });
            }
        }

        private void OnNetEvent(int id, string raw_text) { 
            if (id == MultiplayerNetEvent.GameStartEvent) {
                Debug.Log("Game Start");
                _multiplayerMode.GameStart();
            }

            if (id == MultiplayerNetEvent.GameEndEvent) {
                Debug.Log("Game End");
                _multiplayerMode.GameEnd();
            }
        }

        private void OnSimpleEvent(int id, params object[] customObjects) {
            if (id == MultiplayerInGameEvent.ObjectSpawnEvent && customObjects.Length == 1) {
                var playerBehavior = (NetBasePlayerBehaivor)customObjects[0];

                Debug.Log("MultiplayerEvent.ObjectSpawnEvent ");
                Debug.Log(playerBehavior.player_ref.PlayerId);
                Debug.Log(playerBehavior.player_id);

                Hsinpa.Utility.UtilityFunc.SetDictionary(playerRefDict, playerBehavior.player_ref.PlayerId, playerBehavior);
                Hsinpa.Utility.UtilityFunc.SetDictionary(playerIDDict, playerBehavior.player_id, playerBehavior);

                playerBehavior.transform.SetParent(characterHolder);
            }

            if (id == MultiplayerInGameEvent.UIEventGameStartEvent) {
                Debug.Log("MultiplayerInGameEvent.UIEventGameStartEvent");

                _multiplayerMode.GameStart(send_net_event: true);
            }
        }

        private void OnDestroy() {
            fusionEntryCode.Disconnect();
            SimpleEventSystem.CustomEventListener -= OnSimpleEvent;
        }
        #endregion
    }
}
