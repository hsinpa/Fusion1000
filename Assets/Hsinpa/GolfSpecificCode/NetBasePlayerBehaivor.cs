using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

namespace Hsinpa.Nettwork {
    [RequireComponent(typeof(NetworkObject))]
    public class NetBasePlayerBehaivor : NetworkBehaviour
    {
        private NetworkObject m_netObject;

        [Networked]
        public string player_id { get; set; }

        [Networked]
        public PlayerRef player_ref { get; set; }

        private uint m_fusion_id;
        public uint FusionID => m_fusion_id;

        public void SetUp(string p_player_id, PlayerRef player_ref) {
            m_netObject = GetComponent<NetworkObject>();
            m_fusion_id = m_netObject.Id.Raw;

            this.player_id = p_player_id;
            this.player_ref = player_ref;
        }
    }
}
