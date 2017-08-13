using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float _Speed = 5;
    public float _MaxSpeed = 10;
    public AudioClip _PaddleSound;
    public AudioClip _WallSound;
    public AudioSource _AudioSource;
    public Collider2D _Collider;
    public Rigidbody2D _Body;
    public GameObject _Particles;

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
        Debug.Log("Ball : " + direction);
        Vector2 newDirection = _Body.velocity;
        newDirection.x = Mathf.Abs(newDirection.x) * direction.x;
        newDirection.y = Mathf.Abs(newDirection.y) * direction.y;
        Debug.Log("Ball : " + newDirection);
        _Body.velocity = newDirection;
        Debug.Log("Ball : " + _Body.velocity);
    }

    public Vector2 GetVelocity()
    {
        return _Body.velocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Paddle") {
            _AudioSource.PlayOneShot(_PaddleSound);
            // Particle Effect
            Instantiate(_Particles, transform.position, Quaternion.identity);
        } else if (other.gameObject.tag == "Wall") {
            _AudioSource.PlayOneShot(_WallSound);
            // Particle Effect
            Instantiate(_Particles, transform.position, Quaternion.identity);
        }
    }
}
