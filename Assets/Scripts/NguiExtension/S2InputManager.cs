using UnityEngine;
using System.Collections;

public class S2InputManager {
	
	public static int MOUSE = 12393712;
	private static Vector2 exMousePos;
	
	
	public static int GetTouchId()
    {
	    for (int i = 0; i < Input.touches.Length; i++)
	    {
	        if (Input.touches[i].phase == TouchPhase.Began)
	        {
	            return Input.touches[i].fingerId;
	        }
	    }
        if (Input.GetMouseButtonDown(0))
        {
            exMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            return MOUSE;
        }
        return -1;
	}
	
    public static bool GetTouchPosition(int touchId, out Vector2 pos)
    {
        if (touchId == MOUSE)
        {
            pos = Input.mousePosition;
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                return true;
            }
            return false;
        }
	    for (int i = 0; i < Input.touches.Length; i++)
	    {
            if (Input.touches[i].fingerId == touchId)
            {
                var p = Input.touches[i].phase;
                if (p != TouchPhase.Ended && p != TouchPhase.Canceled)
                {
                    pos = Input.touches[i].position;
                    return true;
                }
                break;
            }
	    }
        pos = new Vector2();
        return false;
    }

	public static Vector2 GetMove(int touchId)
    {
		if ( touchId == MOUSE )
        {
			Vector2 pos = new Vector2(Input.mousePosition.x - exMousePos.x, Input.mousePosition.y - exMousePos.y);
			exMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			return pos;
		}
	    for (int i = 0; i < Input.touches.Length; i++)
	    {
	        if (Input.touches[i].fingerId == touchId)
	        {
	            if (Input.touches[i].phase == TouchPhase.Moved)
	            {
	                return Input.touches[i].deltaPosition;
	            }
	            break;
	        }
	    }
		return default(Vector2);
	}
	
	public static bool EndTouch(int touchId)
    {
		if ( touchId == MOUSE ){
			if ( Input.GetMouseButtonUp(0) )
            {
				return true;
			}
			return false;
		}
	    for (int i = 0; i < Input.touches.Length; i++)
	    {
	        if (Input.touches[i].fingerId == touchId)
	        {
	            if (Input.touches[i].phase == TouchPhase.Ended)
	            {
	                return true;
	            }
                return false;
	        }
	    }
		return true;
	}
	
	public static bool EndTouch(){
		if ( Input.GetMouseButtonUp(0) ){
			return true;
		}
	    for (int i = 0; i < Input.touches.Length; i++)
	    {
	        if (Input.touches[i].phase == TouchPhase.Ended)
	        {
	            return true;
	        }
	    }
		return false;
	}
	
}
