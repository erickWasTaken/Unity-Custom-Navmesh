using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour{
    List<Navnode> path = new List<Navnode>();
    public Navnode hitNode;

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
            currentNode.gScore = Vector3.Distance(currentNode.pos, startNode.pos);
            currentNode.hScore = Vector3.Distance(currentNode.pos, endNode.pos);

            foreach(Navnode node in openSet){
                node.gScore = Vector3.Distance(node.pos, startNode.pos);
                node.hScore = Vector3.Distance(node.pos, endNode.pos);

                if(node.fScore < currentNode.fScore || node.fScore == currentNode.fScore && node.hScore < currentNode.hScore){
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

                Navnode currentNeighbour = currentNode.neighbours[i];
                currentNeighbour.gScore = Vector3.Distance(currentNode.neighbours[i].pos, startNode.pos);
                currentNeighbour.hScore = Vector3.Distance(currentNode.neighbours[i].pos, endNode.pos);

                if(currentNeighbour.hScore < currentNode.hScore || !closedSet.Contains(currentNeighbour)){
                    currentNeighbour.parentId = currentNode.nodeIndex;
                    if(!openSet.Contains(currentNeighbour))
                        openSet.Add(currentNeighbour);
                }
            }

        }
        
        return false;
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
        // if(navMesh == null){
        //     return;
        // }
        
        if(hitNode == null)
            return;
        
        // Gizmos.color = Color.red;
        // for(int i = 0; i < path.Count; i++){
        //     Gizmos.DrawSphere(path[i].pos, 0.1f);
        //     if(i > 0)
        //         Gizmos.DrawLine(path[i].pos, path[i-1].pos);
        // }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(hitNode.pos, 0.3f);
    }
}
