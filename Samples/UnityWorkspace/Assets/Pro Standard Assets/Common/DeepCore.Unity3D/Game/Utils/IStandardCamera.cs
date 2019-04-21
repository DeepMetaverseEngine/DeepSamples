using UnityEngine;
using System.Collections;

namespace DeepCore.Unity3D.Utils
{
    public class CameraAction
    {
        public virtual void Update(ICamera camera, float deltaTime)
        {
        }
    }

    public interface ICamera
    {
        void RotateToWithFocus2D(Vector3 forward);
        void RotateWithActorDirection(Vector3 forward);
    }
}
