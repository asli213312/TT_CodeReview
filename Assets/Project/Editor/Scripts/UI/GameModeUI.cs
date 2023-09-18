using UnityEngine;
using UnityEngine.UI;


public class GameModeUI : MonoBehaviour
{
    [SerializeField] private Button timerModeButton;
    [SerializeField] private Button healthModeButton;
    
    private void Awake()
    {
        timerModeButton.onClick.AddListener(StartTimerGameMode);
        healthModeButton.onClick.AddListener(StartHealthGameMode);
    }

    private void OnDestroy()
    {
        timerModeButton.onClick.RemoveListener(StartTimerGameMode);
        healthModeButton.onClick.RemoveListener(StartHealthGameMode);
    }
    
    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
    
    private void StartTimerGameMode()
    {
        GameStarter gameStarter = GetComponentInParent<GameStarter>();
        gameStarter.StartGameMode(gameStarter.GetTimerMode());
    }
    
    private void StartHealthGameMode()
    {
        GameStarter gameStarter = GetComponentInParent<GameStarter>();
        gameStarter.StartGameMode(gameStarter.GetHealthMode());
    }
}
