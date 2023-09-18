using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthMode : MonoBehaviour, IGameMode
{
    [SerializeField] private Text healthText;

    [Inject] private Player _player;
    [Inject] private GameStarter _gameStarter;

    private event Action HealthChanged;

    private int _health;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        HealthChanged += UpdateHealthText;
    }
    
    private void OnDisable()
    {
        HealthChanged -= UpdateHealthText;
    }

    public void StartGame()
    {
        _health = _player.Health;
        UpdateHealthText();
        gameObject.SetActive(true);
    }

    private void Update()
    {
        CheckGameOutcome();
    }
    
    private void CheckGameOutcome()
    {
        if (_gameStarter.GetPointCounter().Points >= _gameStarter.RequiredPoints)
        {
            EndGame(_gameStarter.GetMenu().WinPanel);
        }
        else if (_health <= 0)
        {
            EndGame(_gameStarter.GetMenu().FailPanel);
        }
    }
    
    private void EndGame(GameObject panelToShow)
    {
        _gameStarter.GetMenu().OpenPanel(panelToShow);
        Time.timeScale = 0f;
    }

    public void ReduceHealth(int damage)
    {
        _health = Mathf.Max(0, _health - damage);
        HealthChanged?.Invoke();
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + _health;
    }
}