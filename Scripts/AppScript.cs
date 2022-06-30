using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppScript : MonoBehaviour
{

    public bool turnedOff = false;
    public bool gameOver = false;

    private float xLeftEnd = -2.1f;
    private float xRightEnd = 1.9f;
    private float ySpawnPos = 6f;

    private SpriteRenderer color;
    private GameObject ceiling;

    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>();
        ceiling = GameObject.Find("Ceiling");

        if (SceneManager.GetActiveScene().name == "GamePlay") { transform.position = SpawnPosition(); }

        if (!turnedOff)
        { Physics2D.IgnoreCollision(ceiling.GetComponent<Collider2D>(), GetComponent<Collider2D>()); }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 SpawnPosition()
    {
        return new Vector3(Random.Range(xLeftEnd, xRightEnd), ySpawnPos, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Those that are repeated a lot
        GameObject cObject = collision.gameObject;
        string cName = cObject.name;

        // If an app hits the ground, turn them off
        if (cObject.CompareTag("Ground"))
        {
            turnAppsOff();
        }

        // If an app hits an another app
        if (cObject.CompareTag("App"))
        {
            // Netflix
            if (gameObject.name == "Netflix(Clone)")
            {
                turnAppsOff();
            }
            // Unity
            else if (gameObject.name == "Unity(Clone)")
            {
                if (cName == "Discord(Clone)") { }
                else { turnAppsOff(); }
            }
            // Discord
            else if (gameObject.name == "Discord(Clone)")
            {
                if (cName == "Unity(Clone)" || cName == "YoutubePremium(Clone)") { }
                else { turnAppsOff();  }
            }
            // Youtube Premium
            else if (gameObject.name == "YoutubePremium(Clone)")
            {
                if (cName == "Unity(Clone)" || cName == "Netflix(Clone)") { turnAppsOff(); }
            }
            // Others
            else
            {
                if (!(cName == "YoutubePremium(Clone)")) { turnAppsOff(); }
            }
        }
    }



    private void turnAppsOff()
    {
        color.material.color = new Color(0.5f, 0.5f, 0.5f);
        turnedOff = true;
    }
}
