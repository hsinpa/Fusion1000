using Fusion;
using Hsinpa.Nettwork;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Fusion.NetworkRunner;

namespace Hsinpa.Nettwork
{
    public class FusionEntryCode : SimulationBehaviour, IPlayerJoined, IPlayerLeft
    {
        [SerializeField]
        private NetworkSceneManagerDefault m_sceneManager;

        [SerializeField]
        private NetworkRunner m_networkRunner;
        public NetworkRunner networkRunner => m_networkRunner;

        public System.Action OnServerConnected;
        public System.Action<PlayerRef> OnPlayerJoinEvent;
        public System.Action<PlayerRef> OnPlayerLeaveEvent;

        public delegate void DataMessageDelegate(int event_id, string content);
        public static DataMessageDelegate OnDataMessage;

        public PlayerRef self_player => m_networkRunner.LocalPlayer;
        public PlayerRef[] players => m_networkRunner.ActivePlayers.ToArray();

        private string player_id;

        public async void JoinRoom(string p_room_id, string player_id) {
            this.player_id = player_id;

            var start_game = await m_networkRunner.StartGame(new StartGameArgs() {
                GameMode = GameMode.Shared,
                SessionName = p_room_id,
                SceneManager = m_sceneManager
            });

            OnServerConnected?.Invoke();
        }

        public void Disconnect() {
            m_networkRunner.Disconnect(m_networkRunner.LocalPlayer);
        }

        public void SpawnObject(NetworkObject netBasePlayerBehaivor, Vector3 position, Quaternion rotation, OnBeforeSpawned spawn_callback) {
            var net_player = m_networkRunner.Spawn(netBasePlayerBehaivor, position, rotation, onBeforeSpawned: spawn_callback);
        }

        public void Despawn(NetworkObject networkObject) {
            Runner.Despawn(networkObject, false);
        }

        [Rpc]
        public static void Rpc_SingleMessage(NetworkRunner runner, [RpcTarget] PlayerRef player, int event_id, string content = "") {
            if (runner.LocalPlayer != player)
                OnDataMessage?.Invoke(event_id, content);
        }

        [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority, InvokeLocal = false)]
        public static void Rpc_IntantBroadcast(NetworkRunner runner, int event_id, string content) {
            OnDataMessage?.Invoke(event_id, content);
        }

        public void PlayerJoined(PlayerRef player) {
            if (player != m_networkRunner.LocalPlayer) OnPlayerJoinEvent?.Invoke(player);
        }

        public void PlayerLeft(PlayerRef player) {
            if (player != m_networkRunner.LocalPlayer) OnPlayerLeaveEvent?.Invoke(player);
        }
    }
}