using GameKit.Dependencies.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Be_control : MonoBehaviour, be_control
{
    Value_giver vg;
    public Value_giver controller;
    public GameObject unless;
    public int goal_pointer = -1;
    public float goal_value = -1;
    public string goal_str = "6002259";
    public bool isFit()
    {
        if (goal_pointer == controller.giver_pointer) return true;
        if (goal_value == controller.getValue(true)) return true;
        if (goal_str == controller.getStr(true)) return true;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        vg = GetComponent<Value_giver>();
        if (vg == null)
        {
            Destroy(this);
        }else
        {
            unless = Instantiate(Center_Source.instance.unless,transform );
            RectTransform rt = unless.GetComponent<RectTransform>();
            RectTransform me = GetComponent<RectTransform>();
            rt.localPosition = new Vector3();
            rt.sizeDelta = me.sizeDelta;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller != null)
            vg.isUsing = isFit();
        if (unless != null)
        {
            unless.SetActive(!vg.isUsing);
           
        }
    }
}
