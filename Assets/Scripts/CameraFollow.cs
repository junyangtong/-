using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;
    public float threshold = 118f;
    public float cammoveDistance = 170f;
    public float playermoveDistance = 0.5f;
    public float smoothTime = 0.02f; // 平滑时间
    private float delayTime ; // 无敌时间
    private float distance; 
    Vector2 screenCenter = new Vector2 (Screen.width/2,Screen.height/2);

    private Vector3 c_targetPos;
    void Update()
    {
        
        if (cameraTransform != null && playerTransform != null) {
      
            Vector3 cameraPos = cameraTransform.position;
            cameraPos.y = 0; // 将 y 坐标设为 0，即将相机位置投影到 x-z 平面上

            Vector3 playerPos = playerTransform.position;
            playerPos.y = 0; // 将 y 坐标设为 0，即将角色位置投影到 x-z 平面上

            distance = Vector3.Distance(cameraPos, playerPos);
            //判断摄像机与角色之间的距离
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraToPlayer = playerTransform.position - cameraTransform.position;
            Vector3 cross = Vector3.Cross(cameraForward, cameraToPlayer);
            distance = distance * cross.y;
            Debug.Log("Distance between camera and player: " + distance);


            delayTime = -1f;
            //通过阈值判断是否移动
            if (Mathf.Abs(distance) > threshold  && delayTime<= 0) { // 当距离大于阈值时
                float direction = distance > 0 ? 1f : -1f; // 判断向左还是向右移动
                Vector3 c_moveVector = direction * cammoveDistance * Vector3.right; // 计算相机移动的距离向量
                //Vector3 p_moveVector = direction * playermoveDistance * Vector3.right; // 计算角色移动的距离向量
                c_targetPos = cameraTransform.position + c_moveVector; // 计算相机移动的目标位置
                //Vector3 p_targetPos = playerTransform.position + p_moveVector; // 计算角色移动的目标位置
                delayTime = 1f;
                do
                {
                    cameraTransform.position = Vector3.Lerp(cameraTransform.position, c_targetPos, smoothTime); // 平滑移动相机
                } while (cameraTransform.position != c_targetPos);
            }
            // else if(cameraTransform.position != c_targetPos)
            // {
            //     delayTime = -1f;
            //     cameraTransform.position = Vector3.Lerp(cameraTransform.position, c_targetPos, smoothTime); // 平滑移动相机
            //     //playerTransform.position = Vector3.Lerp(playerTransform.position, p_targetPos, smoothTime); // 移动角色
            //     
            // }
            
    }
}
}
