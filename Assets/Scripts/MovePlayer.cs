using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public BoxCollider2D bc2d;

    public bool TESST = false;

    public bool isCollide;
    public Transform colliderCheckUpLeft;
    public Transform colliderCheckDownRight;




    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public bool isJumping = false;

    private void FixedUpdate()
    {
        float horizMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        TESST = bc2d.isTrigger;
        isCollide = Physics2D.OverlapArea(colliderCheckUpLeft.position, colliderCheckDownRight.position);

        ExecuteMovePlayer(horizMove);
    }

    void ExecuteMovePlayer(float _horizMove)
    {
        Vector3 targetVelocity = new Vector2(_horizMove, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
}
