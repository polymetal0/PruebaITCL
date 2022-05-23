using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    public Score score;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            score.AddScore();
            UI.SetActive(true);
            Destroy(gameObject);
        }
    }
}
