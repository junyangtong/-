//实现缓慢跟踪某个物体的效果
using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    private Transform target;
    public Vector3 offset;
    public float speed;
    
    public float backDistance = 2;
    public float topDistance = 2;

    
    void LateUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();//Target是MainCamera的子物体，作为跟踪目标

        offset = -target.right * backDistance + target.up * topDistance;
        
        transform.position = Vector3.Lerp(transform.position, target.position + offset,Time.deltaTime*speed);
        
        transform.rotation = target.rotation;
    }
}
