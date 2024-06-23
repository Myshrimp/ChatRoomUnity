using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class News : MonoBehaviour,news_
{
    public bool isPlayer;
    public TextMeshProUGUI news_name;//名字
    public Image news_image;//头像
    public Canvas news_message;//信息message
    public TextMeshProUGUI news_text;//文字
    public RawImage news_content;//图片
    public void sort()
    {
       
    }
    public void set(string name="AI")
    {
        if (isPlayer)
        {
            news_name.text= name;
            news_image.sprite = Center_Source.instance.sprite_player;
            news_text.text = Center_Put.instance.output;
        }
        else if (!isPlayer)
        {
            news_name.text = name;
            news_image.sprite = Center_Source.instance.sprite_ai;
            if (Center_Put.instance.AI_Input_Type == 1)
            {
                news_content.gameObject.SetActive(true);
                news_content.texture = Center_Put.instance.AI_Input_Image;
            }else if(Center_Put.instance.AI_Input_Type == 0){
                news_text.gameObject.SetActive(true);
                news_text.text = Center_Put.instance.AI_Input_String;
            }
        }
        //设置内容
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
