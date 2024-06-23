using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Center_Source : MonoBehaviour,center_
{
    //
    public static Center_Source instance { get; private set; }
    public GameObject unless;//±»½ûÓÃµÄÍ¼Æ¬
    public string name_ai="AI";
    public Sprite sprite_ai;
    public string name_player="Xiaoxia";
    public Sprite sprite_player;
    public TMP_InputField username_IpF;

    //
    //
    //
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName()
    {
        name_player=username_IpF.text;
    }
    public void SetName(string name)
    {
        name_player = name;
    }
}
