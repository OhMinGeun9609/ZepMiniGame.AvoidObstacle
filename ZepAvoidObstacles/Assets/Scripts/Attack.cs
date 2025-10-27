using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Camera cam;
    Vector3 camPos;
    Rigidbody2D rigidbody;

    private float hangTime = 0;
    private float speed = - 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();

        camPos = cam.transform.position;

        float x = camPos.x + 8.5f;
        float y = Random.Range(-2.0f, 2.0f);
        transform.position = new Vector3(x, y, 0);

        Vector3 velocity = rigidbody.velocity;
        velocity.x = speed;

        rigidbody.velocity = velocity;
    }

    private void Update()
    {
        hangTime += Time.deltaTime;

        if(hangTime > 3f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Destroy(this.gameObject);
        }
    }
}
