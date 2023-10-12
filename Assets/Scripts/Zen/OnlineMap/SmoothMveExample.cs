using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMveExample : MonoBehaviour
{
    public static SmoothMveExample Instance;

    public SmoothMveExample()
    {
        Instance = this;
    }

    public bool isMovement;

    private int moveZoom;

    private float _Time = 3;
    private float Angle;  


    private double fromTileX, fromTileY, toTileX, toTileY;   

    private Vector2 fromPosition;
    private Vector2 toPosition;

    public void PushToMove(float time, Vector2 from, Vector2 to, int zoom)
    {
        _Time = time;

        //fromPosition = OnlineMaps.instance.position;
        fromPosition = from;

        //toPosition = OnlineMapsLocationService.instance.position;
        toPosition = to;

        //moveZoom = OnlineMaps.instance.zoom;
        moveZoom = zoom;
        gameObject.GetComponent<OnlineMaps>().projection.CoordinatesToTile(fromPosition.x, fromPosition.y, moveZoom, out fromTileX, out fromTileY);
        gameObject.GetComponent<OnlineMaps>().projection.CoordinatesToTile(toPosition.x, toPosition.y, moveZoom, out toTileX, out toTileY);

        if (OnlineMapsUtils.Magnitude(fromTileX, fromTileY, toTileX, toTileY) < 4)
        {
            Angle = 0;

            isMovement = true;
        }
        else
        {
            gameObject.GetComponent<OnlineMaps>().position = toPosition;
        }
    }

    private void Update()
    {
        if (!isMovement)
        {
            return;
        }

        Angle += Time.deltaTime / _Time;

        if (Angle > 1)
        {
            isMovement = false;
            Angle = 1;
        }

        double px = (toTileX - fromTileX) * Angle + fromTileX;
        double py = (toTileY - fromTileY) * Angle + fromTileY;
        gameObject.GetComponent<OnlineMaps>().projection.TileToCoordinates(px, py, moveZoom, out px, out py);
        gameObject.GetComponent<OnlineMaps>().SetPosition(px, py);
    }
}
