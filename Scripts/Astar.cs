using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour{
    List<Navnode> path = new List<Navnode>();
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

        while(openSet.Count > 0){
            Navnode currentNode = openSet[0];
            float currentFCost = Vector3.Distance(currentNode.pos, startNode.pos) + Vector3.Distance(currentNode.pos, endNode.pos);

            foreach(Navnode node in openSet){
                float fCost = Vector3.Distance(node.pos, startNode.pos) + Vector3.Distance(node.pos, endNode.pos);

                if(fCost < currentFCost || fCost == currentFCost && Vector3.Distance(node.pos, endNode.pos) < Vector3.Distance(currentNode.pos, endNode.pos)){
                    currentNode = node;
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == endNode){
                RetracePath(startNode, endNode);
                return true;
            }

            for(int i = 0; i < currentNode.neighbours.Count; i++){
                if(closedSet.Contains(currentNode.neighbours[i]))
                    continue;

                float hCost = Vector3.Distance(currentNode.neighbours[i].pos, endNode.pos);
                if(hCost < Vector3.Distance(currentNode.pos, endNode.pos) || !closedSet.Contains(currentNode.neighbours[i])){
                    currentNode.neighbours[i].fScore = currentNode.fScore;
                    currentNode.neighbours[i].parentId = currentNode.nodeIndex;
                    if(!openSet.Contains(currentNode.neighbours[i]))
                        openSet.Add(currentNode.neighbours[i]);
                }
            }

        }
        
        return false;
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

    void RetracePath(Navnode startNode, Navnode endNode){
        path = new List<Navnode>();   
        Navnode currentNode;

        currentNode = endNode;
        while(currentNode != startNode){
            path.Add(currentNode);
            currentNode = navMesh[currentNode.parentId];
        }

        path.Reverse();
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
        
        Gizmos.color = Color.red;
        for(int i = 0; i < path.Count; i++){
            Gizmos.DrawSphere(path[i].pos, 0.1f);
            if(i > 0)
                Gizmos.DrawLine(path[i].pos, path[i-1].pos);
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(hitNode.pos, 0.3f);
    }
}
