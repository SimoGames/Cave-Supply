using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public MovingBetweenRooms canMove;
    
    public bool hasDied = false;
    public GameObject killParticle;
    public SpriteRenderer rend;
    public BoxCollider2D col;
    public ParticleSystem respawnParticle;
    public Upgrading itemScript;

    public float conveyorSpeed;
    public bool isMovedByConveyor = false;

    public AudioSource jumpSFX;
    public AudioSource killSFX;
    public AudioSource respawnSFX;

    public Animation cameraShake;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Walking
        moveInput = Input.GetAxisRaw("Horizontal");
        if (canMove.isCamMoving == false && hasDied == false)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (isMovedByConveyor == true)
        {
            rb.AddForce(new Vector2(-conveyorSpeed, 0));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Update()
    {
        //Rotating player in walking direction
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }


        //Jumping
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            isJumping = true;
            jumpSFX.Play();
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
    public void Died()
    {
        cameraShake.Play("CameraShake");
        hasDied = true;
        Instantiate(killParticle, transform.position, transform.rotation);
        killSFX.Play();
        rend.enabled = false;
        col.enabled = false;
        itemScript.isHoldingConveyor = false;
        itemScript.isHoldingPress = false;
        if(canMove.IsInLeft == true)
        {
            canMove.camAnim.Play("PressToMachine");
        }
        if(canMove.IsInRight == true)
        {
            canMove.camAnim.Play("ConveyorToMachine");
        }
        StartCoroutine(killTimer());
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Lava")
        {
            Died();
        }
        if (collision.collider.tag == "Press")
        {
            Died();
        }

        if(collision.collider.tag == "MovingConveyor")
        {
            isMovedByConveyor = true;
        }
        else
        {
            isMovedByConveyor = false;
        }
    }
    IEnumerator killTimer()
    {
        yield return new WaitForSeconds(1.5f);
        cameraShake.Play("CameraShake");
        rend.enabled = true;
        col.enabled = true;
        respawnSFX.Play();
        hasDied = false;
        respawnParticle.Play();
        transform.position = new Vector3(-4.5f, 3, 0);
    }

}