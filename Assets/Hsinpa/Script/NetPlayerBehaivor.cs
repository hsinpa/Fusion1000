using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Inputs;
using UnityEngine.InputSystem;

namespace Hsinpa.Nettwork {
    public class NetPlayerBehaivor : SimulationBehaviour
    {
        [SerializeField]
        private CharacterController m_characterController;

        [SerializeField]
        private float m_speed = 1;

        private CharacterAction m_characterAction;

        private Vector3 m_inputAxis;

        void Start()
        {
            Debug.Log("NetPlayerBehaivor " + Object.HasStateAuthority);
            if (!Object.HasStateAuthority) {
                this.enabled = false;
                return;
            }

            m_inputAxis = new Vector2();
            m_characterAction = new CharacterAction();
            m_characterAction.Enable();
            m_characterAction.Movement.Down.started += OnDownEvent;
            m_characterAction.Movement.Top.started += OnTopEvent;
            m_characterAction.Movement.Right.started += OnRightEvent;
            m_characterAction.Movement.Left.started += OnLeftEvent;

            m_characterAction.Movement.Down.canceled += OnTopEvent;
            m_characterAction.Movement.Top.canceled += OnDownEvent;
            m_characterAction.Movement.Right.canceled += OnLeftEvent;
            m_characterAction.Movement.Left.canceled += OnRightEvent;
        }

        public override void FixedUpdateNetwork() {
            if (!Object.HasStateAuthority || m_characterController == null) return;

            PerformMove(m_inputAxis);
        }

        private void PerformMove(Vector3 p_vector) {
            m_characterController.Move(p_vector * Time.deltaTime * m_speed);
        }

        private void OnDownEvent(InputAction.CallbackContext callbackCtx) {
            m_inputAxis.z -= 1;
        }

        private void OnTopEvent(InputAction.CallbackContext callbackCtx)
        {
            m_inputAxis.z += 1;
        }

        private void OnRightEvent(InputAction.CallbackContext callbackCtx)
        {
            m_inputAxis.x += 1;
        }

        private void OnLeftEvent(InputAction.CallbackContext callbackCtx)
        {
            m_inputAxis.x -= 1;
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
