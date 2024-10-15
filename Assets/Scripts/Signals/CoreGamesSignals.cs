using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGamesSignals : MonoBehaviour
    {
        public static CoreGamesSignals Instance;

        private void Awake()
        {
            if (Instance != null & Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public UnityAction<byte> onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevet = delegate { };
        public UnityAction onReset = delegate { };
        public Func<byte>onGetLevelValue = delegate{ return  0; };
    }
}