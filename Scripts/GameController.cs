using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{
    public static GameController Instance;

    public Vector3 ScreenCenter{
        get{
            return new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
        }
    }

    public Vector3 CursorPos{
        get{
            return Input.mousePosition;
        }
    }

    [SerializeField] float boxDragThreshold;

    Vector3 lastClickPos = Vector3.zero;
    float heldTime = 0f;
    bool boxDrag;
    RaycastHit hit;

    List<Selectable> selectionList = new List<Selectable>();

    void Awake(){
        Instance = this;
    }

    void Update(){
        if(Input.GetMouseButton(0)){
            if(heldTime == 0f){
                lastClickPos = CursorPos + ScreenCenter;
            }
            else if(((CursorPos - ScreenCenter) - CursorPos).magnitude > boxDragThreshold){
                boxDrag = true;
            }
            heldTime += Time.deltaTime;
        }

        else if(Input.GetMouseButtonUp(0)){
            if(Physics.Raycast(Camera.main.ScreenPointToRay(CursorPos), out hit, Mathf.Infinity)){
                var target = hit.collider.gameObject.GetComponent<Selectable>();    

                if(target != null){
                    target.Toggle();
                    selectionList.Add(target);
                }
            }
            else{
                for(int i = 0; i < selectionList.Count; i++){
                    selectionList[i].Toggle();
                }
                selectionList.Clear();
            }
            heldTime = 0f;
        }
    }
    
    void OnDrawGizmos(){
        
    }
}