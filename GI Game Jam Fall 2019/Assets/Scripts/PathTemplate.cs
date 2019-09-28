using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTemplate : MonoBehaviour
{
    public Path[] paths;
    public Vector3 startPosition;
    
    void setPrevNext(Path path)
    {
        for (int i = 0; i < path.path.Length; i++)
        {
            if (i != path.path.Length - 1)
            {
                path.path[i].nextTile = path.path[i + 1];
            }

            if (i != 0)
            {
                path.path[i].prevTile = path.path[i - 1];
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Path path in paths)
        {
            setPrevNext(path);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
