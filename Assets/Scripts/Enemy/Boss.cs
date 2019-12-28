using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    #region variables
    private float enemyHp = 100;

    public GameObject eProjectile;
    public Transform[] positions;
    public GameObject[] waypoints;
    public GameObject[] spawnpoints;
    public GameObject laserFab;
    public Slider healthbar;
    GameObject laser;
    public Animator[] weaponAnim;
    public Animator bossAnim;

    private float cooldown = 1.8f;
    private float timer;
    bool up = true;
    int attacksDone = 0;

    public float EnemyHp
    {
        get
        {
            return enemyHp;
        }

        set
        {
            enemyHp = value;
            healthbar.value = enemyHp;
         
            if(enemyHp <= 0)
            {
                EventManager.Instance.PostNotification(Events.DEADBOSS, this);
            }
        }
    }
    #endregion varaibles

    private enum Phase
    {
        START,MIDDLE,END
    };
    private enum Mode
    {
        LAZER,WAVE
    }

    private Phase currentPhase = Phase.START;
    private Mode attackMode = Mode.LAZER;

    // Use this for initialization
    void Start () {
        timer = Time.time + cooldown;
        positions = GetComponentsInChildren<Transform>();
        bossAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        foreach(Animator anim in weaponAnim)
        {
            anim.SetBool("Attacked", false);
        }
        EnemyAttack();
        EnemyMove();
        ChangePhase();
    }

    void EnemyAttack()
    {
        if (currentPhase == Phase.START)
        {
            if (timer <= Time.time)
            {
                WaveAttack();
                timer = Time.time + cooldown;
            }
        }
        if (currentPhase == Phase.MIDDLE)
        {
            if (timer <= Time.time)
            {
                WaveAttack();
                timer = Time.time + 1.3f;
                Invoke("WaveAttack", 1);
            }
        }
        if (currentPhase == Phase.END && transform.position == waypoints[0].transform.position)
        {
            if (Time.time >= timer && attackMode == Mode.LAZER)
            {
                LaserAttack();
                timer = Time.time + 5;
                attackMode = Mode.WAVE;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else if (currentPhase == Phase.END && attackMode == Mode.WAVE)
        {
            if (Time.time >= timer)
            {
                WaveAttack();
                Invoke("WaveAttack", 1);
                Invoke("WaveAttack", 1.5f);
                timer = Time.time + 1.3f;
                ++attacksDone;
                if(attacksDone == 10)
                {
                    attackMode = Mode.LAZER;
                    attacksDone = 0;
                }
            }
        }
    }

    void EnemyMove()
    {
        if (currentPhase == Phase.MIDDLE)
        {
            if (up == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[1].transform.position, 0.05f);
            }
            if (transform.position == waypoints[1].transform.position)
            {
                bossAnim.SetBool("Rising", false);
                up = false;
            }
            else if (transform.position == waypoints[2].transform.position)
            {
                bossAnim.SetBool("Rising", true);
                up = true;
            }
            if (up == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[2].transform.position, 0.05f);
            }
        }
        else if (currentPhase == Phase.END && attackMode == Mode.LAZER)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[0].transform.position, 0.1f);
            up = true;
        }
        else if (currentPhase == Phase.END && attackMode == Mode.WAVE)
        {
            if (up == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[1].transform.position, 0.05f);
            }
            if(transform.position == waypoints[1].transform.position)
            {
                up = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if(transform.position == waypoints[2].transform.position)
            {
                up = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (up == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[2].transform.position, 0.05f);
            }
        }
    }

    void ChangePhase()
    {
        if (enemyHp <= 75)
        {
            currentPhase = Phase.MIDDLE;
            bossAnim.SetInteger("Phase", 1);
        }
        if (enemyHp <= 45)
        {
            currentPhase = Phase.END;
        }
    }

    void LaserAttack()
    {
        GameObject[] lazers = new GameObject[5];
        lazers[0] = Instantiate(laserFab, new Vector3(spawnpoints[0].transform.position.x, spawnpoints[0].transform.position.y, 1), Quaternion.Euler(0, 0, 0));
        lazers[1] = Instantiate(laserFab, new Vector3(spawnpoints[1].transform.position.x, spawnpoints[1].transform.position.y, 1), Quaternion.Euler(0, 0, 0));
        lazers[2] = Instantiate(laserFab, new Vector3(spawnpoints[2].transform.position.x, spawnpoints[2].transform.position.y, 1), Quaternion.Euler(0, 0, 0));
        lazers[3] = Instantiate(laserFab, new Vector3(spawnpoints[3].transform.position.x, spawnpoints[3].transform.position.y, 1), Quaternion.Euler(0, 0, 0));
        lazers[4] = Instantiate(laserFab, new Vector3(spawnpoints[4].transform.position.x, spawnpoints[4].transform.position.y, 1), Quaternion.Euler(0, 0, 0));

        Destroy(lazers[Random.Range(0, 5)]);
    }

    void WaveAttack()
    {
        int rndm = Random.Range(0, 5);

        foreach (Animator a in weaponAnim)
        {
            if(a != weaponAnim[rndm])
            {
                a.SetBool("Attacked", true);
            }
        }

        GameObject[] proj = new GameObject[5];
        proj[0] = Instantiate(eProjectile, new Vector3(positions[1].position.x, positions[1].position.y), Quaternion.Euler(10, 0, 10));
        proj[1] = Instantiate(eProjectile, new Vector3(positions[2].position.x, positions[2].position.y), Quaternion.Euler(0, 0, 0));
        proj[2] = Instantiate(eProjectile, new Vector3(positions[3].position.x, positions[3].position.y), Quaternion.Euler(0, 0, -5));
        proj[3] = Instantiate(eProjectile, new Vector3(positions[4].position.x, positions[4].position.y), Quaternion.Euler(0, 0, -10));
        proj[4] = Instantiate(eProjectile, new Vector3(positions[5].position.x, positions[5].position.y), Quaternion.Euler(0, 0, -20));

        Destroy(proj[rndm]);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ola");
            PlayerHealth hp = FindObjectOfType<PlayerHealth>();
            hp.TakeDamage(10);
        }
    }
}
