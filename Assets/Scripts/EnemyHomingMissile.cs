using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingMissile : Projectile
{

    private float lifespan = 2.5f;
    private float lifespanTimer = 0;
    private float speed = 4.5f;

    void Start()
    {
        lifespanTimer = Time.time + lifespan;
        rb = GetComponent<Rigidbody2D>();
        pDamage = 15;
        pSpeed = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= lifespanTimer)
        {
            Destroy(this);
        }
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            PlayerHealth hp = FindObjectOfType<PlayerHealth>();
            hp.TakeDamage(pDamage);
        }
        else if (collision.tag != "Enemy" && collision.tag != "projectile" && collision.tag != "Boundary")
        {
            Destroy(gameObject);
            //EventManager.Instance.PostNotification(Events.DESTROYOBJECT, this);
        }

    }
}
