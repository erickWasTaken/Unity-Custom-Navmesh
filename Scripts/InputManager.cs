using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager{    
    public static Vector3 cursorPos{
        get{
            return Input.mousePosition;
        }
    }
    
    public static Vector3 screenCenter{
        get{
            return new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
        }
    }

    public static Vector3 cursorRelativePos{
        get{
            return cursorPos - screenCenter;
        }
    }
}