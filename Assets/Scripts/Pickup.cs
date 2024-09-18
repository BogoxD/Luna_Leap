using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IItem
{
    public static event Action<int> OnCarrotCollect;
    public int worth = 5;
    public void CollectCarrot()
    {
        OnCarrotCollect.Invoke(worth);
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectCarrot();
        if (other.CompareTag("Pickup"))
            other.gameObject.SetActive(false);
    }
}
