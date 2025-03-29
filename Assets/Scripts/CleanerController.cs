using UnityEngine;
using UnityEngine.InputSystem;

public class CleanerController : MonoBehaviour
{
    public float radius;
    public float speed;
    public float climbingSpeed;
    public float minRadius = 1f;
    public float maxRadius = 10f;

    public Transform craneL;
    public Transform craneR;
    public LineRenderer lineRenderer;

    private bool[] playerInput = new bool[2]; // 0 == up, 1 == down
    private bool usingCraneL = false;
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
        Transform crane = usingCraneL ? craneL : craneR;
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
        if (context.performed && !usingCraneL)
        {
            usingCraneL = true;
            SwitchCrane();
        }
    }

    public void Right(InputAction.CallbackContext context)
    {
        if (context.performed && usingCraneL)
        {
            usingCraneL = false;
            SwitchCrane();
        }
    }

    private void SwitchCrane()
    {
        Transform newCrane = usingCraneL ? craneL : craneR;
        Vector2 offset = transform.position - newCrane.position;
        radius = offset.magnitude;
        angle = Mathf.Atan2(-offset.y, offset.x);
        swingDirection = Mathf.Sign(Mathf.Sin(angle));
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
        lineRenderer.SetPosition(0, (usingCraneL ? craneL : craneR).position);
        lineRenderer.SetPosition(1, transform.position);
    }

    private void Climbing()
    {
        if (playerInput[0]) radius = Mathf.Max(minRadius, radius - climbingSpeed * Time.deltaTime);
        if (playerInput[1]) radius = Mathf.Min(maxRadius, radius + climbingSpeed * Time.deltaTime);
    }
}