using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            finishPanel.SetActive(true);
            PlayerController.state = PlayerController.PlayingState.Pause;
        }
    }

   
}
