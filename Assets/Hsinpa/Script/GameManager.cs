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
        private Cinemachine.CinemachineVirtualCamera m_cinemaVirtualCam;

        [SerializeField]
        private string room_id;

        RaycastHit[] _cacheHits = new RaycastHit[1];

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

            var net_player = m_networkRunner.Spawn(m_playerPreafb, FindSpawnPosition(), Quaternion.identity);
            var characterCtrl = net_player.GetComponent<Hsinpa.UnityController.CustomPersonController>();
            m_cinemaVirtualCam.Follow = characterCtrl.CinemachineCameraTarget.transform;

            NetPlayerBehaivor.Rpc_MyStaticRpc(m_networkRunner, 1);
        }

        private Vector3 FindSpawnPosition() 
        {
            float xMin = -10f, xMax = 20f;
            float zMin = 0f, zMax = 30f;

            Vector3 position = new Vector3(Random.Range(xMin, xMax), 10, Random.Range(zMin, zMax));
            int maxTries = 10;
            int tries = 0;
            Ray ray = new Ray(position, new Vector3(0, -1, 0));
            Physics.RaycastNonAlloc(ray, _cacheHits, maxDistance: 11);

            while (Physics.RaycastNonAlloc(ray, _cacheHits, maxDistance: 11) <= 0 || tries < maxTries) {
                ray.origin = new Vector3(Random.Range(xMin, xMax), 10, Random.Range(zMin, zMax));
                tries++;
            }

            return _cacheHits[0].point;
        }

    }
}
