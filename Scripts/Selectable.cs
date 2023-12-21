using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour{
    public bool Active{
        get{
            return active;
        }
    }

    bool active;
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 indicatorOffset;

    GameObject indicator;

    public bool Toggle(){
        if(!indicator){
            indicator = Instantiate(prefab);
            indicator.transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
            indicator.transform.SetParent(transform);
        }
        active = !active;
        Debug.Log(active);
        indicator.SetActive(active);
        return active;
    }
}