using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coob : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.DoColission();
    }
}
