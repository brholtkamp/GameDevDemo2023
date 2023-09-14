using System;
using UnityEngine;

namespace GameDevDemo.Scripts
{
    public class Spin : MonoBehaviour
    {
        [SerializeField]
        private float _spinsPerSecond = 2.0f;

        private void FixedUpdate()
        {
            var spinSpeed = 360.0f / _spinsPerSecond;

            var localTransform = transform;
            var angles = localTransform.localEulerAngles;
            angles.y += spinSpeed * Time.fixedDeltaTime;
            localTransform.localEulerAngles = angles;
        }
    }
}