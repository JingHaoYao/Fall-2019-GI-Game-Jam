using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectInTime : MonoBehaviour
{
    public float destroyTime;

    void Start()
    {
        Destroy(this.gameObject, destroyTime);    
    }
}
