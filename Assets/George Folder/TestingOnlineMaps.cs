using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingOnlineMaps : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnlineMapsProjectionWGS84 b = new OnlineMapsProjectionWGS84();
        double x, y;
        b.CoordinatesToTile(34.03550, -117.652, 17, out x, out y);
        Debug.Log(x + " " + y);
        OnlineMapsProjectionSphericalMercator c = new OnlineMapsProjectionSphericalMercator();
        c.CoordinatesToTile(34.03550, -117.652, 17, out x, out y);
        Debug.Log(x + " " + y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
