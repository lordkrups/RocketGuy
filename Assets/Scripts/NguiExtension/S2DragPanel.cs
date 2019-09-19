using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIPanel))]
[RequireComponent(typeof(UIScrollView))]
public class S2DragPanel : MonoBehaviour
{
    public GameObject objectsParent;
    public GameObject dragObjectPref;

    public int countInLine;
    public int initX;
    public int initY;
    public int gapX;
    public int gapY;

    public S2DragPanelObject[] AllObjects { get; private set; }
    public int TotalCount { get; private set; }
    public int InitX { get; private set; }
    public int InitY { get; private set; }
    public float GapX { get; private set; }
    public float GapY { get; private set; }

    private float _length;
    //private float _p;
    private float _gap;
    private float _checkInitPos;
    private float _checkFinalPos;
    private List<S2DragPanelObject[]> _objectList;
    private UIPanel _panel;
    private UIScrollView _scrollView;
    private Vector3 _initPanelPos;
    private float _lastPosition;
    private UIScrollView.Movement _movement;

    private Action<S2DragPanelObject> _setPosition;

    private bool _isEnable;
    public void Init<T>(Action<int, T> initObject, Action<int, T> setObject)
    {
        _panel = GetComponent<UIPanel>();
        _scrollView = GetComponent<UIScrollView>();
        _initPanelPos = transform.localPosition;
        if (objectsParent == null)
        {
            objectsParent = gameObject;
        }

        if (_panel == null || _scrollView == null)
        {
            throw new Exception("S2DragPanel: UIPanel or UIScrollView is null");
        }

        _movement = _scrollView.movement;
        InitX = initX;
        InitY = initY;
        GapX = gapX;
        GapY = gapY;
        float startPos = CalculateInitPosAndGap();

        float gap = Mathf.Abs(_gap);
        float finalP = startPos + _length + 3.5f * gap;

        if (countInLine == 0)
        {
            throw new Exception("S2DragPanel: countInLine must not be zero");
        }
        if (gap == 0)
        {
            throw new Exception("S2DragPanel: gap must not be zero");
        }

        int lineCount = (int)((finalP - startPos) / gap);
        if (lineCount >= 999)
        {
            throw new Exception("S2DragPanel: gap is too small");
        }
        _objectList = new List<S2DragPanelObject[]>(lineCount);
        AllObjects = new S2DragPanelObject[lineCount * countInLine];

        int index = 0;
        for (int i = 0; i < lineCount; i++)
        {
            var newLine = new S2DragPanelObject[countInLine];
            for (int k = 0; k < countInLine; k++)
            {
                var baseObj = NGUITools.AddChild(objectsParent, dragObjectPref);
                S2DragPanelObject<T> obj = new S2DragPanelObject<T>();
                obj.Init(baseObj, _panel, index, initObject, setObject);
                _setPosition(obj);
                newLine[k] = obj;
                AllObjects[index] = obj;
                index++;
            }
            _objectList.Add(newLine);
        }

/*
        while (_p < finalP && index < 1000)
        {
            var newLine = new S2DragPanelObject[countInLine];
            for (int k = 0; k < countInLine; k++)
            {
                var baseObj = NGUITools.AddChild(objectsParent, dragObjectPref);
                S2DragPanelObject<T> obj = new S2DragPanelObject<T>();
                obj.Init(baseObj, _panel, index, initObject, setObject);
                _setPosition(obj);
                newLine[k] = obj;
                ObjectList.Add(obj);
                index++;
            }
            _objectList.Add(newLine);
            _p += gap;
        }
        if (index >= 999)
        {
            throw new Exception("S2DragPanel: gap is too small");
        }
*/
        if (AllObjects.Length == 0)
        {
            throw new Exception("S2DragPanel: Object is not exist");
        }
        _isEnable = true;
    }

    public void SetObjectGap(float x, float y)
    {
        GapX = x;
        GapY = y;
        CalculateInitPosAndGap();
    }

    private float CalculateInitPosAndGap()
    {
        float startPos = 0;
        if (_movement == UIScrollView.Movement.Vertical)
        {
            _length = _panel.finalClipRegion.w;
            startPos = InitY;
            _gap = GapY;
            _setPosition = SetVerticalPanelPosition;
            float checkLength = _panel.finalClipRegion.w * 0.5f + Mathf.Abs(_gap) * 1.5f;
            float cal = objectsParent != null ? objectsParent.transform.localPosition.y : 0;
            _checkInitPos = _panel.finalClipRegion.y + checkLength - cal + _initPanelPos.y + transform.localPosition.y;
            _checkFinalPos = _panel.finalClipRegion.y - checkLength - cal + _initPanelPos.y + transform.localPosition.y;
        }
        else if (_movement == UIScrollView.Movement.Horizontal)
        {
            _length = _panel.finalClipRegion.z;
            startPos = InitX;
            _gap = GapX;
            _setPosition = SetHorizontalPanelPosition;
            float checkLength = _panel.finalClipRegion.z * 0.5f + Mathf.Abs(_gap) * 1.5f;
            float cal = objectsParent != null ? objectsParent.transform.localPosition.x : 0;
            _checkInitPos = _panel.finalClipRegion.x - checkLength + cal - _initPanelPos.x + transform.localPosition.x;
            _checkFinalPos = _panel.finalClipRegion.x + checkLength + cal - _initPanelPos.x + transform.localPosition.x;
        }
        return startPos;
    }

    public void SetPanelDepth(int depth)
    {
        _panel.depth = depth;
    }

    public void SetTotalCount(int cnt, bool refreshPostion = false)
    {
        TotalCount = cnt;
        ResetData(refreshPostion);
    }
    private void SetVerticalPanelPosition(S2DragPanelObject obj)
    {
        int index = obj.ObjectIndex;
        obj.transform.localPosition = new Vector3(InitX + GapX * (index % countInLine), InitY - GapY * (index / countInLine));
    }
    private void SetHorizontalPanelPosition(S2DragPanelObject obj)
    {
        int index = obj.ObjectIndex;
        obj.transform.localPosition = new Vector3(InitX + GapX * (index / countInLine), InitY - GapY * (index % countInLine));
    }

    private void SetObject(S2DragPanelObject obj)
    {
        if (obj.ObjectIndex < TotalCount)
        {
            obj.gameObject.SetActive(true);
            obj.Set();
        }
        else
        {
            obj.gameObject.SetActive(false);
        }
    }

    public void ResetData(bool refreshPosition)
    {
        if (refreshPosition)
        {
            _scrollView.DisableSpring();
            transform.localPosition = _initPanelPos;
            _panel.clipOffset = new Vector2(0, 0);
            for (int i = 0; i < _objectList.Count; i++)
            {
                for (int j = 0; j < _objectList[i].Length; j++)
                {
                    _objectList[i][j].ObjectIndex = i * _objectList[i].Length + j;
                    _setPosition(_objectList[i][j]);
                }
            }
        }
        for (int i = 0; i < AllObjects.Length; i++)
        {
            SetObject(AllObjects[i]);
        }
    }

    public void MoveTo(Vector3 moveToRelative)
    {
        _scrollView.MoveRelative(moveToRelative);
    }

    public void MoveTo(int dataIndex)
    {
        dataIndex = Mathf.Clamp(dataIndex, 0, TotalCount);
        var toMove = new Vector3();
        if (_movement == UIScrollView.Movement.Vertical)
        {
            toMove.y = _gap * (dataIndex / countInLine);
        }
        else if (_movement == UIScrollView.Movement.Horizontal)
        {
            toMove.x = _gap * (dataIndex / countInLine);
        }
        if (_length < (TotalCount / countInLine) * _gap)
        {
            _scrollView.DisableSpring();
            MoveTo(_initPanelPos + toMove);
        }
    }

    public void SetEnableScrollView(bool isEnable)
    {
        _scrollView.enabled = isEnable;
    }

    void Update()
    {
        if (_isEnable == false)
        {
            return;
        }

        S2DragPanelObject[] firstLine = _objectList[0];
        S2DragPanelObject [] lastLine = _objectList[_objectList.Count - 1];
        int firstIndex = firstLine[0].ObjectIndex;
        int lastIndex = lastLine[lastLine.Length - 1].ObjectIndex;

        if (_movement == UIScrollView.Movement.Vertical)
        {
            if (_lastPosition < transform.localPosition.y)
            {
                if (GapY > 0)
                {
                    if (lastIndex < TotalCount - 1 && firstLine[0].Pos.y > _checkInitPos)
                    {
                        _objectList.Add(firstLine);
                        _objectList.RemoveAt(0);
                        for (int i = 0; i < firstLine.Length; i++)
                        {
                            firstLine[i].ObjectIndex = lastIndex + i + 1;
                            firstLine[i].transform.localPosition = new Vector3(firstLine[i].transform.localPosition.x,
                                lastLine[i].transform.localPosition.y - GapY, 0);
                            SetObject(firstLine[i]);
                        }
                    }
                }
                else
                {
                    if (firstIndex > 0 && lastLine[0].Pos.y > _checkInitPos)
                    {
                        _objectList.Insert(0, lastLine);
                        _objectList.RemoveAt(_objectList.Count - 1);
                        for (int i = 0; i < lastLine.Length; i++)
                        {
                            lastLine[i].ObjectIndex = firstIndex - lastLine.Length + i;
                            lastLine[i].transform.localPosition = new Vector3(lastLine[i].transform.localPosition.x,
                                firstLine[i].transform.localPosition.y + GapY, 0);
                            SetObject(lastLine[i]);
                        }
                    }
                }
            }
            else if (_lastPosition > transform.localPosition.y)
            {
                if (GapY > 0)
                {
                    if (firstIndex > 0 && lastLine[0].Pos.y < _checkFinalPos)
                    {
                        _objectList.Insert(0, lastLine);
                        _objectList.RemoveAt(_objectList.Count - 1);
                        for (int i = 0; i < lastLine.Length; i++)
                        {
                            lastLine[i].ObjectIndex = firstIndex - lastLine.Length + i;
                            lastLine[i].transform.localPosition = new Vector3(lastLine[i].transform.localPosition.x,
                                firstLine[i].transform.localPosition.y + GapY, 0);
                            SetObject(lastLine[i]);
                        }
                    }
                }
                else
                {
                    if (lastIndex < TotalCount - 1 && firstLine[0].Pos.y < _checkFinalPos)
                    {
                        _objectList.Add(firstLine);
                        _objectList.RemoveAt(0);
                        for (int i = 0; i < firstLine.Length; i++)
                        {
                            firstLine[i].ObjectIndex = lastIndex + i + 1;
                            firstLine[i].transform.localPosition = new Vector3(firstLine[i].transform.localPosition.x,
                                lastLine[i].transform.localPosition.y - GapY, 0);
                            SetObject(firstLine[i]);
                        }
                    }
                }
            }
            else
            {
                return;
            }
            _lastPosition = transform.localPosition.y;

        }
        else if (_movement == UIScrollView.Movement.Horizontal)
        {
            if (_lastPosition > transform.localPosition.x)
            {
                if (GapX > 0)
                {
                    if (lastIndex < TotalCount - 1 && firstLine[0].Pos.x < _checkInitPos)
                    {
                        _objectList.Add(firstLine);
                        _objectList.RemoveAt(0);
                        for (int i = 0; i < firstLine.Length; i++)
                        {
                            firstLine[i].ObjectIndex = lastIndex + i + 1;
                            firstLine[i].transform.localPosition = new Vector3(lastLine[i].transform.localPosition.x + GapX,
                                firstLine[i].transform.localPosition.y, 0);
                            SetObject(firstLine[i]);
                        }
                    }
                }
                else
                {
                    if (firstIndex > 0 && lastLine[0].Pos.x < _checkInitPos)
                    {
                        _objectList.Insert(0, lastLine);
                        _objectList.RemoveAt(_objectList.Count - 1);
                        for (int i = 0; i < lastLine.Length; i++)
                        {
                            lastLine[i].ObjectIndex = firstIndex - lastLine.Length + i;
                            lastLine[i].transform.localPosition = new Vector3(firstLine[i].transform.localPosition.x - GapX,
                                lastLine[i].transform.localPosition.y, 0);
                            SetObject(lastLine[i]);
                        }
                    }
                }
            }
            else if (_lastPosition < transform.localPosition.x)
            {
                if (GapX > 0)
                {
                    if (firstIndex > 0 && lastLine[0].Pos.x > _checkFinalPos)
                    {
                        _objectList.Insert(0, lastLine);
                        _objectList.RemoveAt(_objectList.Count - 1);
                        for (int i = 0; i < lastLine.Length; i++)
                        {
                            lastLine[i].ObjectIndex = firstIndex - lastLine.Length + i;
                            lastLine[i].transform.localPosition = new Vector3(firstLine[i].transform.localPosition.x - GapX,
                                lastLine[i].transform.localPosition.y, 0);
                            SetObject(lastLine[i]);
                        }
                    }
                }
                else
                {
                    if (lastIndex < TotalCount - 1 && firstLine[0].Pos.x > _checkFinalPos)
                    {
                        _objectList.Add(firstLine);
                        _objectList.RemoveAt(0);
                        for (int i = 0; i < firstLine.Length; i++)
                        {
                            firstLine[i].ObjectIndex = lastIndex + i + 1;
                            firstLine[i].transform.localPosition = new Vector3(lastLine[i].transform.localPosition.x + GapX,
                                firstLine[i].transform.localPosition.y, 0);
                            SetObject(firstLine[i]);
                        }
                    }
                }
            }
            else
            {
                return;
            }
            _lastPosition = transform.localPosition.x;
        }


    }

    #region S2DragPanelObject
    public class S2DragPanelObject
    {
        public Guid Id { get; internal set; }
        public int ObjectIndex { get; set; }
        public Transform transform { get; internal set; }
        public GameObject gameObject { get; internal set; }
        internal UIPanel _panel;
        public Vector3 Pos
        {
            get { return _panel.transform.localPosition + transform.localPosition; }
        }
        public void Init(GameObject baseObj, UIPanel panel, int index)
        {
            Id = Guid.NewGuid();
            _panel = panel;
            transform = baseObj.GetComponent<Transform>();
            gameObject = baseObj.gameObject;
            ObjectIndex = index;
        }
        public virtual void Set()
        {
        }
    }

    public class S2DragPanelObject<T> : S2DragPanelObject
    {
        public T Obj { get; private set; }
        private Action<int, T> _setAction;

        public void Init(GameObject baseObj, UIPanel panel, int index, Action<int, T> initAction, Action<int, T> setAction)
        {
            Init(baseObj, panel, index);
            _setAction = setAction;
            Obj = baseObj.GetComponent<T>();
            if (initAction != null)
            {
                initAction(index, Obj);
            }
        }
        public override void Set()
        {
            if (_setAction != null)
            {
                _setAction(ObjectIndex, Obj);
            }
        }

    }
    #endregion



}
