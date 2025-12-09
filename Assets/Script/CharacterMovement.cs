using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    private Vector3 movement;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movement = new Vector3(moveX, 0, moveZ).normalized;

        bool isMoving = movement.magnitude > 0;
        animator.SetBool("isMoving", isMoving);
    }
}
