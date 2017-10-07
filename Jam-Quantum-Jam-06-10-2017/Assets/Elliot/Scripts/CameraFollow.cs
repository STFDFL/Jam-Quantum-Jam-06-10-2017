using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform localTargetRef;

    public void CameraFollowConstructor(Transform targetTransform)
    {
        localTargetRef = targetTransform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (localTargetRef != null)
        {
            Vector3 desiredPosition = localTargetRef.position + GameManager.Instance.offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, GameManager.Instance.smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
