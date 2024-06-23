using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Center_Put : MonoBehaviour,center_
{
    public static Center_Put instance { get; private set; }

    public Value_giver model;//����ָ��0 ����  1  ͼƬ
    public Value_giver wordCounts;//
    public Value_giver wordStyle;
    public Value_giver imageSize;
    public Value_giver imageStyle;

    public string output;//�ı�����
    public string input;//�ı����

    public Toggle ai_use;
    public bool AI_HasAT = true;
    public string AI_Input_String;
    public int local = 1;
    public Texture  AI_Input_Image;
    private bool AI_Texture_Received=false;
    public void AI_Input(string s) {
        if (AI_Input_Type == 0) AI_Input_String = s;
      
    }
    public void AI_Input(Texture t)
    {
        if (AI_Input_Type == 1) AI_Input_Image = t;
        print("AI Iput texture ready!");
        AI_Texture_Received = true;
    }

    public event Action<string> AI_Output;
    
    public int AI_Input_Type;

    public string toString(bool ai) {
        AI_HasAT = ai;
        if (AI_HasAT)
        {
            output = " @AI\nΪ������" +
                (model.giver_pointer == 0 ? wordCounts.toString() : imageSize.toString()) + " , " +
                (model.giver_pointer == 0 ? wordStyle.toString() : imageStyle.toString()) + " , " +
                "ʹ��" + model.toString() + "��ʽ�ظ��ң����ľ�������Ϊ" + input;
            Debug.Log("~~~�������ݣ�" + output);
            AI_Input_Type = model.giver_pointer;
            AI_Input_String = "";
            AI_Input_Image = null;
            AI_Output(output);            
        }
        else
        {
            output = input;
        }
        return output;
    }

    public float getValue(string type) {
        if (type.Equals("����")||type.Equals("wordCounts"))
        {
            return wordCounts.getValue(true);
        }
        else if (type.Equals("��С") || type.Equals("imageSize"))
        {
            return imageSize.getValue(true);
        }
        else if (type.Equals("���ַ��") || type.Equals("wordStyle"))
        {
            return wordStyle.getValue(true);
        }
        else if (type.Equals("ͼƬ���") || type.Equals("imageStyle"))
        {
            return imageStyle.getValue(true);
        }
        


            return 0; 
    }
    public void a(string s) { }
    public string getStr(string type)
    {
        if (type.Equals("����") || type.Equals("wordCounts"))
        {
            return wordCounts.getStr(true);
        }
        else if (type.Equals("��С") || type.Equals("imageSize"))
        {
            return imageSize.getStr(true);
        }
        else if (type.Equals("���ַ��") || type.Equals("wordStyle"))
        {
            return wordStyle.getStr(true);
        }
        else if (type.Equals("ͼƬ���") || type.Equals("imageStyle"))
        {
            return imageStyle.getStr(true);
        }

        return "";
    }

    public bool testback()
    {
        if (AI_HasAT)
        {
            if (!AI_Input_String.Equals(""))
            {
                return true;
            }
            else if (AI_Input_Image != null)
            {
                return true;
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        AI_Output += a;
    }

    // Update is called once per frame
    void Update()
    {
           if (AI_Texture_Received)
           {
                //�ɹ��������,set �������� 
                News ne = Center_News.instance.addNews(false);
                ne.set();
                ai_manager.instance.RequestingFromURL = false;
                AI_Texture_Received = false;
           }    
    }

}
