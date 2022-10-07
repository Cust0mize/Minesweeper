using UnityEngine;

public class UserInterface : MonoBehaviour
{
    private void Start()
    {
        EventManager.Win += OnWin;
        EventManager.Lose += OnLose;
    }

    private void OnWin()
    {
        print("YouWin");
    }

    private void OnLose()
    {
        print("YouLose");
    }
}
