using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo{
    Vector3 target;
    Vector3 prevPosition;
    Vector3 inputDirection;

    public void BehaviourUpdate(Unit unit){
        RaycastHit hit;
        if(Physics.Raycast(unit.transform.position, -Vector3.up, out hit, Mathf.Infinity)){
            Vector3 groundNormal = hit.normal;
            float speedDelta = unit.moveSpeed * Time.deltaTime;
            Vector3 xAxis = Mathx.ProjectDirectionOnPlane(unit.transform.right, groundNormal);
            Vector3 zAxis = Mathx.ProjectDirectionOnPlane(unit.transform.forward, groundNormal);
            Debug.DrawLine(unit.transform.position, target);
            unit.transform.position += ((xAxis * (target.x - unit.transform.position.x) + zAxis * (target.z - unit.transform.position.z))).normalized * speedDelta;
        }
    }

    public void SetTarget(Vector3 point){
        target = point;
        inputDirection = target - prevPosition;
    }
}