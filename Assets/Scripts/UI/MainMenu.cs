using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public Animator animator;

    [Header("Options")]
    public TMP_InputField gameSessionInputField;
    public TMP_InputField enemySpawnTimeInputField;

    private void Start()
    {
        if(GameManager.gameSessionTime <=0)
            GameManager.SetGameSession(60);

        if(GameManager.enemySpawnTime <= 0)
            GameManager.SetEnemySpawnTime(1);

        UpdateInputField();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenOptions()
    {
        animator.SetTrigger("OpenOptions");
    }

    public void OpenMainMenu()
    {
        animator.SetTrigger("OpenMainMenu");
    }

    public void SaveOptions()
    {
        var gameSessionTxt = gameSessionInputField.text;

        if (IsNumber(gameSessionTxt, out float gameSessionNumber))
            GameManager.SetGameSession(gameSessionNumber);


        var enemySpawnTimeTxt = enemySpawnTimeInputField.text;

        if (IsNumber(enemySpawnTimeTxt, out float enemySpawnTimeNumber))
            GameManager.SetEnemySpawnTime(enemySpawnTimeNumber);

        UpdateInputField();
    }

    private void UpdateInputField()
    {
        gameSessionInputField.text = GameManager.gameSessionTime.ToString();
        enemySpawnTimeInputField.text = GameManager.enemySpawnTime.ToString();
    }

    public bool IsNumber(string text, out float number)
    {
        int intNumber;
        float floatNumber;

        number = -1f;

        bool isNumber = false;

        if (int.TryParse(text, out intNumber))
        {
            number = (float)intNumber;
            isNumber = true;
        }
        else if (float.TryParse(text, out floatNumber))
        {
            number = floatNumber;
            isNumber = true;
        }

        return isNumber;
    }
}
