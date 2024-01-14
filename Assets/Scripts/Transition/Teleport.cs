using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
//[SceneName]public string sceneFrom;
[SceneName]public string sceneToGo;

private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("进入碰撞区域");
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("按下E");
            SceneManager.LoadScene(sceneToGo);
        }
    }

}
