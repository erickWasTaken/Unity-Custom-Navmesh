using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour{
    Navnode[] gizmos;
    Navnode hitNode;

    Navmesh navMesh;

    [SerializeField]LayerMask probeMask;

    void OnEnable(){
        RaycastHit hit;
        Vector3 origin = transform.position;
        origin.y += 0.25f;
        if(Physics.Raycast(origin, -Vector3.up, out hit, Mathf.Infinity, probeMask)){
            navMesh = new Navmesh((MeshCollider)hit.collider);
        }
    }

    public bool FindPath(Vector3 startPos, Vector3 targetPos){
        Navnode startNode = FindClosestNode(startPos);
        Navnode endNode = FindClosestNode(targetPos);

        List<Navnode> openSet = new List<Navnode>();
        HashSet<Navnode> closedSet = new HashSet<Navnode>();

        openSet.Add(startNode);
        return false;

        while(openSet.Count > 0){

        }

    }

    void Update(){
        RaycastHit hit;
        if(Input.GetMouseButtonUp(0)){
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, probeMask)){
                bool pathFound = FindPath(transform.position, hit.point);
                hitNode = FindClosestNode(hit.point);
                Debug.Log(hitNode.nodeIndex);
                Debug.Log("nodeCount = " + navMesh.Count);
                Debug.Log("verticesCount = " + navMesh.baseMesh.vertices.Length);
            }
        }

    }

    public Navnode FindClosestNode(Vector3 pos){
        float minDistance = float.MaxValue;
        Navnode closestNode = null;

        foreach(Navnode node in navMesh){
            float distance = Vector3.Distance(pos, node.pos);

            if(distance < minDistance){
                minDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        if(navMesh == null){
            return;
        }
        
        if(hitNode == null)
            return;
        
        for(int i = 0; i < hitNode.neighbours.Count; i++){
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hitNode.neighbours[i].pos, 0.1f);
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(hitNode.pos, 0.3f);
    }
}
