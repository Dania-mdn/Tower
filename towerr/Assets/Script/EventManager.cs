using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action Colission;
    public static event Action EndGame;
    public static event Action MuteAudio;
    public static event Action PlayAudio;
    public static void DoColission()
    {
        Colission?.Invoke();
    }
    public static void DoEndGame()
    {
        EndGame?.Invoke();
    }
    public static void DoMuteAudio()
    {
        MuteAudio?.Invoke();
    }
    public static void DoPlayAudio()
    {
        PlayAudio?.Invoke();
    }
}
