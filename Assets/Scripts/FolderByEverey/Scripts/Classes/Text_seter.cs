using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_seter : MonoBehaviour,text_seter
{
    public  TextMeshProUGUI text;
    Value_giver vg;
    public bool useValue;
    public void setText()
    {
       if(vg.giver_strs.Length>0) text.text = useValue ? ""+vg.getValue() : vg.getStr();
    }

    // Start is called before the first frame update
    void Start()
    {
        vg = GetComponent<Value_giver>();
        if (vg == null)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        setText();
    }
}
