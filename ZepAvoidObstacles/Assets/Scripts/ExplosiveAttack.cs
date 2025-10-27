using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAttack : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sr;
    private Animator anim;

    float duration = 3f;

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
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

        Warning();

        anim.SetBool("isBoom", true);

        WaitAnime(duration);
    }

    private IEnumerator Warning()
    {
        float time = 0f;
        Color start = sr.color;

        while(time < duration)
        {
            time += Time.deltaTime;

            start.a = Mathf.Lerp(start.a, 1f, duration);
            sr.color = start;

            yield return null;
        }
    }

    private IEnumerable WaitAnime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
