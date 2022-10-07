using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action Win;
    public static event Action Lose;

    public static void OnWin()
    {
        Win?.Invoke();
    }

    public static void OnLose()
    {
        Lose?.Invoke();
    }
}
