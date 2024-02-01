using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private Transform Tata;
    [SerializeField]private float RoomHeight = 10f;
    [SerializeField]private float RoomUp = 10f;
    private float currentPosX;
    private float currentPosX2;
    private float currentPosY ;
    private float currentPosY2;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]private int UpNo = 1;

    private void Start()
    {
        currentPosX2 = transform.position.x;
        currentPosX = transform.position.x;
        currentPosY = transform.position.y;
    }

    private void Update()
    {
        
       
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity,speed );  
            
            
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(transform.position.x, currentPosY, transform.position.z), ref velocity,speed );

            if (Tata.transform.position.y>RoomHeight&&UpNo == 1)
            {
                MoveToNewUp(RoomUp);
                UpNo--;
            }
            else if(Tata.transform.position.y<RoomHeight&&UpNo == 0)
            {
                MoveToNewUp(-RoomUp);
                UpNo++;
            }
     
    }

    public void MoveToNewRoom(float _newRoom )
    {
            
            currentPosX = currentPosX2+_newRoom;
            currentPosX2 = currentPosX;
    
    }
    public void MoveToNewUp(float _newRoom )
    {
            
        currentPosY = currentPosY2+_newRoom;
        currentPosY2 = currentPosY;
        

    }
}
