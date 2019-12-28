using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    int pDamage = 7;
    float cooldown = 0.2f;
    float invincibility = 0;
    float lifetime = 0;

    private void Start()
    {
        lifetime = Time.time + 4;
    }

    private void FixedUpdate()
    {
        if(Time.time >= lifetime)
        {
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, this);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time > invincibility)
        {
            if (collision.tag == "Player")
            {
                PlayerHealth hp = FindObjectOfType<PlayerHealth>();
                hp.TakeDamage(pDamage);
                invincibility = Time.time + cooldown;
            }
        }
    }
}
