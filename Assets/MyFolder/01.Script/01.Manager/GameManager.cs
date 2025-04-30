using UnityEngine;

public class GameManager : SingleTone<GameManager>
{
    public bool IsGameRunning { get; private set; } = true;

    public void StartGame() => IsGameRunning = true;

    public void StopGame() => IsGameRunning = false;
}