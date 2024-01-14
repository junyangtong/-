using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour
{
    public GameObject Button;
    public GameObject TalkUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Button.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
    }



    void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            TalkUI.SetActive(true);
        }
    }
}
