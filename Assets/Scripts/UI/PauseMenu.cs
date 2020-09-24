using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator animator;
    public bool isOpen = false;

    private void Start()
    {
        Time.timeScale = 1;
        GameManager.onEndGame += EndGame;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
        GameManager.onEndGame -= EndGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.End && Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;
            SetOpenMenu(isOpen);
        }
    }

    private void EndGame()
    {
        if(this != null && isOpen)
            SetOpenMenu(false);
    }

    public void SetOpenMenu(bool isOpen)
    {
        if (isOpen)
        {
            animator.SetTrigger("Open");
            GameManager.Instance.currentState = GameManager.GameState.Pause;
            Time.timeScale = 0;
        }
        else
        {
            animator.SetTrigger("Close");
            GameManager.Instance.currentState = GameManager.GameState.Playing;
            Time.timeScale = 1;
        }

        this.isOpen = isOpen;
    }


    public void ContinueGame()
    {
        SetOpenMenu(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
