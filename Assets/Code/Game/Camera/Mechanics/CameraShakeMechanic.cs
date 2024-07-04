using DG.Tweening;
using System;
using UnityEngine;

namespace ArenaShooter.CameraScripts
{
    public class CameraShakeMechanic
    {
        private Camera _camera;

        public CameraShakeMechanic(Camera camera)
        {
            _camera = camera;
        }

        public void ShakeCamera(CameraShakeData data)
        {
            _camera.transform.DOShakePosition(data.Duration, data.Strength, data.Vibrato, data.Randomness);
        }
    }

    [Serializable]
    public struct CameraShakeData
    {
        public float Duration;
        public float Strength;
        public int Vibrato;
        public float Randomness;
    }
}