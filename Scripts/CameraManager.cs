using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager{
    List<Camera> cameraSources = new List<Camera>();

    public static Camera GetCamera(){
        return Camera.main;
    }
}