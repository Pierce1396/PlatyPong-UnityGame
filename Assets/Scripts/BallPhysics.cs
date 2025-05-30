using UnityEngine;
using System.Collections;

public class BallPhysics : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float angleDegrees;
    private float angleRadians;
    private float ballSpeed;

    private enum BallState { Idle, Launching, Active }
    private BallState ballState = BallState.Idle;

    [Header("Speed Settings")]
    public float baseSpeed = 13f;

    [Header("Reflection Settings")]
    public float maxBounceAngle = 40f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetBall();
        StartCoroutine(LaunchBallAfterDelay(1.5f));
    }

    void FixedUpdate()
    {
        if (ballState == BallState.Active)
        {
            direction = rb.linearVelocity.normalized;

            if (ballSpeed < baseSpeed)
            {
                StartCoroutine(LaunchBallAfterDelay(1.5f));
            }
        }
    }

    void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
        ballState = BallState.Launching;
    }

    public IEnumerator LaunchBallAfterDelay(float delay)
    {
        ResetBall();
        ballSpeed = baseSpeed;
        yield return new WaitForSeconds(delay);

        // Determine launch angle
        if (Random.value > 0.5f)
            angleDegrees = Random.Range(-20f, 20f);
        else
            angleDegrees = Random.Range(160f, 200f);

        angleRadians = angleDegrees * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
        rb.linearVelocity = direction.normalized * ballSpeed;

        ballState = BallState.Active;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ballState != BallState.Active)
            return;

        if (collision.gameObject.CompareTag("Paddle"))
        {
            HandlePaddleBounce(collision);
        }
        else
        {
            // Wall bounce - preserve current speed
            Vector2 normal = collision.GetContact(0).normal;
            direction = Vector2.Reflect(direction, normal);
            float currentSpeed = Mathf.Max(rb.linearVelocity.magnitude, ballSpeed);
            rb.linearVelocity = direction.normalized * currentSpeed;
        }
    }

    private void HandlePaddleBounce(Collision2D collision)
    {
        // Paddle bounds
        Transform paddle = collision.transform;
        float paddleY = paddle.position.y;
        float ballY = transform.position.y;
        float paddleHeight = paddle.GetComponent<Collider2D>().bounds.size.y;

        // Calculate the hit factor (-1 at bottom, 0 center, 1 at top)
        float yOffset = (ballY - paddleY) / (paddleHeight / 2);
        yOffset = Mathf.Clamp(yOffset, -1f, 1f);

        // Calculate bounce angle based on offset
        float bounceAngle = yOffset * maxBounceAngle;
        float bounceRad = bounceAngle * Mathf.Deg2Rad;

        // Determine direction based on which paddle
        float xDirection = (transform.position.x < 0) ? 1 : -1;
        direction = new Vector2(xDirection * Mathf.Cos(bounceRad), Mathf.Sin(bounceRad));

        // Speed increases the farther from center it hits
        float speedBoost = 1f + Mathf.Abs(yOffset);
        float currentSpeed = Mathf.Max(rb.linearVelocity.magnitude, baseSpeed);
        ballSpeed = currentSpeed * speedBoost;

        rb.linearVelocity = direction.normalized * ballSpeed;
    }

    public void OnScore()
    {
        if (ballState != BallState.Launching)
        {
            ballState = BallState.Idle;
            StartCoroutine(LaunchBallAfterDelay(1.5f));
        }
    }
}
