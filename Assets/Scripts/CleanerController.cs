using UnityEngine;
using UnityEngine.InputSystem;

public class CleanerController : MonoBehaviour
{
    public float radius;
    public float speed;
    public float climbingSpeed;

    public Transform craneL;
    public Transform craneR;
    public LineRenderer lineRenderer;

    private bool[] playerInput = new bool[2]; //0==up, 1==down
    private bool usingCraneL = false;

    void Start()
    {

    }

    void Update()
    {
        DrawSpiderWeb();
        Climbing();

        Transform crane = usingCraneL ? craneL : craneR;
        // Swinging cleaner under the crane



    }

    public void Left(InputAction.CallbackContext context)
    {
        if (context.performed)
            usingCraneL = true;
    }

    public void Right(InputAction.CallbackContext context)
    {
        if (context.performed)
            usingCraneL = false;
    }

    public void Up(InputAction.CallbackContext context)
    {
        if (context.started)
            playerInput[0] = true;
        if (context.canceled)
            playerInput[0] = false;
    }

    public void Down(InputAction.CallbackContext context)
    {
        if (context.started)
            playerInput[1] = true;
        if (context.canceled)
            playerInput[1] = false;
    }

    private void DrawSpiderWeb()
    {
        lineRenderer.SetPosition(0, (usingCraneL ? craneL : craneR).position);
        lineRenderer.SetPosition(1, transform.position);
    }

    private void Climbing()
    {
        if (playerInput[0]) // up
        {
            radius -= climbingSpeed * Time.deltaTime;
        }

        if (playerInput[1]) // down
        {
            radius += climbingSpeed * Time.deltaTime;
        }
    }
}
