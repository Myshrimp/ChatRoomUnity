using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private GameObject SendBox;
    [SerializeField] private GameObject ConnectBox;
    [SerializeField] private TextMeshProUGUI sendbox_state;
    [SerializeField] private TextMeshProUGUI connectbox_state;
    private bool sendbox_ = true;
    private bool connectbox_ = true;
    public string image_url;
    public void DisplayImageFromURL()
    {
        StartCoroutine(nameof(loadNetWork));
    }
    public void SwitchSendBoxState()
    {
        if(sendbox_)
        {
            SendBox.SetActive(false);
            sendbox_ = false;
            return;
        }
        else
        {
            SendBox.SetActive(true);
            sendbox_= true;
        }   
    }
    public void SwitchConnectBoxState()
    {
        if (connectbox_)
        {
            ConnectBox.SetActive(false);
            connectbox_ = false;
            connectbox_state.text = "Show";
            return;
        }
        else
        {
            ConnectBox.SetActive(true);
            connectbox_ = true;
            connectbox_state.text = "Hide";
        }
    }

    IEnumerator loadNetWork()
    {
        print("Displaying img from:" + image_url);
        if (img.texture == null)
        {
            WWW image = new WWW(image_url);
            /*while (!date.isDone) 
			{
				yield return null;
				Debug.Log ("时间：" + Time.time + " " + "进度：" + date.progress);
			}*/
            yield return image;
            img.texture = image.texture;
        }
    }
}
