using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AIManager : MonoBehaviour
{
    [SerializeField] TMP_InputField prompt_ipf;
    [SerializeField] RawImage DisplayAIImage_rmg;
    private string url;
    public void SendPromptText2Img()
    {
        string prompt = prompt_ipf.text;
        UdpSocket.instance.SendData(prompt);
        print("Prompt sent!");
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if(UdpSocket.instance != null)
        {
            if(UdpSocket.instance.isTxStarted) {
                StartRequest(UdpSocket.instance.response);
                UdpSocket.instance.isTxStarted = false;
            }
        }
    }
    private void StartRequest(string url)
    {
        print("Starting request");
        StartCoroutine(StartRequestForImg(url));
    }
    IEnumerator StartRequestForImg(string url)
    {
        WWW www = new WWW(url); 
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            // 获取加载的纹理
            Texture texture = www.texture;
            DisplayAIImage_rmg.texture = texture;
            StopCoroutine(StartRequestForImg(url));
        }
        else
        {
            Debug.LogError("Failed downloading" + www.error);
        }
    }
}
