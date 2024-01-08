using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour{
    [SerializeField]float edgeSize; 
    [SerializeField]float moveSpeed; 
    [SerializeField]bool centerProbe; 

    Vector3 contactNormal;
    Vector3 velocity;
    Vector3 inputAngles;
    
    void Update(){
        RaycastHit hit;
        if(centerProbe){
            if(Physics.Raycast(CameraManager.GetCamera().ScreenPointToRay(InputManager.screenCenter), out hit, Mathf.Infinity))
                contactNormal = hit.normal;
        }
        else{
            if(Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity)){
                contactNormal = hit.normal;
            }
        }
    }

    void LateUpdate(){
        float speedDelta = moveSpeed * Time.deltaTime;
        velocity = Vector3.zero;

        if(Mathf.Abs(InputManager.cursorRelativePos.x) > Screen.width * 0.5f - edgeSize){
            // Debug.Log(InputManager.cursorRelativePos.x);
            Vector3 xAxis = Mathx.ProjectDirectionOnPlane(transform.right, contactNormal) * Mathf.Sign(InputManager.cursorRelativePos.x);
            velocity += xAxis;
        }

        if(Mathf.Abs(InputManager.cursorRelativePos.y) > Screen.height * 0.5f - edgeSize){
            // Debug.Log(InputManager.cursorRelativePos.y);
            Vector3 yAxis = Mathx.ProjectDirectionOnPlane(transform.up, contactNormal) * Mathf.Sign(InputManager.cursorRelativePos.y);
            velocity += yAxis;
        }

        transform.position += velocity * speedDelta;
    }
}
