using UnityEngine;

namespace Unityroom.Internal
{
    public sealed class UnityroomSettings : ScriptableObject
    {
        [SerializeField] private string _hmacKey;
        public string HmacKey => _hmacKey;

        const string AssetPath = "UnityroomSettings";

        public static UnityroomSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<UnityroomSettings>(AssetPath);
                }
                return _instance;
            }
        }
        private static UnityroomSettings _instance;
    }
}