using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFactory{
    
    List<Navnode> nodeList = new List<Navnode>();

    public Navnode Get(int nodeIndex){
        foreach(Navnode node in nodeList){
            if(node.nodeIndex == nodeIndex){
                return node;
            }
        }
        nodeList.Add(new Navnode(nodeIndex));
        return nodeList[nodeList.Count-1];
    }
}