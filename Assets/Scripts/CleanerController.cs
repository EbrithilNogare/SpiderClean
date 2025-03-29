using UnityEngine;
using UnityEngine.InputSystem;

public class CleanerController : MonoBehaviour
{
    public float radius;
    public float speed;
    public float climbingSpeed;
    public float minRadius;
    public float maxRadius;

    public Transform[] cranes;
    public LineRenderer lineRenderer;

    private bool[] playerInput = new bool[2]; // 0 == up, 1 == down
    private int usingCrane = 1;
    private Vector2 gravity = new Vector2(0, -9.81f);
    private Vector2 velocity;

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

        // Compute acceleration from gravity
        Vector2 tangentialVelocity = new Vector2(-direction.y, direction.x) * velocity.magnitude;
        Vector2 acceleration = gravity - (Vector2.Dot(gravity, direction) * direction);

        // Integrate velocity and apply constraints
        velocity += acceleration * Time.deltaTime;
        Vector2 newPosition = (Vector2)crane.position + direction * radius + velocity * Time.deltaTime;

        // Maintain rope constraint
        offset = newPosition - (Vector2)crane.position;
        transform.position = (Vector2)crane.position + offset.normalized * radius;

        // Adjust velocity to be tangent to the swing
        velocity = Vector2.Dot(velocity, new Vector2(-direction.y, direction.x)) * new Vector2(-direction.y, direction.x);
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

    private void SwitchCrane()
    {
        Transform crane = cranes[usingCrane];
        Vector2 offset = transform.position - crane.position;
        radius = offset.magnitude;
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
        lineRenderer.SetPosition(0, crane.position);
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
    }
}