using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Ball's speed
    public float _Speed = 3f;
    // Maximum speed
    public float _MaxSpeed = 9f;
    // Sound played when a Paddle is hit
    public AudioClip _PaddleSound;
    // Sound played when a Wall is hit
    public AudioClip _WallSound;

    public AudioSource _AudioSource;
    public Collider2D _Collider;
    public Rigidbody2D _Body;
    public GameObject _Particles;
    public TrailRenderer _Trail;

    // Size of the collider bounds
    private Vector2 _Size;


    void Start()
    {
        _Size = _Collider.bounds.size;
    }

    public Vector2 GetSize()
    {
        return _Size;
    }

    public float GetSpeed()
    {
        return _Speed;
    }

    public void SetSpeed(float speed)
    {
        // Only set the speed if it's not greater than the maximum speed
        if (speed <= _MaxSpeed) {
            _Speed = speed;
        }
    }

    public void SetVelocity(Vector2 direction)
    {
        _Body.velocity = direction * _Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        // Change the velocity x and y signs depending on the given direction
        Vector2 newDirection = _Body.velocity;
        newDirection.x = Mathf.Abs(newDirection.x) * direction.x;
        newDirection.y = Mathf.Abs(newDirection.y) * direction.y;

        _Body.velocity = newDirection;
    }

    public Vector2 GetVelocity()
    {
        return _Body.velocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Paddle") { // if a paddle is hit
            // Play sound
            _AudioSource.PlayOneShot(_PaddleSound);

            // Particle effect
            Instantiate(_Particles, transform.position, Quaternion.identity);
        } else if (other.gameObject.tag == "Wall") { // if a wall is hit
            // Play sound
            _AudioSource.PlayOneShot(_WallSound);

            // Particle effect
            Instantiate(_Particles, transform.position, Quaternion.identity);
        }
    }

    // Reset the trail effect. Used when the ball pass through a portal
    public void ResetTrail()
    {
        _Trail.Clear();
    }
}
