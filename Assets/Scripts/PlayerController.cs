using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private TrailRenderer tr;
    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private float Speed = 2.18f;
    [SerializeField]
    private float Jump = 8f;
    [SerializeField]
    private bool canDash = true;
    private float dashCooldown = (float)2.0f;
    private float dashDuration = (float)0.1f;
    [SerializeField]
    private float dashForce = 20f;
    private float originalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        originalGravity = rb.gravityScale;
        tr.time = dashDuration + 0.4f;
        tr.emitting = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-Speed * 20f, 0), ForceMode2D.Force);

            if (Input.GetKeyDown(KeyCode.B) && canDash)
            {
                StartCoroutine(Dash(Vector2.left, rb));
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(Speed * 20f, 0), ForceMode2D.Force);

            if (Input.GetKeyDown(KeyCode.B) && canDash)
            {
                StartCoroutine(Dash(Vector2.right, rb));
            }
        }
        else if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector2(0, Jump * 130), ForceMode2D.Force);
            isJumping = true;
        }
        else if (Input.GetKey(KeyCode.Q) && !isJumping)
        {
            rb.AddForce(new Vector2(-Speed * 90, Jump * 130), ForceMode2D.Force);
            isJumping = true;

            if (Input.GetKeyDown(KeyCode.B) && canDash)
            {
                StartCoroutine(Dash(new Vector2(-1, 1), rb));
            }
        }
        else if (Input.GetKey(KeyCode.E) && !isJumping)
        {
            rb.AddForce(new Vector2(Speed * 90, Jump * 130), ForceMode2D.Force);
            isJumping = true;

            if (Input.GetKeyDown(KeyCode.B) && canDash)
            {
                StartCoroutine(Dash(new Vector2(1, 1), rb));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }

    private IEnumerator Dash(Vector2 direction, Rigidbody2D rb)
    {
        tr.emitting = true;
        canDash = false;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
