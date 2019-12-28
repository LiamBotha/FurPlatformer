using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour {

    #region variables
    private Rigidbody2D player;
    [SerializeField]private GameObject teleporter;
    private GameObject tele;
    private Animator anim;
    public LayerMask groundLayer;

    //int momentum = 0;
    float speed = 200;
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
    #endregion valiables

    #region methods
    void CheckAbilities()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Destroy(tele);
            tele = GameObject.Instantiate(teleporter);
            tele.name = "Teleporter";
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)&& tele != null)
        {
            gameObject.transform.position = tele.transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Destroy(tele);
        }
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.72f;

        Debug.DrawRay(position, direction,Color.red);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        else return false;
    }
    #endregion methods

    // Use this for initialization
    void Start () {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Atk = false;
        float xMovement = Input.GetAxis("Horizontal");
        //float yMovement = Input.GetAxis("Vertical");

        player.velocity = new Vector2(xMovement * speed * Time.deltaTime, player.velocity.y);

        if(/*player.velocity.y == 0 && */IsGrounded())
        {
            isJumping = false;
            grounded = true;
            anim.SetBool("jump", false);
        }
        else
        {
            grounded = false;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isJumping == false && grounded ==true)
        {
            anim.SetBool("jump", true);
            player.AddForce(new Vector2(0, jump));
            isJumping = true;
            grounded = false;
        }
        CheckAbilities();
	}


}
