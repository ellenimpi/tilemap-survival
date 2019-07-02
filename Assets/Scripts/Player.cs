using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float ladderSpeed = 5f;

    Rigidbody2D playerBody;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider;
    BoxCollider2D feetCollider;
    float startingGravity;

    bool isMoving = false;
    bool isAlive = true;
    bool isClimbing = false;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        startingGravity = playerBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false) { return; }

        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            StartCoroutine("Die");
        }
    }

    public void Run()
    {
            var control = Input.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(control * moveSpeed, playerBody.velocity.y);
            playerBody.velocity = playerVelocity;

            playerAnimator.SetBool("Running", isMoving);
        
    }

    public void Jump()
    {
            if (Input.GetButtonDown("Jump") && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
            Debug.Log("Perform Jump");
                Vector2 jumpAdd = new Vector2(0f, jumpSpeed);
                playerBody.velocity += jumpAdd;
            }
        
    }

    public void ClimbLadder()
    {
            if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                playerBody.gravityScale = 0f;

                //do climb
                var control = Input.GetAxis("Vertical");
                Vector2 climbSpeed = new Vector2(playerBody.velocity.x, control * ladderSpeed);
                playerBody.velocity = climbSpeed;

                isClimbing = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
                playerAnimator.SetBool("Climbing", isClimbing);
            }
            else
            {
                isClimbing = false;
                playerAnimator.SetBool("Climbing", false);
                playerBody.gravityScale = startingGravity;
            }
        
    }


    public void FlipSprite()
    {
        isMoving = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
         if (isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerBody.velocity.x), 1f);
        }
    }

    IEnumerator Die()
    {
            Debug.Log("dead!");
            yield return new WaitForSecondsRealtime(0.2f);
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            playerBody.velocity = new Vector2(0f, 100f);
    }

}
