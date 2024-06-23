using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animation_small : MonoBehaviour,animation_small
{
   private Image re;

   public  bool isUsing=false;
    public float once_time = 0.1f;
   public  int  times=1;  
   public  bool looping=true; 
   public  Sprite[] sprites; 
   public  bool will_destroy=false ;

    private int pointer;
    private int hasTimes;
    private float  hasTime;

    public void play()
    {
        isUsing = true;
        if (hasTimes == 0)
        {
            hasTimes = times;
        }
    }

    public void stop()
    {
        isUsing = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        re = GetComponent<Image>();
        hasTimes = times;
        hasTime = once_time;
        if (sprites == null)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsing && (looping || hasTimes > 0)){
            if (hasTime < 0)
            {
                re.sprite = sprites[pointer++];
                if (pointer >= sprites.Length)
                {
                    pointer = 0;
                    if (!looping)
                    {
                        hasTimes--;
                    }
                }
                hasTime = once_time;
              
            }else
            {
                hasTime -= Time.deltaTime;
            }

        }
        if (hasTimes== 0 && isUsing)
        {
            isUsing = false;
            if (will_destroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
