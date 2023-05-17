using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class ScoreText : MonoBehaviour
{
    private TMP_Text text;
    private int totalScore = 0;

    [Inject]
    private void Construct()
    {
        text = GetComponent<TMP_Text>();
        text.text = totalScore.ToString();
    }

    public void AddScore(int score)
    {
        this.totalScore += score;
        text.text = totalScore.ToString();
    }
}
