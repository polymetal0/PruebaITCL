using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject lvlList;
    [SerializeField] private GameObject lvlListElem;
    void Start()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            //scenes.Add(sceneName);
            Debug.Log("Scene: " + sceneName);
            if (sceneName != SceneManager.GetActiveScene().name)
            {
                Instantiate(lvlListElem, lvlList.transform).GetComponentInChildren<Text>().text = sceneName;
            }
        }
    }

    public void LoadLevel(Text lvl)
    {
        SceneManager.LoadScene(lvl.text);
        
    }
}
