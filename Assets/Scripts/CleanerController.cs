using UnityEngine;
using UnityEngine.InputSystem;

public class CleanerController : MonoBehaviour
{
    [Header("Per scene")]
    public Transform[] cranes;
    public LineRenderer lineRenderer;

    [Header("Global")]
    public float radius;
    public float speed;
    public float climbingSpeed;
    public float minRadius;
    public float maxRadius;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private bool[] playerInput = new bool[2]; // 0 == up, 1 == down
    private int usingCrane = 1;
    private Vector2 gravity = new Vector2(0, -9.81f);
    private Vector2 velocity;
    private float slowdownConstant = 0.999f;
    private float lineLength = 1;

    // x is right, -x is left, y is up, -y is down

    void Update()
    {
        Climbing();
        Swinging();
        DrawSpiderWeb();
        DrawCleaner();
    }

    private void Swinging()
    {
        Transform crane = cranes[usingCrane];
        Vector2 offset = (Vector2)transform.position - (Vector2)crane.position;
        Vector2 direction = offset.normalized;

        Vector2 tangentialVelocity = new Vector2(-direction.y, direction.x) * velocity.magnitude;
        Vector2 acceleration = gravity - (Vector2.Dot(gravity, direction) * direction);

        velocity += acceleration * Time.deltaTime;
        Vector2 newPosition = (Vector2)crane.position + direction * radius + velocity * Time.deltaTime;

        offset = newPosition - (Vector2)crane.position;
        transform.position = (Vector2)crane.position + offset.normalized * radius;

        velocity = Vector2.Dot(velocity, new Vector2(-direction.y, direction.x)) * new Vector2(-direction.y, direction.x);
        velocity *= slowdownConstant;
    }

    public void Left(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            usingCrane = Mathf.Max(usingCrane - 1, 0);
            SwitchCrane();
        }
    }

    public void Right(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            usingCrane = Mathf.Min(usingCrane + 1, cranes.Length - 1);
            SwitchCrane();
        }
    }

    public void GotHit()
    {
        lineLength = -.2f;
        velocity = Vector2.zero;
    }

    public void StartSpiderSense()
    {
        animator.SetTrigger("spiderSenseStart");
    }

    private void SwitchCrane()
    {
        Transform crane = cranes[usingCrane];
        Vector2 offset = transform.position - crane.position;
        radius = offset.magnitude;
        lineLength = 0;
    }

    public void Up(InputAction.CallbackContext context)
    {
        if (context.started) playerInput[0] = true;
        if (context.canceled) playerInput[0] = false;
    }

    public void Down(InputAction.CallbackContext context)
    {
        if (context.started) playerInput[1] = true;
        if (context.canceled) playerInput[1] = false;
    }

    private void DrawSpiderWeb()
    {
        Transform crane = cranes[usingCrane];
        if (lineLength < 1) lineLength = Mathf.Min(1, lineLength + Time.deltaTime / Vector3.Distance(transform.position, crane.position) * 50f);
        lineRenderer.SetPosition(0, Vector3.Lerp(transform.position, crane.position, lineLength));
        lineRenderer.SetPosition(1, transform.position);
    }

    private void Climbing()
    {
        if (playerInput[0] || radius > maxRadius)
            radius = Mathf.Max(minRadius, radius - climbingSpeed * Time.deltaTime);
        if (playerInput[1])
            radius = Mathf.Min(maxRadius, radius + climbingSpeed * Time.deltaTime);
    }

    private void DrawCleaner()
    {
        Vector2 offset = transform.position - cranes[usingCrane].position;
        float angle = Mathf.Atan2(-offset.y, offset.x);
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Rad2Deg * (angle - 90));

        spriteRenderer.flipX = velocity.x < 0;
    }
}