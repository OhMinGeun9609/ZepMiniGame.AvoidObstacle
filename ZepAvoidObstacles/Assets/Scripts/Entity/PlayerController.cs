using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera;
    private NpcController npc;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }

        if(npc != null && npc.canTalk)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                npc.isTalk = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Angela"))
        {
            npc = collision.GetComponent<NpcController>();
            Debug.Log($"{npc.name}");
            if (npc != null)
                npc.canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Angela"))
        {
            npc = collision.GetComponent<NpcController>();
            Debug.Log($"{npc.name}");
            // 미니게임 설명 UI
            if (npc != null)
                npc.canTalk = false;
        }
    }
}
