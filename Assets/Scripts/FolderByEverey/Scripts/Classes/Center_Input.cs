using System;
using System.Collections;
using System.Collections.Generic;
using FishNet;
using FishNet.Broadcast;
using TMPro;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Transporting;

public class Center_Input : MonoBehaviour, center_
{
    public static Center_Input instance { get; private set; }

    public bool  isWorking;
    public float maxGetTime = 6;
    public float hasTime;
    public TMP_InputField input_;
    public bool show_prompt_description=false;
    public bool send_message_from_ipf = false;
    private string origin_text;
    private Action<Message,Channel> OnServerSend;
    private Action<NetworkConnection,Message,Channel> OnClientSend;
       // Start is called before the first frame update
    private void OnEnable()
    {
        OnServerSend += OnServerSendMessage;
        OnClientSend += OnClientSendMessage;
        InstanceFinder.ClientManager.RegisterBroadcast<Message>(OnServerSend);
        InstanceFinder.ServerManager.RegisterBroadcast<Message>(OnClientSend);
    }
    private void OnDisable()
    {
        InstanceFinder.ClientManager.UnregisterBroadcast<Message>(OnServerSend);
        InstanceFinder.ServerManager.UnregisterBroadcast<Message>(OnClientSend);
    }
    void Awake()
    {
        instance = this;
        if (input_ != null)
        {
            origin_text = input_.text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessage();
        }
    }
    public void SendMessage()
    {
        if (input_.text.Equals(origin_text))
        {
            Debug.Log("请输入一定内容再发送");
            return;
        }
        print("Sending message");
        //Constructing msg
        send_message_from_ipf = true;
        Message msg= new Message();
        msg.message=input_.text;
        msg.isAi=Center_Put.instance.ai_use.isOn;
        msg.show_description = show_prompt_description;
        msg.username = Center_Source.instance.name_player;
        msg.giver_pointer=Center_Put.instance.model.giver_pointer;
        Debug.Log("Giver pointer:"+msg.giver_pointer.ToString());
        string cmd = "";
        if(msg.giver_pointer==0)
        {
            cmd = "gpt";
        }
        if (msg.giver_pointer==1)
        {
            cmd = "t2i";
        }
        if (msg.isAi)
        {
            msg.message = cmd + msg.message;
            print("Sending msg:"+msg.message);
            Center_Put.instance.input = msg.message;
            Center_Put.instance.toString(msg.isAi);
            UdpSocket.instance.SendData(msg.message);
            msg.message = Center_Put.instance.output;
            if(msg.giver_pointer==0)
            {
                StartCoroutine(WaitForGPT(msg));
            }
            if(msg.giver_pointer==1)
            {
                StartCoroutine(WaitForPythonResponse(msg));
            }       
        }
        else
        {
            if (InstanceFinder.IsServer)
            {
                InstanceFinder.ServerManager.Broadcast(msg);
            }
            else if (InstanceFinder.IsClient)
            {
                InstanceFinder.ClientManager.Broadcast(msg);
            }
        }      
        //Constructing msg    
    }
    public void WelcomeMessage(Message msg)
    {
        print("Sending welcome message"); 
        if (InstanceFinder.IsServer)
        {
            InstanceFinder.ServerManager.Broadcast(msg);
        }
        else if (InstanceFinder.IsClient)
        {
            InstanceFinder.ClientManager.Broadcast(msg);
        }   
    }
    public void stopWork()
    {
        isWorking = false;

    }
    private void OnServerSendMessage(Message msg,Channel cn=Channel.Unreliable)
    {
        print("Receiving message from server");
        print("Requesting type:" + msg.giver_pointer.ToString());
        if (!msg.isAi)
        {
            //Center_Put.instance.AI_Input_Type = 0;
            Center_Put.instance.input = msg.message;
            Center_Put.instance.toString(msg.isAi);
            Center_News.instance.addNews(true).set(msg.username);
        }
        if (msg.isAi)
        {
            Center_Put.instance.AI_Input_Type = msg.giver_pointer;
            if(msg.giver_pointer==1)//text2img
            {
                ai_manager.instance.StartRequest(msg.url);
            }      
            if(msg.giver_pointer==0)//gpt
            {
                Center_Put.instance.AI_Input_String = msg.gpt_response;
                News ne = Center_News.instance.addNews(false);
                ne.set();
            }
            if(msg.show_description)//show description of image asked for AI
            {
                Center_Put.instance.input = msg.message;
                Center_Put.instance.toString(false);
                Center_News.instance.addNews(true).set("AI");
            }                       //                    
            hasTime = maxGetTime;
            isWorking = true;
            Debug.Log("NOW开始工作，等待ai返回，\n最大等待时间：" + hasTime + "秒");
        }   
    }
    private void OnClientSendMessage(NetworkConnection conn,Message msg, Channel cn = Channel.Unreliable)
    {
        print("receiving message from client");
        InstanceFinder.ServerManager.Broadcast(msg);
    }
    public struct Message : IBroadcast
    {
        public string message;
        public bool isAi;
        public string url;
        public bool show_description;
        public string username;
        public int giver_pointer;
        public string gpt_response;
    }
    IEnumerator WaitForPythonResponse(Message msg)
    {
        while(true)
        {
            bool ready = false;
            if (UdpSocket.instance.isTxStarted)
            {
                ready = true;
                UdpSocket.instance.isTxStarted = false;
            }
            yield return 0;
            if (ready)
            {
                msg.url = UdpSocket.instance.response;
                Debug.Log("Response from python:" + UdpSocket.instance.response);
                if (InstanceFinder.IsServer)
                {
                    InstanceFinder.ServerManager.Broadcast(msg);
                }
                else if (InstanceFinder.IsClient)
                {
                    InstanceFinder.ClientManager.Broadcast(msg);
                }
                StopCoroutine(WaitForPythonResponse(msg));
                break;
            }
            else
            {
                Debug.Log("Waiting for response");
            }
        }
        
    }
    IEnumerator WaitForGPT(Message msg)
    {
        while (true)
        {
            bool ready = UdpSocket.instance.isTxStarted;
            yield return 0;
            if (ready)
            {
                UdpSocket.instance.isTxStarted=false;
                msg.gpt_response = UdpSocket.instance.response;
                if (InstanceFinder.IsServer)
                {
                    InstanceFinder.ServerManager.Broadcast(msg);
                }
                else if (InstanceFinder.IsClient)
                {
                    InstanceFinder.ClientManager.Broadcast(msg);
                }
                StopCoroutine(WaitForGPT(msg));
                break;
            }
            else
            {
                print("Waiting for gpt");
            }
        }
    }
}
