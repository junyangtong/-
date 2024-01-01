using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float moveRate;
    private float startPointX;
    private float camStartX; 
    private float startPointY;
    private float camStartY;

    public bool LockY;
    void Start()
    {
        startPointX = transform.position.x;
        camStartX = Cam.transform.position.x;
    }

    void Update()
    {
        if (LockY)
        {
            transform.position = new Vector3(startPointX + (Cam.position.x - camStartX) * moveRate, transform.position.y,transform.position.z);
        }
        else
        {
            transform.position = new Vector3(startPointX + (Cam.position.x - camStartX) * moveRate, startPointY+(Cam.position.y-camStartY)*moveRate, transform.position.z);
        }
    }
}
