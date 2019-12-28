using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public AudioClip dmgSound;

    public Slider healthBar;
    public float maxHealth = 100;

    private float invinciblity = 0.38f;
    private float invincTimer = 0;

    private float currentHealth;
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
            if (CurrentHealth <= 0)
            {
                EventManager.Instance.PostNotification(Events.DEADPLAYER, GameObject.Find("Player").GetComponent<PlayerController>());
            }
            if (CurrentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    private static PlayerHealth instance = null;
    public static PlayerHealth Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    //public void TakeDamage(float Damage)
    //{
    //    CurrentHealth -= Damage;
    //    if (CurrentHealth <= 0)
    //    {
    //        EventManager.Instance.PostNotification(Events.DEADPLAYER, this);
    //    }
    //    healthBar.value = CurrentHealth;
    //}

    public void TakeDamage(float Damage)
    {
        if (Time.time >= invincTimer)
        {
            AudioSource.PlayClipAtPoint(dmgSound, transform.position,0.5f);
            CurrentHealth -= Damage;
            healthBar.value = CurrentHealth;
            invincTimer = Time.time + invinciblity;
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
        if(scene.name == "Level")
        {
            healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
            CurrentHealth = maxHealth;
            TakeDamage(0);
        }
        if(scene.name == "Boss Fight")
        {
            healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
            TakeDamage(0);
        }
    }
}
