using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navnode{
    public int nodeIndex{
        get{
            return _nodeIndex;
        }
    }

    public Vector3 pos{
        get{
            return _pos;
        }
        set{
            _pos = value;
        }
    }

    Vector3 _pos;
    int _nodeIndex;

    public List<Navnode> neighbours = new List<Navnode>();
    public List<int> connectedTriangles = new List<int>();

    public float fScore{
        get{
            return hScore + gScore;
        }
    }

    public float hScore;
    public float gScore;
    public int parentId;

    public Navnode(int index){
        _nodeIndex = index;
    }

    public Navnode(Vector3 pos){
        this.pos = pos;
    }

    public void AddNeighbour(Navnode neighbour){
        if(!neighbours.Contains(neighbour) && neighbour != this){
            neighbours.Add(neighbour);
        }
    }

    public void AddTriangle(int triangle){
        if(!connectedTriangles.Contains(triangle)){
            connectedTriangles.Add(triangle);
        }
    }

    public bool SharesTrianglesWith(Navnode neighbour){
        foreach(int triangle in connectedTriangles){
            if(neighbour.connectedTriangles.Contains(triangle))
                return true;
        }

        return false;
    }
}