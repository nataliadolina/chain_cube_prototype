using UnityEngine;

namespace GameSettings
{
    internal class ApplyGameSettings : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}
