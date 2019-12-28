using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    Scene currentScene;

    [SerializeField] private GameObject bindingPanel;
	
	// Update is called once per frame
	void Update () {
        currentScene = SceneManager.GetActiveScene();

		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Bye");
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(bindingPanel != null)
            {
                bindingPanel.SetActive(!bindingPanel.activeSelf);
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && currentScene.name == "Death")
        {
            SceneManager.LoadScene("Level");
        }
        if (Input.GetKeyDown(KeyCode.Return) && currentScene.name == "Endscreen")
        {
            //SceneManager.LoadScene("Input");
        }
        if (Input.GetKeyDown(KeyCode.Return) && currentScene.name == "StartMenu")
        {
            SceneManager.LoadScene("Level");
        }
        if (Input.GetKeyDown(KeyCode.Return) && currentScene.name == "Highscores")
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void GetName(string name)
    {
        GameManager.Instance.playerName = name;
        SceneManager.LoadScene("Highscores");
    }
}
