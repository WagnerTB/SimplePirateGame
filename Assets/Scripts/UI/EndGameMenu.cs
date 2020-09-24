using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        GameManager.onEnd += ShowEndGameMenu;
    }

    public void ShowEndGameMenu()
    {
        animator.SetTrigger("Open");
        GameManager.Instance.currentState = GameManager.GameState.End;
        Time.timeScale = 1;
    }

    public void Retry()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
