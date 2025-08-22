using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TankMovement : MonoBehaviour
{
    public InputActionAsset action;

    private InputAction m_moveAction;

    private Vector2 m_moveTank;

    private Rigidbody2D rb;

    public float speed;
    public float speedRotate;

    private void OnEnable()
    {
        action.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        action.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>(); 
    }
    private void Update()
    {
        m_moveTank = m_moveAction.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Walking();
        Rotate();
    }

    private void Walking()
    {
        Vector2 dir = transform.up * m_moveTank.y * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + dir);
    }
    private void Rotate()
    {
        if (m_moveTank.y != 0)
        {
            float rotationAmount = m_moveTank.x * speedRotate * Time.deltaTime;
            rb.MoveRotation(rb.rotation + rotationAmount);
        }
    }
}
