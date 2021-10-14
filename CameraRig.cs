using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform lockTarget;
    public float cameraLookSmoothing = 0.0f;

    public void LateUpdate()
    {
    	transform.position = Vector3.Lerp(transform.position, lockTarget.position, 1/cameraLookSmoothing);
        transform.rotation = Quaternion.Lerp(transform.rotation, lockTarget.rotation, 1/cameraLookSmoothing);
    }

    public void SetLockTarget(Transform target)
    {
    	lockTarget = target;
    }

    public void SetSmoothing(float smoothValue)
    {
        cameraLookSmoothing = smoothValue;
    }
}
