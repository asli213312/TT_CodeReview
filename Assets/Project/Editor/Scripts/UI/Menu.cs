using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject winPanel;
    
    private const float PAUSE_TIME = 0f;
    private const float NORMAL_TIME = 1f;
    private void Start()
    {
        SetTimeScale(PAUSE_TIME);
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        SetTimeScale(NORMAL_TIME);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = NORMAL_TIME;
    }
    
    private void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public GameObject FailPanel => failPanel;
    public GameObject WinPanel => winPanel;
}
