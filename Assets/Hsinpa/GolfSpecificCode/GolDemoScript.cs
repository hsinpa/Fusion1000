using Fusion;
using Hsinpa.Nettwork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hsinpa.Demo
{
    public class GolDemoScript : MonoBehaviour
    {
        [SerializeField]
        private NetBasePlayerBehaivor netBasePlayerBehaivorPrefab;

        FusionEntryCode fusionEntryCode;

        public void Start() {
            string room_id = "demo_room_1";
            string player_id = System.Guid.NewGuid().ToString();
            Debug.Log("player_id " + player_id);

            fusionEntryCode = GameObject.FindObjectOfType<FusionEntryCode>();
            fusionEntryCode.OnServerConnected += OnServerConnectedEvent;
            fusionEntryCode.OnPlayerJoinEvent += OnPlayerJoinEvent;
            fusionEntryCode.OnPlayerLeaveEvent += OnPlayerLeaveEvent;

            fusionEntryCode.JoinRoom(room_id, player_id);
        }

        #region Event

        private void OnServerConnectedEvent() {
            Debug.Log("OnServerConnectedEvent");

            fusionEntryCode.SpawnObject(netBasePlayerBehaivorPrefab, Vector2.zero, Quaternion.identity);
        }

        private void OnPlayerJoinEvent(PlayerRef playerRef) {

        }


        private void OnPlayerLeaveEvent(PlayerRef playerRef) {

        }
        #endregion
    }
}
