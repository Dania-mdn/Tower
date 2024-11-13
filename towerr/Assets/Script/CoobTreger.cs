using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoobTreger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.DoColission();
    }
}
