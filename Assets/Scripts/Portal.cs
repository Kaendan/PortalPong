using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Portal : When a ball enters its collider teleports the ball to its linked portal
public class Portal : MonoBehaviour
{
    // The portal to which the ball will be teleported
    public Portal _LinkedPortal;
    public Collider2D _Collider;
    public Vector2 _Direction;
    public GameObject _Particles;
    public AudioSource _AudioSource;

    // Size of the collider bounds
    private Vector2 _Size;

    void Start()
    {
        _Size = _Collider.bounds.size;
    }

    public Vector2 GetDirection()
    {
        return _Direction;
    }

    public void SetDirection(Vector2 direction)
    {
        _Direction = direction;
    }

    // Get the position to where the ball will go
    public Vector2 GetTeleportPosition(Ball ball)
    {
        Vector2 newPos = transform.position;
        // Moves the ball in front of the portal depending on the portal's direction
        newPos.x += _Direction.x * ((_Size.x + ball.GetSize().x) / 2 + 0.03f);
        // Moves the ball to the portal the portal's Y Coordinate relatively to where the ball hit the other portal
        newPos.y += ball.transform.position.y - _LinkedPortal.transform.position.y;
        return newPos;
    }

    // Inverses the portal direction : Used when it moves to the other wall
    public void ChangeDirection()
    {
        _Direction.x *= -1;
    }

    public void Teleport(Ball ball)
    {
        // Reset the ball's trail effect
        ball.ResetTrail();
        // Move the ball to its new position
        ball.transform.position = GetTeleportPosition(ball);

        // Compute the ball's new direction without altering its velocity
        Vector2 direction = Vector2.zero;
        if (ball.GetVelocity().x != 0) {
            // Changes velocity value to a -1 or 1 value
            direction.x = Mathf.Abs(ball.GetVelocity().x) / ball.GetVelocity().x;
        }

        if (ball.GetVelocity().y != 0) {
            // Changes velocity value to a -1 or 1 value
            direction.y = Mathf.Abs(ball.GetVelocity().y) / ball.GetVelocity().y;
        }

        // Inverse the X coordinate of the direction if the ball does not go in the portal's direction
        if (_Direction.x != 0 && Mathf.Sign(_Direction.x) != Mathf.Sign(direction.x)) {
            direction.x *= -1; 
        }

        // Change the ball's direction
        ball.SetDirection(direction);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // When a ball hits the portal
        if (other.gameObject.tag == "Ball") {
            Ball ball = other.gameObject.GetComponent<Ball>();
            // Teleports the ball to the other portal
            _LinkedPortal.Teleport(ball);

            _AudioSource.Play();

            // Particle Effect
            Vector2 newPos = transform.position;
            newPos.x += +_Direction.x * _Size.x / 2f; // spawns the particle on the good side
            Instantiate(_Particles, newPos, Quaternion.identity);
        }
    }
}
