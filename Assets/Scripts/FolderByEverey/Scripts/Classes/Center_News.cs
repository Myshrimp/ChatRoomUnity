using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Center_News : MonoBehaviour,center_
{
    public static Center_News instance { get; private set; }
    public List<News> news;
    public VerticalLayoutGroup content;
    public GameObject news_model;//模型信息 自带news
    public GameObject news_me;
    
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        news = new List<News>();
    }
    
        
    

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public News addNews(bool isPlayer)
    {
        //   if (isPlayer) content.childAlignment =TextAnchor.UpperRight;
        //  else  content.childAlignment =TextAnchor.UpperLeft;
        GameObject g;
       if (isPlayer) g= Instantiate(news_me,content.transform);
       else  g = Instantiate(news_model, content.transform);
        News n = g.GetComponent<News>();
        if (n != null)
        {
            news.Add(n);
            content.GetComponent<RectTransform>().position += new Vector3(0,300);
            n.isPlayer = isPlayer;
            n.sort();
        }
        return n;
    }
    public void clear()
    {
        GameObject[] gs=  content.GetComponentsInChildren<GameObject>();
        foreach(var i in gs)
        {
            Destroy(i);
        }
        news.Clear();
    }
}
