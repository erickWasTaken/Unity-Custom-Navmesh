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

    public int parentId{
        get{
            return _parentId;
        }
        set{
            _parentId = value;
        }
    }

    public int fScore{
        get{
            return _fScore;
        }
        set{
            _fScore = value;
        }
    }

    public int hScore{
        get{
            return _hScore;
        }
        set{
            _hScore = value;
        }
    }

    public int gScore{
        get{
            return _gScore;
        }
        set{
            _gScore = value;
        }
    }

    int _fScore;
    int _hScore;
    int _gScore;
    int _parentId;

    public Navnode(int index){
        _nodeIndex = index;
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