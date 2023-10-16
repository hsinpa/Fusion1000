using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Hsinpa.Utility;
using Hsinpa.Nettwork;
using static Hsinpa.Utility.EventStatic;

namespace Hsinpa.Demo
{
    public class GolfDemoUI : MonoBehaviour
    {
        [SerializeField]
        private Button start_button;

        // Start is called before the first frame update
        void Start()
        {
            start_button.onClick.AddListener(OnGameStartBtn);
            FusionEntryCode.OnDataMessage += OnNetEvent;
            //SimpleEventSystem.CustomEventListener += OnSimpleEvent;
        }

        private void OnGameStartBtn() {
            gameObject.SetActive(false);
            SimpleEventSystem.Send(MultiplayerInGameEvent.UIEventGameStartEvent);
        }

        private void OnNetEvent(int id, string raw_text) {
            if (id == MultiplayerNetEvent.GameStartEvent) {
                gameObject.SetActive(false);
            }

            if (id == MultiplayerNetEvent.GameEndEvent) {
                gameObject.SetActive(true);
            }
        }
        //private void OnSimpleEvent(int id, params object[] customObjects) {

        //}

        //private void OnDestroy() {
        //    SimpleEventSystem.CustomEventListener -= OnSimpleEvent;
        //}
    }
}