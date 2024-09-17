using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectorManager : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;
    public bool Portal_sprite;
    

    public delegate void CollectAction(int amount);
    public static CollectAction OncarrotCollect;

    private void Start()
    {
       progressAmount = 0;
       progressSlider.value = 0;

        OncarrotCollect += IncreaseProgressAmount;

        Portal_sprite = false;
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;
        if (progressAmount >= 100)
        {
           Portal_sprite = true;
            Debug.Log("Portal Open");
        }
    }
    
}
