using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navmesh{
    NodeFactory nodeFactory;

    public Navnode this[int i]{
        get{
            return navNodes[i];
        }
    }

    public int Count{
        get{
            return navNodes.Count;
        }
    }

    MeshCollider collider;
    public Mesh baseMesh;
    List<Navnode> navNodes = new List<Navnode>();   

    public Navmesh(MeshCollider col){
        nodeFactory = new NodeFactory();

        if(!col.sharedMesh)
            baseMesh.Clear();
            
        collider = col;
        baseMesh = col.sharedMesh;

        navNodes.Clear();   

        for(int i = 0; i < baseMesh.triangles.Length; i+=3){
            Navnode nodeA = nodeFactory.Get(baseMesh.triangles[i]);
            nodeA.pos = col.gameObject.transform.TransformPoint(baseMesh.vertices[baseMesh.triangles[i]]);
            Navnode nodeB = nodeFactory.Get(baseMesh.triangles[i+1]);
            nodeB.pos = col.gameObject.transform.TransformPoint(baseMesh.vertices[baseMesh.triangles[i+1]]);
            Navnode nodeC = nodeFactory.Get(baseMesh.triangles[i+2]);
            nodeC.pos = col.gameObject.transform.TransformPoint(baseMesh.vertices[baseMesh.triangles[i+2]]);

            nodeA.AddTriangle(baseMesh.triangles[i]);
            nodeB.AddTriangle(baseMesh.triangles[i]);
            nodeC.AddTriangle(baseMesh.triangles[i]);

            AddNodes(nodeA, nodeB, nodeC);
        }

        foreach(Navnode node in navNodes){
            foreach(Navnode neighbour in navNodes){
                if(node.SharesTrianglesWith(neighbour) && neighbour != node){
                    if(!node.neighbours.Contains(neighbour)){
                        node.neighbours.Add(neighbour);
                    }
                }
            }
        }
    }

    void AddNodes(params Navnode[] nodes){
        foreach(Navnode node in nodes){
            if(!navNodes.Contains(node)){
                navNodes.Add(node);
            }
        }
    }

    public IEnumerator GetEnumerator(){
        return navNodes.GetEnumerator();
    }
}
