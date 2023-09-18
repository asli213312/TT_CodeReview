using System;
using UnityEngine;
using UnityEngine.UI;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameModeUI gameModeUI;
    [SerializeField] private Menu menu;
    [SerializeField] private TimerMode timerMode;
    [SerializeField] private HealthMode healthMode;
    [SerializeField] private PointCounter pointCounter;
    [Header("Options")] 
    [SerializeField] private int requiredPoints;
    
    public int RequiredPoints => requiredPoints;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGameMode(IGameMode gameMode)
    {
        gameMode.StartGame();
        gameModeUI.SetVisible(false);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public HealthMode GetHealthMode() => healthMode;
    public TimerMode GetTimerMode() => timerMode;
    public PointCounter GetPointCounter() => pointCounter;
    public Menu GetMenu() => menu;
}
