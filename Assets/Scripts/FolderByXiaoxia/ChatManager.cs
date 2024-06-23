using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FishNet;
using FishNet.Broadcast;
using FishNet.Connection;
using System;
using FishNet.Transporting;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;
    public Action<Message,Channel> OnSendMessage;
    public Action<NetworkConnection,Message,Channel> OnClientSendMsg;
    [SerializeField] private GameObject textObject;
    [SerializeField] private TMP_InputField user_text_inputField;
    [SerializeField] private Transform chatHolder;
    private List<GameObject> user_text_collection = new List<GameObject>();
    private void OnEnable()
    {
        OnSendMessage = OnSendMessageBroadcast;
        OnClientSendMsg = OnClientSendMessageBroadcast;
        InstanceFinder.ClientManager.RegisterBroadcast<Message>(OnSendMessage);
        InstanceFinder.ServerManager.RegisterBroadcast<Message>(OnClientSendMsg);
    }
    private void OnDisable()
    {
        InstanceFinder.ClientManager.UnregisterBroadcast<Message>(OnSendMessage);
        InstanceFinder.ServerManager.UnregisterBroadcast<Message>(OnClientSendMsg);
    }
    private void Awake()
    {
        instance = this;
    }
    public void PutTextOnPanel()
    {
        Message msg;
        msg.text = user_text_inputField.text;
       if(InstanceFinder.IsServer)
        {
            InstanceFinder.ServerManager.Broadcast(msg);
        }
        else if(InstanceFinder.IsClient)
        {
            InstanceFinder.ClientManager.Broadcast(msg);
        }
       
    }
    private void OnSendMessageBroadcast(Message message,Channel cn=Channel.Unreliable)
    {
        string text = message.text;
        if (text.Length > 0)
        {
            GameObject textOb = Instantiate(textObject, chatHolder);
            textOb.GetComponent<TextMeshProUGUI>().text = text;
            user_text_collection.Add(textOb);
        }
    }
    private void OnClientSendMessageBroadcast(NetworkConnection conn,Message message,Channel cn=Channel.Unreliable)
    {
        InstanceFinder.ServerManager.Broadcast(message);
    }
    public struct Message : IBroadcast
    {
        public string text;
    }

}

