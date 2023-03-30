using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float score;
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void AddScore(float points)
    {
        score += points;
    }

    private void Update()
    {
        //Add score per second. Check if necessary
        //score += Time.deltaTime;
        scoreText.text = "Score: " + score.ToString("0");
    }
}
