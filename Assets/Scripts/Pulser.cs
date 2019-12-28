using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulser : MonoBehaviour {

    public float pulseStart = 1;
    public int pulseDamage = 10;
    bool active = false;
    Animator anim;
    PlayerController player;
    Transform room;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        room = transform.parent.parent.Find("Room Camera");
    }

    private void FixedUpdate()
    {
        if(player != null && active == false && Vector2.Distance(player.transform.position, room.position) < 9)
        {
            active = true;
            StartCoroutine(Activate());
        }
        if(player != null && active == true && Vector2.Distance(player.transform.position, room.position) > 15)
        {
            anim.SetBool("Active", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(pulseDamage);
        }
    }

    IEnumerator Activate()
    {
        yield return new WaitForSecondsRealtime(pulseStart);
        anim.SetBool("Active", true);
    }
}
