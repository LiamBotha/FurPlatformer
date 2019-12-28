using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private int enemyHp = 20;

    public GameObject eProjectile;
    private float cooldown = 5f;
    private float timer;
    Transform room;
    public Transform aim;
    PlayerController player;

    public int EnemyHp
    {
        get
        {
            return enemyHp;
        }

        set
        {
            enemyHp = value;
            if(enemyHp <= 0)
            {
                EventManager.Instance.PostNotification(Events.DEADENEMY, this);
            }
        }
    }

    // Use this for initialization
    void Start () {
        timer = Time.time + cooldown;
        player = FindObjectOfType<PlayerController>();
        room = transform.parent.Find("Room Camera");
    }
	
	// Update is called once per frame
	void Update () {
        EnemyAttack();
    }

    void EnemyAttack()
    {
        if(player != null && timer <= Time.time && Vector2.Distance(room.position, player.transform.position) < 10)
        {
            Instantiate(eProjectile,new Vector3(aim.position.x,aim.position.y,transform.position.z),Quaternion.identity);
            timer = Time.time + cooldown;
        }
    }
}
