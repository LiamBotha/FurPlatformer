using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {

    #region variables
    private int damage;
    private int j = 1;
    public float timer = 0;
    public float cooldown = 1.5f;
    bool top = true;
    public GameObject projectile;
    private GameObject player;
    public Transform[] turrets;

    //Enums
    public enum Directions
    {
        UP,DOWN,LEFT,RIGHT
    };
    public enum Mode
    {
        NORMAL,ALTERNATE,INCREMENT,FULL
    };

    public Directions direction;
    public Mode mode;
    Transform room;

    #endregion variables
    #region methods
    void Attack()
    {
        int miss = Random.Range(2, turrets.Length -1);
        for(int i = 1; i < turrets.Length;++i)
        {
            if(i != miss && i != miss + 1 && i != miss -1)
            {
                GameObject fire;
                switch (direction)
                {
                    case Directions.UP:
                        {
                            fire = Instantiate(projectile, new Vector2(turrets[i].position.x, turrets[i].position.y + 0.6f), new Quaternion(0,0,0,0));
                            fire.transform.eulerAngles = new Vector3(0, 0, 270);
                        }
                        break;
                    case Directions.DOWN:
                        {
                            fire = Instantiate(projectile, new Vector2(turrets[i].position.x, turrets[i].position.y - 0.6f), new Quaternion(0, 0, 0, 0));
                            fire.transform.eulerAngles = new Vector3(0, 0, 90);
                        }
                        break;
                    case Directions.LEFT:
                        {
                            fire = Instantiate(projectile, new Vector2(turrets[i].position.x - 0.6f, turrets[i].position.y), new Quaternion(0, 0, 0, 0));
                            fire.transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                        break;
                    case Directions.RIGHT:
                        {
                            fire = Instantiate(projectile, new Vector2(turrets[i].position.x + 0.6f, turrets[i].position.y), new Quaternion(0, 0, 0, 0));
                            fire.transform.eulerAngles = new Vector3(0, 0, 180);
                        }
                        break;
                    default:
                        {
                            fire = Instantiate(projectile, new Vector2(turrets[i].position.x - 0.6f, turrets[i].position.y), new Quaternion(0, 0, 0, 0));
                        }
                        break;
                }
                fire.transform.localScale = new Vector2(0.6f, 0.6f);
                fire.GetComponent<TrailRenderer>().widthCurve = new AnimationCurve(new Keyframe(0.1f, 0.5f), new Keyframe(0.3f, 0.2f), new Keyframe(0.6f, 0.0f));
            }
        }
    }
    void AltAttack()
    {
        if (top == true)
        {
            for (int i = 4; i < turrets.Length; ++i)
            {
                GameObject fire = Instantiate(projectile, new Vector2(turrets[i].position.x - 0.6f, turrets[i].position.y), transform.rotation);
                fire.transform.localScale = new Vector2(0.6f, 0.6f);
                fire.GetComponent<TrailRenderer>().widthCurve = new AnimationCurve(new Keyframe(0.1f, 0.5f), new Keyframe(0.3f, 0.2f), new Keyframe(0.6f, 0.0f));
            }
            top = false;
        }
        else
        {
            for (int i = 1; i < turrets.Length - 3; ++i)
            {
                GameObject fire = Instantiate(projectile, new Vector2(turrets[i].position.x - 0.6f, turrets[i].position.y), transform.rotation);
                fire.transform.localScale = new Vector2(0.6f, 0.6f);
                fire.GetComponent<TrailRenderer>().widthCurve = new AnimationCurve(new Keyframe(0.1f, 0.5f), new Keyframe(0.3f, 0.2f), new Keyframe(0.6f, 0.0f));
            }
            top = true;
        }
    }
    void AltAttack2()
    {
        for (int i = 1; i < turrets.Length; ++i)
        {
            if (i != j && i != j + 1 && i != j + 2)
            {
                GameObject fire = Instantiate(projectile, new Vector2(turrets[i].position.x - 0.6f, turrets[i].position.y), transform.rotation);
                fire.transform.localScale = new Vector2(0.6f, 0.6f);
                fire.GetComponent<TrailRenderer>().widthCurve = new AnimationCurve(new Keyframe(0.1f, 0.5f), new Keyframe(0.3f, 0.2f), new Keyframe(0.6f, 0.0f));
            }
        }
        if (j > turrets.Length - 4)
        {
            j = 1;
        }
        else j++;
    }
    void FullAttack()
    {
        for (int i = 1; i < turrets.Length; ++i)
        {
            GameObject fire;
            switch (direction)
            {
                case Directions.UP:
                    {
                        fire = Instantiate(projectile, new Vector2(turrets[i].position.x, turrets[i].position.y + 0.6f), new Quaternion(0, 0, 0, 0));
                        fire.transform.eulerAngles = new Vector3(0, 0, 270);
                    }
                    break;
                case Directions.DOWN:
                    {
                        fire = Instantiate(projectile, new Vector2(turrets[i].position.x, turrets[i].position.y - 0.6f), new Quaternion(0, 0, 0, 0));
                        fire.transform.eulerAngles = new Vector3(0, 0, 90);
                    }
                    break;
                case Directions.LEFT:
                    {
                        fire = Instantiate(projectile, new Vector2(turrets[i].position.x - 0.6f, turrets[i].position.y), new Quaternion(0, 0, 0, 0));
                        fire.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    break;
                case Directions.RIGHT:
                    {
                        fire = Instantiate(projectile, new Vector2(turrets[i].position.x + 0.6f, turrets[i].position.y), new Quaternion(0, 0, 0, 0));
                        fire.transform.eulerAngles = new Vector3(0, 0, 180);
                    }
                    break;
                default:
                    {
                        fire = Instantiate(projectile, new Vector2(turrets[i].position.x - 0.6f, turrets[i].position.y), new Quaternion(0, 0, 0, 0));
                    }
                    break;
            }
            fire.transform.localScale = new Vector2(0.6f, 0.6f);
            fire.GetComponent<TrailRenderer>().widthCurve = new AnimationCurve(new Keyframe(0.1f, 0.5f), new Keyframe(0.3f, 0.2f), new Keyframe(0.6f, 0.0f));
        }
    }

    void AttackManage()
    {
        if (Time.time >= timer)
        {
            switch (mode)
            {
                case Mode.NORMAL:
                    {
                        Attack();
                    }
                    break;
                case Mode.ALTERNATE:
                    {
                        AltAttack();
                    }
                    break;
                case Mode.INCREMENT:
                    {
                        AltAttack2();
                    }
                    break;
                case Mode.FULL:
                    {
                        FullAttack();
                    }
                    break;
            }
            timer = Time.time + cooldown;
        }
    }
    #endregion methods

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        turrets = gameObject.GetComponentsInChildren<Transform>();
        room = transform.parent.Find("Room Camera");
    }
    private void Update()
    {
        if(player != null && Vector2.Distance(room.position, player.transform.position) < 10)
        {
                AttackManage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            PlayerHealth hp = collision.GetComponent<PlayerHealth>();
            hp.TakeDamage(2);
            Debug.Log("blep");
        }
    }
}
