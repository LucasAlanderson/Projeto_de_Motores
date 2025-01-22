using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private bool isJumping = false;

    [SerializeField]
    private float Speed = 4f;

    [SerializeField]
    private float Jump = 10f;
    [SerializeField]
    private bool canDash = false;
    [SerializeField]
    private float dashCooldown = 2f;
    [SerializeField]
    private float dashDuration = Time.deltaTime;

    private float originalGravity;

    private float originalMass;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        originalGravity = rb.gravityScale;
        originalMass = rb.mass;

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-Speed * 20f, transform.position.y), ForceMode2D.Force);
        }else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.B))
            {
                StartCoroutine(dash());
            }
            rb.AddForce(new Vector2(Speed * 20f, transform.position.y), ForceMode2D.Force);
        }else if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector2(transform.position.x, Jump * 130), ForceMode2D.Force);
            isJumping = true;
        }
        else if (Input.GetKey(KeyCode.Q) && !isJumping)
        {
            rb.AddForce(new Vector2(-Speed * 90, Jump * 130), ForceMode2D.Force);
            isJumping = true;
        }
        else if (Input.GetKey(KeyCode.E) && !isJumping)
        {
            rb.AddForce(new Vector2(Speed * 90, Jump * 130), ForceMode2D.Force);
            isJumping = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(Speed * 100, transform.position.y), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }

    IEnumerator dash()
    {
        
        yield return null;
    }
}
