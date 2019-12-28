using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        pDamage = 12;
        pSpeed = 5;
    }

    // Update is called once per frame
    void Update() {
        transform.position += -transform.right * Time.deltaTime * pSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerHealth hp = FindObjectOfType<PlayerHealth>();
            hp.TakeDamage(pDamage);
        }
        if (collision.tag != "projectile" && collision.tag != "Boundary")
        {
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, this);
        }
    }
}
