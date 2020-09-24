using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI scoreText;


    private void Start()
    {
        GameManager.onEndGame += ShowEndGameMenu;
    }

    private void OnDestroy()
    {
        GameManager.onEndGame -= ShowEndGameMenu;
    }

    public void ShowEndGameMenu()
    {
        animator.SetTrigger("Open");
        scoreText.text = "Final Score: " + GameManager.Instance.player.playerScore;
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
