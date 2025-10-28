using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera;
    private bool isNpcArea;
    

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // 가로방향 입력
        float vertical = Input.GetAxisRaw("Vertical"); // 세로방향 입력
        movementDirection = new Vector2(horizontal, vertical); // 가로입력과 세로입력을 사용해 vector2 생성
        if(movementDirection.magnitude != 0f) // horizontal, vertical이 0일때, NaN에러 방지를 위해
        {
            movementDirection = movementDirection.normalized; // 진행거리를 정규화하여 부모클래스 BaseController의 프로퍼티에 넣어준다.
        }
        // 대각선 이동시에 (1, 1)(가로, 세로)이/가 입력되면, 대각선 방향이 더 길기 때문에, 원래 속도보다 1.4배 빠르게이동하기에, 정규화로 방향만 남긴다.
        
        Vector2 mousePosition = Input.mousePosition; // 카메라 범위 안에서의 마우스 좌표정보를 받음
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition); // 마우스의 위치의 단위가 카메라 범위 내의 좌표를 월드좌표로 변환
        lookDirection = (worldPos - (Vector2)transform.position); // 플레이어의 위치가 시작점, 마우스의 월드좌표가 끝점이라 했을때, (끝점 - 시작점) 식으로 플레이어가 보는 방향의 좌표가 설정됨

        if (lookDirection.magnitude < .9f) // 보는 방향이 0에 가까우면, 정규화할때 0에 근접한수로 나누어지고, 이때, 정규화가 불안정해서 캐릭터 떨림 등의 현상이 발생하기 때문에, 0으로 설정해 계산을 멈춤
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized; // 보는방향값을 정규화
        }

        if(isNpcArea && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.TalkToNPC();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Npc"))
        {
            gameManager.CloseToNPC();
            isNpcArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Npc"))
        {
            if(gameManager != null)
            {
                gameManager.ExitToNPC();
            }
            
            isNpcArea = false;
        }
    }
}
