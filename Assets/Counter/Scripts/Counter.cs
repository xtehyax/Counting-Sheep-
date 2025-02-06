using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;

    public int sheepHerded;

    private void Start()
    {
        sheepHerded = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        sheepHerded = +1;
        Debug.Log("Sheep in pen: " + sheepHerded);
    }
}
