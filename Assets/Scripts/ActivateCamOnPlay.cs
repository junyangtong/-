using UnityEngine;

namespace Cinemachine.Examples
{

[AddComponentMenu("")] // Don't display in add component menu
public class ActivateCamOnPlay : MonoBehaviour
{
    public CinemachineVirtualCameraBase vcam1;
    public CinemachineVirtualCameraBase vcam2;
    public CinemachineVirtualCameraBase vcam3;

    public Transform cam1;
    public Transform cam2;
    public Transform cam3;
    public Transform playerTransform;
    private float distance1; 
    private float distance2; 
    private float distance3; 


	// Use this for initialization
	void Start () 
    {  
        
        
            if (vcam3)
            {
                vcam3.MoveToTopOfPrioritySubqueue();
            }
        
	}
    void Update(){
        if (cam1 != null && cam2 != null && cam3 != null) {

            Vector3 cam1Pos = cam1.position;
            Vector3 cam2Pos = cam2.position;
            Vector3 cam3Pos = cam3.position;

            Vector3 playerPos = playerTransform.position;

            //判断摄像机与角色之间的距离
            distance1 = Vector3.Distance(cam1Pos, playerPos);
            distance2 = Vector3.Distance(cam2Pos, playerPos);
            distance3 = Vector3.Distance(cam3Pos, playerPos);
            float min = findMin(distance1,distance2,distance3);
            Debug.Log("The max value is: " + min);
            //Debug.Log("Distance between camera1 and player: " + distance1);
            //Debug.Log("Distance between camera2 and player: " + distance2);
            //Debug.Log("Distance between camera3 and player: " + distance3);
            if (min == 3)
            {
                vcam3.Priority = 13;
                vcam2.Priority = 12;
                vcam1.Priority = 11;
            }
            if (min == 2)
            {
                vcam3.Priority = 12;
                vcam2.Priority = 13;
                vcam1.Priority = 11;
            }
            if (min == 1)
            {
                vcam3.Priority = 11;
                vcam2.Priority = 12;
                vcam1.Priority = 13;
            }
            if (vcam1)
            {
                vcam1.MoveToTopOfPrioritySubqueue();
                
            }
        }
    }
        public static float findMin(float distance1 , float distance2 , float distance3)
    {
        float min = 0;
        if (distance1 < distance2 && distance1 < distance3) {
        min = 1;
    } else if (distance2 < distance1 && distance2 < distance3) {
        min = 2;
    } else if (distance3 < distance1 && distance3 < distance2) {
        min = 3; 
    }
    return min;
}

}
}