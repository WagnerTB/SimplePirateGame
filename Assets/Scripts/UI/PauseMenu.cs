using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Animator animator;
    public bool isOpen = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;
            SetOpenMenu(isOpen);
        }
    }

    public void SetOpenMenu(bool isOpen)
    {
        if(isOpen)
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
    }


}
