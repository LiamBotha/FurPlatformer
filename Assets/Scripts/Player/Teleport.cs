using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Rigidbody2D rb;
    private int pSpeed = 20;
    private bool pForward = true;
    public GameObject throwPoint;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        throwPoint = GameObject.Find("Throw Point");

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
        if(rb != null)
        {
            if (pForward == true)
            {
                //rb.velocity = Vector2.right * 10;
                transform.position += transform.right * Time.deltaTime * pSpeed;
            }
            else
            {
                //rb.velocity = Vector2.left * 10;
                transform.position += transform.right * Time.deltaTime * pSpeed;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Boundary" && collision.tag != "Player" && collision.tag != "OneWay")
        {
            var empty = new GameObject();
            empty.transform.parent = collision.transform;
            gameObject.transform.parent = empty.transform;
            //gameObject.transform.parent = collision.transform;
            Destroy(rb);
        }
        if(collision.tag == "projectile" || collision.name.Contains("Enemy"))
        {
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, this);
        }
    }
}
