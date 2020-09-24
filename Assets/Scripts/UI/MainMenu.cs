using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public Animator animator;

    [Header("Options")]
    public TMPro.TMP_InputField gameSessionInputField;
    public TMPro.TMP_InputField enemySpawnTimeInputField;


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
