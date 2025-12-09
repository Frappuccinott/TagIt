using UnityEngine;

public class Cacmove : MonoBehaviour, IFruitBoostable
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Control Keys")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode rotateLeftKey = KeyCode.V;
    public KeyCode rotateRightKey = KeyCode.B;

    private Vector3 moveDirection;
    private Animator animator;

    private bool speedBoostActive = false;
    private float originalSpeed;
    private float boostEndTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        originalSpeed = moveSpeed;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        UpdateSpeedBoost();
    }
    void HandleMovement()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(upKey)) vertical = 1f;
        if (Input.GetKey(downKey)) vertical = -1f;
        if (Input.GetKey(leftKey)) horizontal = -1f;
        if (Input.GetKey(rightKey)) horizontal = 1f;

        moveDirection = (forward * vertical + right * horizontal).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        animator.SetFloat("Speed", moveDirection.magnitude);
    }

    void HandleRotation()
    {
        float rotationSpeed = 90f * Time.deltaTime;

        if (Input.GetKey(rotateLeftKey)) transform.Rotate(Vector3.up, -rotationSpeed);
        if (Input.GetKey(rotateRightKey)) transform.Rotate(Vector3.up, rotationSpeed);
    }

    public void ApplySpeedBoost(float duration)
    {
        moveSpeed = originalSpeed * 2f;
        boostEndTime = Time.time + duration;
        speedBoostActive = true;
    }

    private void UpdateSpeedBoost()
    {
        if (speedBoostActive && Time.time >= boostEndTime)
        {
            moveSpeed = originalSpeed;
            speedBoostActive = false;
        }
    }
}
