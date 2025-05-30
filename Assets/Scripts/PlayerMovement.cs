using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float upperBound = 4f;
    public float lowerBound = -4f;

    float verticalMovement;

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + Vector2.up * verticalMovement * moveSpeed * Time.fixedDeltaTime;

        // Clamp the Y position to stay within bounds
        newPosition.y = Mathf.Clamp(newPosition.y, lowerBound, upperBound);

        rb.MovePosition(newPosition);
    }

    public void move(InputAction.CallbackContext context)
    {
        verticalMovement = context.ReadValue<Vector2>().y;
    }
}
