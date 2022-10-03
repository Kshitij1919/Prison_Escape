using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Rigidbody2D myRigidBody;
    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        myRigidBody = GetComponent<Rigidbody2D>();
        bulletSpeed *= player.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(bulletSpeed, 0f);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
