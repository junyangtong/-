using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mingxiang : Interactive
{
    private DialogueController dialogueController;
    public GameObject Partical;

    

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    
    protected override void OnClickedAction()
    {
        // 播放使用物品的动画
        Partical.SetActive(true);
        Debug.Log("进入冥想状态");
    }
}