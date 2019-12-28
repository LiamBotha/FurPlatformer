using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecureConnection : MonoBehaviour {

    public string playerName;

    private string secretKey = "POE";
    public string writeScoreURL = "http://localhost/FurDatabase/insertScores.php?";

    public string highscoreURL = "http://localhost/FurDatabase/displayScores.php";

    public Text statusText;

    // Use this for initialization
    private void Awake()
    {
        string uName = GameManager.Instance.playerName;
        string time = GameManager.Instance.NiceTime;
        int health = System.Convert.ToInt32(GameManager.Instance.playerHP);

        StartCoroutine(WriteScores(uName, time, health));
    }
    void Start () {
        StartCoroutine(GetScores());
	}
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator GetScores()
    {
        yield return new WaitForSeconds(0.5f);

        statusText.text = "Loading Scores";
        WWW getRequest = new WWW(highscoreURL);
        yield return getRequest;

        if(getRequest.error != null)
        {
            print("There was an error getting the high score: " + getRequest.error);
        }
        else
        {
            statusText.text = getRequest.text;
        }
    }

    public string MD5Encrypt(string text)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(text);

        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        string hashString = "";

        for(int i = 0;i< hashBytes.Length;++i)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    IEnumerator WriteScores(string name, string timer, int health)
    {
        string hash = MD5Encrypt(name + timer + health + secretKey);

        string fullSendURL = writeScoreURL + "username=" + WWW.EscapeURL(name) + "&time=" + timer + "&health=" + health + "&hash=" + hash;
        Debug.Log(fullSendURL);

        WWW sendRequest = new WWW(fullSendURL);
        yield return sendRequest;

        if(sendRequest.error != null)
        {
            Debug.Log("There was an error posting the high score: " + sendRequest.error);
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
        if (scene.name == "Highscores")
        {
            StartCoroutine(GetScores());
        }
    }
}
