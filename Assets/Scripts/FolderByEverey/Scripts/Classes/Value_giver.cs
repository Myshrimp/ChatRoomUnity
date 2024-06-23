using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//专门给滚动条用
public class Value_giver : MonoBehaviour,value_giver
{
    private Scrollbar sc;

    public int giver_pointer;
    public float[] giver_values;
    public string[] giver_strs;

    public string str_front, str_back;
    public bool useValue;

    public bool isUsing=true;
    public string getStr()
    {
        if (isUsing) return giver_strs[giver_pointer];
        return "被禁用";
    }
    public string getStr(bool must)
    {
        if (must) return giver_strs[giver_pointer];
        return getStr();
    }
    public float getValue()
    {
        if (isUsing) return giver_values[giver_pointer];
        return 6;
    }
    public float getValue(bool must)
    {
        if (must) return giver_values[giver_pointer];
        return getValue();
    }
    public string toString()
    {
        if (isUsing) return str_front +( useValue ? getValue() : getStr())+str_back;
        return "";
    }

    public void uiToPointer(float value)
    {
            
            if (sc != null)
            {
                float one_piece = 1f / (2 + 2 * (sc.numberOfSteps - 2));
                
                int a = (int)(((value - one_piece) / (2 * one_piece)) + 1);
                
                if (a < 0) { a = 0; }
                if (a > sc.numberOfSteps - 1) { a = sc.numberOfSteps - 1; }
                giver_pointer = a;
            }
        }
        
    

    // Start is called before the first frame update
    void Start()
    {
       sc = GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
