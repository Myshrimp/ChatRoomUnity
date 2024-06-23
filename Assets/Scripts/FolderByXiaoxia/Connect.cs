using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FishNet.Transporting.Tugboat;
using FishNet.Transporting;
using FishNet.Example;
using System;

public class Connect : MonoBehaviour
{
    public StartType start_type;
    public TMP_InputField input;
    public Tugboat tgb;
    public NetworkHudCanvases networkHudCanvases;
    public bool hasCloudServer = false;
    private void Start()
    {
        if(hasCloudServer)
        {
            if (start_type == StartType.Host)
            {
                networkHudCanvases.OnClick_Server();
                networkHudCanvases.OnClick_Client();
            }
            if (start_type == StartType.Server)
            {
                networkHudCanvases.OnClick_Server();
            }
            if (start_type == StartType.Client)
            {
                networkHudCanvases.OnClick_Client();
            }
        }     
    }
    public void SetIPV4()
    {
        string ip = input.text;
        tgb.SetClientAddress(ip);
        networkHudCanvases.OnClick_Client();
    }
    public void LocalhostTest()
    {
        string ip = "127.0.0.1";
        tgb.SetClientAddress(ip);
        networkHudCanvases.OnClick_Server();
        networkHudCanvases.OnClick_Client();
    }
    public void ConnectToLocalServer()
    {
        string ip = "127.0.0.1";
        tgb.SetClientAddress(ip);
        networkHudCanvases.OnClick_Client();
    }
    [Serializable]
    public enum StartType
    {
        Host,Client,Server
    }
}
