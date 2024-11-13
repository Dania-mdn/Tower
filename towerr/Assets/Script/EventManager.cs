using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action Colission;
    public static event Action EndGame;
    public static void DoColission()
    {
        Colission?.Invoke();
    }
    public static void DoEndGame()
    {
        EndGame?.Invoke();
    }
}
