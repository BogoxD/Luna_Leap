using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IItem
{
    public static event Action<int> OncarrotCollect;
    public int worth = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void CollectCarrot()
    {
        OncarrotCollect.Invoke(worth);
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectCarrot();
        if (other.CompareTag("Pickup"))
            other.gameObject.SetActive(false);
    }
}
