using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerMode : MonoBehaviour, IGameMode
{
    [SerializeField] private Text timerText;
    [SerializeField] private float timeLimit = 30f;

    private float _timer;
    private bool _timerIsRunning;

    private Menu _menu;
    private PointCounter _pointCounter;

    [Inject] private GameStarter _gameStarter;

    private void Awake()
    {
        gameObject.SetActive(false);
        
        _menu = _gameStarter.GetMenu();
        _pointCounter = _gameStarter.GetPointCounter();
    }
    
    public void StartGame()
    {
        _timer = timeLimit;
        _timerIsRunning = true;
        
        gameObject.SetActive(true);
    }
    
    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (_timerIsRunning)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0 || CheckWinCondition())
            {
                _timerIsRunning = false;
                TimeIsOff();
            }

            UpdateTimerDisplay();    
        }
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = FormatTimer(_timer);
    }

    private void TimeIsOff()
    {
        GameObject panelToShow = CheckWinCondition() ? _menu.WinPanel : _menu.FailPanel;

        _menu.OpenPanel(panelToShow);
        Time.timeScale = 0;
    }

    private bool CheckWinCondition()
    {
        int points = _pointCounter.Points;
        int requiredPoints = _gameStarter.RequiredPoints;
        
        return points >= requiredPoints;
    }

    private string FormatTimer(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        int secondsInt = Mathf.FloorToInt(seconds % 60f);
        
        minutes = Mathf.Max(minutes, 0);
        secondsInt = Mathf.Max(secondsInt, 0);
        
        return $"{minutes:00}:{secondsInt:00}";
    }
}
