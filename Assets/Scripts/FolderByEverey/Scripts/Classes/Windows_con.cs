using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windows_con : MonoBehaviour,windows_controller
{
    public GameObject close, open;
    public bool isUsing;
    public void self_checking()
    {
        if (close != null)
        {
            if (!close.activeSelf)  isUsing = true;
            else  isUsing = false; 
        }
        if (open != null)
        {
            if (open.activeSelf)isUsing = true;
            else isUsing = false;
        }
    }

    public void use()
    {
        if (close != null)close.SetActive(isUsing);
        if (open != null) open.SetActive(!isUsing);
        isUsing = !isUsing;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        self_checking();
    }
}
