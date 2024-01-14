using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI���")]
    public Text textLabel;
    public Image faceImage;

    [Header("�ı��ļ�")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("ͷ��")]
    public Sprite face01, face02;


    bool textFinished;
    bool haveWater;

    List<string> textList = new List<string>();
    void Awake()
    {
        GetTextFromFile(textFile);
    }
    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.E)&&textFinished==true)
        {
            //textLabel.text = textList[index];
            //index++;
            StartCoroutine(SetTextUI());

        }
    }
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var LineData = file.text.Split('\n');

        foreach(var line in LineData)
        {
            textList.Add(line);
        }
    }
    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        switch (textList[index])
        {
            case "A\r":

                faceImage.sprite = face01;

                index++;

                break;

            case "B\r":
                faceImage.sprite = face02;

                index++;

                break;

        }

        for(int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];

            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;
    }
}
