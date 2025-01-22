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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A)){
            rb.AddForce(new Vector2(-Speed * 15, transform.position.y));
        }else if(Input.GetKey(KeyCode.D)){
            rb.AddForce(new Vector2(Speed * 15, transform.position.y));
        }else if(Input.GetKey(KeyCode.W) && !isJumping){
            rb.AddForce(new Vector2(transform.position.x, Jump * 130), ForceMode2D.Force);
            isJumping = true;
        }else if(Input.GetKey(KeyCode.Q) && !isJumping){
            rb.AddForce(new Vector2(-Speed * 90, Jump * 130), ForceMode2D.Force);
            isJumping = true;
        }else if(Input.GetKey(KeyCode.E) && !isJumping){
            rb.AddForce(new Vector2(Speed * 90, Jump * 130), ForceMode2D.Force);
            isJumping = true;
        }else if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.X)){
            rb.AddForce(new Vector2(Speed * 100, transform.position.y), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }
}
