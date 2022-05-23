using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timeText;

    private float t = 0f;

    void Update()
    {
        DisplayTime();
    }

    private void DisplayTime()
    {
        //only increment time when in play mode
        if (PlayerController.state == PlayerController.PlayingState.Playing)
        {
            t += Time.deltaTime;
            float min = Mathf.FloorToInt(t / 60);
            float s = Mathf.FloorToInt(t % 60);
            timeText.text = string.Format("{0:00}:{1:00}", min, s);
        }
    }
}
