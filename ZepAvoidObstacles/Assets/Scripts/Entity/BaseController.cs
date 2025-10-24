using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected AnimationHandler animationHandler;
    public GameManager gameManager;


    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    // 물리연산은 FixedUpdate에서 진행
    protected virtual void FixedUpdate()
    {
        Movement(MovementDirection);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction)
    {
        direction = direction * 5; // 진행방향 좌표에 진행속도를 곱하여 이동속도를 계산
        if(knockbackDuration > 0.0f) // 넉백이 지속될 때, 이동속도에 0.2를 곱하고, 넉백수치를 더해준다. 
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction; // rigidbody의 가속도에 계산한 이동속도를 넣어준다.
        animationHandler.Move(direction); // 애니메이션 핸들러클래스에 이동속도를 넣어주면, 일정속도 이상일 때, 애니메이션을 전환
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 시선방향 좌표 (x, y)에서 x축을 기준으로 어느각도인지 라디안으로 계산후, 도(Degree) 단위로 변환한다.
        bool isLeft = Mathf.Abs(rotZ) > 90f; // 계산되어 변환된 각도의 절댓값이 90도를 넘으면, true, 아니면 false;

        characterRenderer.flipX = isLeft; // flipX는 bool값이고, 값이 true이면 x축(가로) 방향으로 뒤집고, false이면 뒤집지 않는다.
        // 유니티 상 캐릭터스프라이트는 오른쪽을 바라보고 있으므로 시선방향이 오른쪽이면 뒤집을 필요가 없고, 왼쪽이면 뒤집어야함

        if(weaponPivot != null) // 무기 피벗이 null이 아닐 때
        {
            // rotation : Quaternion(사원수)자료형, Euler : 사람이 이해하기 쉬운 각도
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
            // 무기 또한 캐릭터가 보는 방향으로 회전시켜야하므로 z축 각도를 설정한다.
        }
    }


    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration; // 넉백 지속시간
        knockback = -(other.position - transform.position).normalized * power;
    }
    
}
