using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;
using System;
using UnityEngine.UIElements;

public class ai_manager : MonoBehaviour
{
    public static ai_manager instance;
   // public Requests current_request = Requests.Txt2Img;
  //  [SerializeField] RawImage DisplayAIImage_rmg;
  //  [SerializeField] TMP_Dropdown dropdown;
    private string url;
    public bool RequestingFromURL = false;
    private void Start()
    {
        instance = this;
        Center_Put.instance.AI_Output += SendPromptText2Img;
    }
    public void ChangeAIResponseMode()
    {
        /*
        switch (dropdown.value)
        {
            case 0:
                No_Response();
                print("Current mode:No_response");
                break;
            case 1:
                Txt2Img();
                print("Current mode:txt2img");
                break;
            case 2:
                Txt2Audio();
                print("Current mode:txt2audio");
                break;
            default:
                break;
        }
        */
    }
    public void SendPromptText2Img(string s)
    {
        UnityEngine.Debug.Log("输出"+s);
        UdpSocket.instance.SendData(s);
        print("Prompt sent!");
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
    }
    public void StartRequest(string request)
    {
        RequestingFromURL = true;
        print("Starting request:"+request);
        StartCoroutine(StartRequestForImg(request));
    }
    IEnumerator StartRequestForImg(string url)
    {
        if (Center_Put.instance.AI_Input_Type == 1)
        {
            WWW www = new WWW(url);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                // 获取加载的纹理
                Texture texture = www.texture;
                Center_Put.instance.AI_Input(texture);
                StopCoroutine(StartRequestForImg(url));
            }
            else
            {
                //  Debug.LogError("Failed downloading" + www.error);
            }
        }else if (Center_Put.instance.AI_Input_Type == 0)
        {
            Center_Put.instance.AI_Input(url);
        }
    }
}

