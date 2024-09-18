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

    private void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;

        Pickup.OnCarrotCollect += IncreaseProgressAmount;

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
