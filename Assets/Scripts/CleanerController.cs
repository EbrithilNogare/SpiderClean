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
    private float gravity = 9.81f;
    private float angle = Mathf.PI / 2f;
    private float swingDirection = 1f;

    void Update()
    {
        Climbing();
        Swinging();
        DrawSpiderWeb();
    }

    private void Swinging()
    {
        Transform crane = cranes[usingCrane];
        angle += speed * swingDirection * Time.deltaTime;
        angle = Mathf.Clamp(angle, 0, Mathf.PI);

        float x = crane.position.x + Mathf.Cos(angle) * radius;
        float y = crane.position.y - Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, transform.position.z);

        if (angle >= Mathf.PI || angle <= 0)
            swingDirection *= -1f;
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
        angle = Mathf.Atan2(-offset.y, offset.x);
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
        if (playerInput[0]) radius = Mathf.Max(minRadius, radius - climbingSpeed * Time.deltaTime);
        if (playerInput[1]) radius = Mathf.Min(maxRadius, radius + climbingSpeed * Time.deltaTime);
    }
}