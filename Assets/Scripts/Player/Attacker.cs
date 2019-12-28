using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Projectile{

    public GameObject throwPoint;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        throwPoint = GameObject.Find("Throw Point");
        pDamage = 6;
        pSpeed = 20;

        if (player.transform.localScale.x > 0)
        {
            transform.position = new Vector3(throwPoint.transform.position.x, throwPoint.transform.position.y,1);
            pForward = true;
        }
        else
        {
            transform.position = new Vector3(throwPoint.transform.position.x, throwPoint.transform.position.y,1);
            pForward = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (rb != null)
        {
            if (pForward == true)
            {
                transform.position += transform.right * Time.deltaTime * pSpeed;
            }
            else
            {
                transform.position += transform.right * Time.deltaTime * pSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Contains("Boss") && !collision.name.Contains("Projectile"))
        {
            Boss enemy = collision.GetComponent<Boss>();
            enemy.EnemyHp -= pDamage;
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, this);
        }
        if (collision.name.Contains("Enemy") && !collision.name.Contains("Projectile"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.EnemyHp -= pDamage;
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, this);
        }
        else if(collision.tag == "Breakable")
        {
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, collision);
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, this);
        }
        else if (collision.tag != "projectile" && collision.tag != "Player" && collision.tag != "Boundary")
        {
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, this);
        }

    }
}
