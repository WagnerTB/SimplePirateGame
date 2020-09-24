using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
