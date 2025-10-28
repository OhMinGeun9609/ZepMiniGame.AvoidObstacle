using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAttack : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sr;
    private Animator anim;

    float duration = 0.417f;

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        rigidbody = GetComponentInChildren<Rigidbody2D>();

        position = cam.transform.position;

        float xRangePlus = position.x + 3.1f;
        float xRangeMinus = position.x - 3f;
        float yRangePlus = position.y + 1.5f;
        float yRangeMinus = position.y - 1.5f;

        float x = Random.Range(xRangeMinus, xRangePlus);
        float y = Random.Range(yRangeMinus, yRangePlus);

        transform.position = new Vector2(x, y);

        DelayBomb(duration);
    }


    private IEnumerator DelayBomb(float delay)
    {
        anim.SetBool("Boom", true);
        yield return new WaitForSeconds(delay);
        Destroy(anim);
        Destroy(this.gameObject);
    }
}
