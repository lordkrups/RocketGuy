using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class VirtualController : MonoBehaviour
{

    public GameObject obj;
    public UISprite point;
    //public UISprite bg;

    private const float MaxRange = 80;
    private Action<Vector2> _holdingAction;
    private Action _releaseAction;
    private Camera _uiCamera;
    private Coroutine _coPlayControlling;

    public bool canBeUsed;
    public bool isPlaying;

    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

    public void Init(Camera uiCamera, Action<Vector2> holdingAction, Action releaseAction)
    {
        _uiCamera = uiCamera;
        _holdingAction = holdingAction;
        _releaseAction = releaseAction;
        //_logs = new List<string>();
        Hide();
    }

    private void Hide()
    {
        Stop();
        obj.Off();
    }

    public void Stop()
    {
        IsPlaying = false;
        if (_coPlayControlling != null)
        {
            StopCoroutine(_coPlayControlling);
            _coPlayControlling = null;
            if (_releaseAction != null)
            {
                _releaseAction();
            }

        }
    }

    /*
        private void AddLog(string log)
        {
            _cnt++;
            _logs.Add(_cnt +  " " + log);
            if (_logs.Count > 10)
            {
                _logs.RemoveAt(0);
            }

            var sb = new StringBuilder();
            foreach (var g in _logs)
            {
                sb.AppendLine(g);
            }

            tempLabel.text = sb.ToString();
        }
    */

#if UNITY_EDITOR

    void Update()
    {
        if (_holdingAction == null)
        {
            return;
        }
        if (_coPlayControlling != null)
        {
            return;
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        if (vertical != 0 || horizontal != 0)
        {
            IsPlaying = true;
            _holdingAction(new Vector2(horizontal, vertical));
            //Debug.Log("horizontal: " + horizontal);
            //Debug.Log("vertical: " + vertical);
        }
        else
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                if (_releaseAction != null)
                {
                    _releaseAction();
                }
            }
        }


    }
#endif

    public void StartControlling(int touchId = -1)
    {
        if (canBeUsed)
        {
            if (IsPlaying)
            {
                return;
            }
            if (touchId == -1)
            {
                touchId = S2InputManager.GetTouchId();
            }

            //AddLog("StartControlling TouchId : " + touchId);
            if (touchId >= 0 && canBeUsed)
            {
                obj.On();
                IsPlaying = true;
                Vector2 touchPos;
                S2InputManager.GetTouchPosition(touchId, out touchPos);
                Vector3 pos = _uiCamera.ScreenToWorldPoint(touchPos);
                var dir = UpdatePoint(transform.InverseTransformPoint(pos));
                transform.position = pos;
                _coPlayControlling = StartCoroutine(CoPlayControlling(touchId));
                /*
                            if (_holdingAction != null)
                            {
                                _holdingAction(dir);
                            }
                */
            }
        }
    }

    private IEnumerator CoPlayControlling(int touchId)
    {
        yield return null;
        Vector2 touchPos;
        bool isStarted = false;
        while (S2InputManager.GetTouchPosition(touchId, out touchPos))
        {
            var pos = _uiCamera.ScreenToWorldPoint(touchPos);
            var localPos = transform.InverseTransformPoint(pos);
            var dir = UpdatePoint(localPos);
            if (isStarted == false)
            {
                if (Vector2.Distance(localPos, new Vector2()) > 5)
                {
                    isStarted = true;
                }
            }
            if (isStarted && _holdingAction != null)
            {
                _holdingAction(dir);
            }

            yield return null;
        }

        while (S2InputManager.GetTouchPosition(touchId, out touchPos))
        {
            var pos = _uiCamera.ScreenToWorldPoint(touchPos);
            var dir = UpdatePoint(transform.InverseTransformPoint(pos));
            if (_holdingAction != null)
            {
                _holdingAction(dir);
            }
            yield return null;

        }

        if (_releaseAction != null)
        {
            _releaseAction();
        }

        Hide();
    }


    private Vector2 UpdatePoint(Vector2 localPos)
    {
        //Debug.Log("VC UpdatePoint");

        Vector2 dir = localPos.normalized;
        Vector2 d;
        if (Vector2.Distance(new Vector2(), localPos) < MaxRange)
        {
            d = localPos;
        }
        else
        {
            d = dir * MaxRange;
        }
        point.transform.localPosition = d;
        return dir;
    }


}
