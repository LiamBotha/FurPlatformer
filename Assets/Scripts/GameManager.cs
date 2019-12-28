using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,IListener {

    private static GameManager instance = null;
    public Text text;
    public PlayerController player;
    bool timerEnabled = true;
    float timer = 0.0f;
    public float playerHP = 0;
    public string playerName = "NoName";

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public string NiceTime { get; set; }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        text = GameObject.FindObjectOfType<Text>();
    }

    // Use this for initialization
    void Start () {
        EventManager.Instance.AddListener(Events.DEADENEMY, this);
        EventManager.Instance.AddListener(Events.DEADPLAYER, this);
        EventManager.Instance.AddListener(Events.DESTROYOBJECT, this);
        EventManager.Instance.AddListener(Events.DEADBOSS, this);
}
	
	// Update is called once per frame
	void Update () {
        Timer();
        if (player == null)
        {
            if (Input.GetKey("tab"))
            {
                SceneManager.LoadScene(0);
            }
        }
	}

    void Timer()
    {
        if(timerEnabled)
        {
            timer += Time.deltaTime;
        }
    }

    public void OnEvent(Events Event, Component Sender, Object Param = null)
    {
        switch(Event)
        {
            case Events.DEADENEMY:
                {
                    Debug.Log("An enemy has Died");
                    Destroy(Sender.gameObject);
                }
                break;
            case Events.DEADPLAYER:
                {
                    Debug.Log("The Hero has Died");
                    Destroy(Sender.gameObject);
                    SceneManager.LoadScene("Death");
                }
                break;
            case Events.DESTROYOBJECT:
                {
                    Destroy(Sender.gameObject);
                }
                break;
            case Events.DEADBOSS:
                {
                    Debug.Log("Boss has Died");
                    Destroy(Sender.gameObject);
                    SceneManager.LoadScene("Endscreen");
                }
                break;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        text = GameObject.FindObjectOfType<Text>();
        if(scene.name == "Endscreen")
        {
            timerEnabled = false;
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            NiceTime = string.Format("{0:00}:{1:00:00}", minutes, seconds);

            Text score = GameObject.Find("Score Text").GetComponent<Text>();
            score.text = (NiceTime+ "        "+ playerHP);           
        }
    }
}
