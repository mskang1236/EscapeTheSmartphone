using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float jumpForce;
    public bool isJumping = true;
    public float moveSpeed;
    public static bool hitOnApp = false;
    public static bool escapePhone = false;

    public AudioSource hitGround;
    public AudioSource jumpSound;

    private new Rigidbody2D rigidbody;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frames
    void Update()
    {
        // Move with right and left keys
        if (Input.GetKey(KeyCode.LeftArrow))
        { rigidbody.AddForce(Vector3.left * moveSpeed * Time.deltaTime, ForceMode2D.Impulse); }
        if (Input.GetKey(KeyCode.RightArrow))
        { rigidbody.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse); }

        // Jump with space bar key
        if (Input.GetKey(KeyCode.Space) && !isJumping) {
            jumpSound.Play();
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true; }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Some things that are used repeatedly
        GameObject cObject = collision.gameObject;
        bool cTagApp = cObject.CompareTag("App");
        bool turnedOff;

        // When running into falling app block, game is over
        if (cTagApp)
        {
            turnedOff = cObject.GetComponent<AppScript>().turnedOff;

            if (!turnedOff)
            {
                hitOnApp = true;
                gameManager.GetComponent<GameManager>().GameOver();
            }
        }

        // Player is available to jump on ground or an off app block
        if (cObject.CompareTag("Ground") || (cTagApp && cObject.transform.position.y < gameObject.transform.position.y))
        {
            //hitGround.Play();x
            isJumping = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            escapePhone = true;
            gameManager.GetComponent<GameManager>().GameClear();
        }
    }
}
