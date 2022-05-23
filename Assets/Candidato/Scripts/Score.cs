using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private float _score = 0f;

    public void AddScore()//Transform targetTx)
    {
        // The less time has passed, the more points
        _score += 1000 - Time.realtimeSinceStartup;
        scoreText.text = string.Format("{0:0000}", _score);
    }


}
