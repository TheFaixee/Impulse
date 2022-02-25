using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public float speed = 1.0f;
    private float yRange = 4.7f;
    private float xRange = 9.0f;
    public GameObject wave;
    public AudioSource playerAudio;
    public AudioClip fireSound;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float sideInput = Input.GetAxis("Horizontal");

        
        playerRB.AddForce((Vector2.up).normalized * speed * forwardInput);
        playerRB.AddForce((Vector2.right).normalized * speed * sideInput);

        if (transform.position.y > yRange)
        {
            transform.position = new Vector2(transform.position.x, yRange);
        }
        if(transform.position.y < -yRange)
        {
            transform.position = new Vector2(transform.position.x, -yRange);
        }
        if(transform.position.x > xRange)
        {
            transform.position = new Vector2(xRange, transform.position.y);
        }
        if(transform.position.x < -xRange)
        {
            transform.position = new Vector2(-xRange, transform.position.y);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(wave, transform.position, wave.transform.rotation);
            playerAudio.PlayOneShot(fireSound, 1.0f);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Time.timeScale = 0f;
            Debug.Log("Game Over!");
        }
    }
}
