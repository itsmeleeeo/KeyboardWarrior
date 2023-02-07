using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    public Animator animator;

    Rigidbody2D rb;
    BoxCollider2D cb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        cb = GetComponent<BoxCollider2D>();
    }

    void Update() {
        if(isFacingDown()) {
            rb.velocity = new Vector2(0f, -moveSpeed);
        } else {
            rb.velocity = new Vector2(0f, moveSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.name == "PatrollingUp") {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), transform.localScale.y);
            animator.SetBool("moveUp",true);
        } else {
            transform.localScale = new Vector2((Mathf.Sign(rb.velocity.x)), transform.localScale.y);
            animator.SetBool("moveUp",false);
        }
    }
    private bool isFacingDown() {
        return transform.localScale.x > Mathf.Epsilon;
    }

}
