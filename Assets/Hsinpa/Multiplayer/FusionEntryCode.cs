using Fusion;
using Hsinpa.Nettwork;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Collections.Generic;

namespace Hsinpa.Nettwork
{
    public class FusionEntryCode : SimulationBehaviour, IPlayerJoined, IPlayerLeft
    {    
        [SerializeField]
        private NetworkRunner m_networkRunner;
        public NetworkRunner networkRunner => m_networkRunner;

        public System.Action OnServerConnected;
        public System.Action<PlayerRef> OnPlayerJoinEvent;
        public System.Action<PlayerRef> OnPlayerLeaveEvent;

        public delegate void DataMessageDelegate(string event_id, string content);
        public static DataMessageDelegate OnDataMessage;

        public PlayerRef self_player => Runner.LocalPlayer;
        public ReadOnlyCollection<PlayerRef> players => new ReadOnlyCollection<PlayerRef>(Runner.ActivePlayers.ToArray());

        private string player_id;

        public async void JoinRoom(string p_room_id, string player_id) {
            this.player_id = player_id;

            var start_game = await m_networkRunner.StartGame(new StartGameArgs() {
                GameMode = GameMode.Shared,
                SessionName = p_room_id,
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });

            OnServerConnected?.Invoke();
        }

        public void Disconnect() {
            m_networkRunner.Disconnect(Runner.LocalPlayer);
        }

        public void SpawnObject(NetBasePlayerBehaivor netBasePlayerBehaivor, Vector3 position, Quaternion rotation) {
            var net_player = m_networkRunner.Spawn(netBasePlayerBehaivor, position, rotation);
            net_player.SetUp(player_id);
        }

        [Rpc]
        public static void Rpc_IntantMessage(NetworkRunner runner, [RpcTarget] PlayerRef player, string event_id, string content) {
            if (runner.LocalPlayer != player)
                OnDataMessage?.Invoke(event_id, content);
        }

        [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority, InvokeLocal = false)]
        public static void Rpc_IntantMessage(NetworkRunner runner, string event_id, string content) {
            OnDataMessage?.Invoke(event_id, content);
        }

        public void PlayerJoined(PlayerRef player) {
            if (player != Runner.LocalPlayer) OnPlayerJoinEvent?.Invoke(player);
        }

        public void PlayerLeft(PlayerRef player) {
            if (player != Runner.LocalPlayer) OnPlayerJoinEvent?.Invoke(player);
        }
    }
}