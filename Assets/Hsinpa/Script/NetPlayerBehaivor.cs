using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Inputs;
using UnityEngine.InputSystem;
using Hsinpa.UnityController;

namespace Hsinpa.Nettwork {
    public class NetPlayerBehaivor : NetworkBehaviour
    {
        [SerializeField]
        private CustomPersonController m_characterController;

        [SerializeField]
        private PlayerInput m_playerInput;

        [SerializeField]
        private Animator m_animator;

        [SerializeField]
        private CharacterController m_characterCtrl;

        [SerializeField]
        private SkinnedMeshRenderer m_skinMesh;
        public SkinnedMeshRenderer SkinMesh => m_skinMesh;

        [SerializeField]
        private NetworkObject m_netObject;

        private int m_id;
        public int ID => m_id;

        void Start()
        {
            Debug.Log("NetPlayerBehaivor " + Object.HasStateAuthority);

            m_id = m_netObject.Id.GetHashCode();

            if (!Object.HasStateAuthority) {
                CharacterLODManager.Instance.Register(m_id, this);
                this.enabled = false;
                return;
            }
            m_characterController.SetUp(Camera.main);

            m_playerInput.enabled = true;
            m_characterController.enabled = true;
        }

        public override void FixedUpdateNetwork() {
            if (!Object.HasStateAuthority || !m_characterController.enabled) return;

            m_characterController.OnUpdate();
        }

        public void SetObjectVisible(bool is_visible) {
            m_animator.enabled = is_visible;
            m_characterCtrl.enabled = is_visible;
            m_skinMesh.enabled = is_visible;
        }

        private void OnDestroy()
        {
            CharacterLODManager.Instance.Remove(m_id);
        }

        [Rpc]
        public static void Rpc_MyStaticRpc(NetworkRunner runner, [RpcTarget] PlayerRef player, int a) {
            Debug.Log(a);
        }

        [Rpc]
        public static void Rpc_MyStaticRpc(NetworkRunner runner, int a)
        {
            Debug.Log(a);
        }

    }
}
