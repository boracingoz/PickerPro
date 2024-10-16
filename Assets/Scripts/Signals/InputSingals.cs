using Keys;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Signals
{
    public class InputSingals : MonoBehaviour
    {
        public static InputSingals Instance;


        private void Awake()
        {
            if (Instance != null & Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public UnityAction onFirstTouch = delegate { };
        public UnityAction onInputTaken = delegate { };
        public UnityAction onInputRelase = delegate { };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
    }
}