using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : MonoBehaviour
{
    [SerializeField]private float RoomSize = 20f;
    [SerializeField]private float RoomHeight = 10f;
    [SerializeField]private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" )
        {
            if (collision.transform.position.y < RoomHeight)
            {
                cam.MoveToNewUp(RoomSize);
            }
            else
            {
                cam.MoveToNewUp(-RoomSize);
            }
        }
    }
}
