using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hsinpa.Nettwork {
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private NetworkRunner m_networkRunner;

        [SerializeField]
        private NetPlayerBehaivor m_playerPreafb;

        [SerializeField]
        private string room_id;

        private void Start()
        {
            JoinRoom(room_id, System.Guid.NewGuid().ToString());
        }

        private async void JoinRoom(string p_room_id, string player_id) {

            var start_game = await m_networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = GameMode.Shared,
                SessionName = p_room_id,
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });

            Vector3 position = new Vector3( Random.Range(-20f, 20f), 1, Random.Range(-20f, 20f));
            var net_player = m_networkRunner.Spawn(m_playerPreafb, position, Quaternion.identity);

            NetPlayerBehaivor.Rpc_MyStaticRpc(m_networkRunner, 1);
        }



    }
}
