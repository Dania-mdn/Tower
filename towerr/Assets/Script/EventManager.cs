using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action Colission;
    public static void DoColission()
    {
        Colission?.Invoke();
    }
}
