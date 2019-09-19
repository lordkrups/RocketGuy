using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainCamera : MonoBehaviour
{
    private class FollowingData
    {
        public Transform Target { get; private set; }
        public Vector3 TargetOffset { get; private set; }
        private Vector3 _speed;
        public FollowingData(Transform target, Vector3 targetOffset)
        {
            Target = target;
            TargetOffset = targetOffset;
        }
        public void Update(Transform tran)
        {
            if (Target == null)
            {
                return;
            }
            tran.position = Vector3.SmoothDamp(tran.position, GetToPosition(), ref _speed, 0.15f);
        }

        public Vector3 GetToPosition()
        {
            if (Target == null)
            {
                return TargetOffset;
            }
            return Target.position + TargetOffset;
        }

    }

    private readonly Vector3 CameraOffset = new Vector3(14.64f, 12.2f, 14.47f);
    private FollowingData _followingData;
    public Transform FollowingTarget
    {
        get { return _followingData == null ? null : _followingData.Target; }
    }

    public void StartFollowing(Transform target, bool smoothStart)
    {
        _followingData = new FollowingData(target, CameraOffset);
        if (smoothStart == false)
        {
            transform.position = _followingData.GetToPosition();
        }

    }
    public void StopFollowing()
    {
        _followingData = null;
    }

    public Vector3 GetCameraDirection(Vector2 controllerDir)
    {
        var d = transform.TransformDirection(new Vector3(controllerDir.x, 0, controllerDir.y));
        d.y = 0;
        return d.normalized;
    }
    void Update()
    {
        if (_followingData != null)
        {
            _followingData.Update(transform);
        }
    }
    public Camera GetCamera()
    {
        return GetComponent<Camera>();

    }


}
