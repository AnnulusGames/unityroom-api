using System.Collections;
using UnityEngine;

namespace Unityroom.Internal
{
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    internal sealed class CoroutineDispatcher : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            _instance = new GameObject(nameof(CoroutineDispatcher)).AddComponent<CoroutineDispatcher>();
        }

        private static CoroutineDispatcher _instance;
        public static CoroutineDispatcher Instance => _instance;

        public void Run(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}