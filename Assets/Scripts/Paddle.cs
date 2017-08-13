using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Paddle : MonoBehaviour
{

    public float _Speed = 5;
    public string _Axis = "Horizontal";
    public bool _Top = false;
    public Portal _Portal;
    public Collider2D _Collider;

    protected int _DirectionY;
    protected Vector2 _Size;

    protected float _MinBound = -2.2f;
    protected float _MaxBound = 2.2f;

    void Start()
    {
        if (_Top) {
            _DirectionY = -1;
        } else {
            _DirectionY = 1;
        }

        _Size = _Collider.bounds.size;
    }

    void FixedUpdate()
    { 
        float x = _Speed * GetDirection() * Time.deltaTime;
        transform.Translate(x, 0, 0);

        if (transform.position.x > _MaxBound) {
            transform.position = new Vector3(_MaxBound, transform.position.y, 0);
        } else if (transform.position.x < _MinBound) {
            transform.position = new Vector3(_MinBound, transform.position.y, 0);
        }
    }

    public abstract float GetDirection();

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball") {
            float x = (other.transform.position.x - transform.position.x) / _Size.x;
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.SetSpeed(ball.GetSpeed() * 1.1f);
            ball.SetVelocity(new Vector2(x, _DirectionY).normalized);
        }
    }

}
