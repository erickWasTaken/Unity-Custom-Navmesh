using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour{
    public float moveSpeed{
        get{
            return _moveSpeed;
        }
    }

    [SerializeField]float _moveSpeed;

    MoveTo movementModule;
    Astar pathFinder;//temporarily attached to game object
    RaycastHit hit;
    [SerializeField]LayerMask probeMask;
    float closeThreshold = 0.125f;

    void OnEnable(){
        pathFinder = gameObject.GetComponent<Astar>();//temporarily attached to game object
        movementModule = new MoveTo();
        movementModule.SetTarget(transform.position);
    }

    void Update(){
        if(Input.GetMouseButtonUp(0)){
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, probeMask)){
                // bool pathFound = pathFinder.FindPath(transform.position, hit.point);
                // pathFinder.hitNode = pathFinder.FindClosestNode(hit.point);
                pathFinder.hitNode = pathFinder.FindClosestNode(transform.position);
            }
        }
        if(Vector3.Distance(transform.position, hit.point) < closeThreshold){
            return;
        }
        movementModule.SetTarget(hit.point);
        movementModule.BehaviourUpdate(this);
    }
}

