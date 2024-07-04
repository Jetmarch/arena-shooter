using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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

        public void ShakeCamera(float duration)
        {
            _camera.transform.DOShakePosition(duration);
        }
    }
}