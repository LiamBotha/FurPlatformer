using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    #region variables
    [SerializeField]
    private float moveSpeed = 2;
    private bool isMoving = false;
    private bool forward = true;
    //Cooldown periods
    private float atkCooldown = 0.5f;
    private float teleCooldown = 0.6f;

    //Timers
    private float atkTimer = 0;
    private float teleTimer = 0;

    //Prefabs
    [SerializeField] private GameObject attacker;
    [SerializeField] private GameObject teleporter;
    //Components
    private Animator anim;
    private Rigidbody2D player;
    private GameObject tele;
    public LayerMask groundLayer;
    AudioSource bgm;
    public AudioClip warpSound;

    float jump = 400;
    bool atk = false;
    bool isJumping = false;
    bool grounded = true;

    public bool Atk
    {
        get
        {
            return atk;
        }

        set
        {
            atk = value;
        }
    }

    #endregion variables

    #region methods
    void HandleInput()
    {
        FlipPlayer();
        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0,0);
            anim.SetBool("move", true);
            isMoving = true;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0,0);
            anim.SetBool("move", true);
            isMoving = true;
        }
        if (Input.GetKeyDown("m"))
        {
            bgm.mute = !bgm.mute;
        }
    }

    void HandleAttacks()
    {
        if (Input.GetKey(KeyBinding.Keys["btnAttack"]))
        {         
            if(!isMoving)
            {
                if(atkTimer <= Time.time)
                {
                    anim.SetInteger("attack", 1);
                    Instantiate(attacker);
                    atkTimer = Time.time + atkCooldown;
                }
            }
            else
            {
                if (atkTimer <= Time.time)
                {
                    anim.SetInteger("attack", 2);
                    Instantiate(attacker);
                    atkTimer = Time.time + atkCooldown;
                }
            }
        }
    }

    void ResetValues()
    {
        isMoving = false;
        anim.SetBool("move", false);
        anim.SetInteger("attack", 0);
    }

    void FlipPlayer()
    {
        float movement = Input.GetAxis("Horizontal");
        if((movement > 0 && !forward) || (movement < 0 && forward))
        {
            Vector3 playerScale = transform.localScale;
            playerScale.x = -playerScale.x;
            transform.localScale = playerScale;
            forward = !forward;
        }
    }

    void CheckAbilities()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && tele == null)
        {
            if (teleTimer <= Time.time && !isMoving)
            {
                //Destroy(tele);
                anim.SetInteger("attack", 1);
                tele = GameObject.Instantiate(teleporter);
                tele.name = "Teleporter";
                teleTimer = Time.time + teleCooldown;
            }
            else if (teleTimer <= Time.time && isMoving)
            {
                //Destroy(tele);
                anim.SetInteger("attack", 2);
                tele = GameObject.Instantiate(teleporter);
                tele.name = "Teleporter";
                teleTimer = Time.time + teleCooldown;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && tele != null)
        {
            AudioSource.PlayClipAtPoint(warpSound, transform.position);
            gameObject.transform.position = tele.transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            Destroy(tele);
            if(tele.transform.parent != null)
            {
                Destroy(tele.transform.parent.gameObject);
            }
        }
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.65f;

        Debug.DrawRay(position, direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        else return false;
    }

    #endregion methods

    private void Start()
    {      
        anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        bgm = GetComponent<AudioSource>();

        GameObject spawn = GameObject.Find("Spawn Point");
        transform.position = spawn.transform.position;
    }
    // Update is called once per frame
    void Update () {

        ResetValues();
        HandleInput();
        HandleAttacks();

        Atk = false;
        float xMovement = Input.GetAxis("Horizontal");
        //float yMovement = Input.GetAxis("Vertical");

        player.velocity = new Vector2(xMovement * moveSpeed * Time.deltaTime, player.velocity.y);

        if (/*player.velocity.y == 0 && */IsGrounded())
        {
            isJumping = false;
            grounded = true;
            anim.SetBool("jump", false);
        }
        else
        {
            grounded = false;
        }
        if (Input.GetKeyDown(KeyBinding.Keys["btnJump"]) && isJumping == false && grounded == true)
        {
            anim.SetBool("jump", true);

            player.AddForce(new Vector2(0,jump));

            isJumping = true;
            grounded = false;
        }
        CheckAbilities();
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
        if(scene.name == "StartMenu")
        {
            Destroy(gameObject);
        }
        else if(scene.name != "Level" && scene.name != "Boss Fight")
        {
            GameManager.Instance.playerHP = PlayerHealth.Instance.CurrentHealth;
            gameObject.SetActive(false);
        }
        else
        {
            GameObject spawn = GameObject.Find("Spawn Point");
            transform.position = spawn.transform.position;
        }
    }
}
