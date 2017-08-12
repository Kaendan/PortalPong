using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float _Speed = 5;
    public AudioClip _PaddleSound;
    public AudioClip _WallSound;
    public AudioSource _AudioSource;
    public Collider2D _Collider;
    public Rigidbody2D _Body;

    private Vector2 _Size;

    void Start()
    {
        _Size = _Collider.bounds.size;
    }

    public Vector2 GetSize()
    {
        return _Size;
    }

    public void SetVelocity(Vector2 direction)
    {
        _Body.velocity = direction * _Speed;
    }

    public Vector2 GetVelocity()
    {
        return _Body.velocity;
    }

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.x - racketPos.x) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Paddle1") {
            float x = HitFactor(transform.position,
                          other.transform.position,
                          other.collider.bounds.size.x);

            Vector2 dir = new Vector2(x, 1).normalized;
            _Body.velocity = dir * _Speed;
        }
            
        if (other.gameObject.name == "Paddle2") {
            float x = HitFactor(transform.position,
                          other.transform.position,
                          other.collider.bounds.size.x);

            Vector2 dir = new Vector2(x, -1).normalized;
            _Body.velocity = dir * _Speed;
        }

        if (other.gameObject.tag == "Paddle") {
            _AudioSource.PlayOneShot(_PaddleSound);
        } else if (other.gameObject.tag == "Wall") {
            _AudioSource.PlayOneShot(_WallSound);
        }
    }
}
