using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingScript : MonoBehaviour
{

    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("App"))
        {
            if (other.gameObject.GetComponent<AppScript>().turnedOff)
            {
                gameManager.GetComponent<GameManager>().GameOver();
            }
        }
    }
}
