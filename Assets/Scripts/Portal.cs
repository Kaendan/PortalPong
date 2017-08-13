using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal _LinkedPortal;
    public Collider2D _Collider;
    public Vector2 _Direction;

    private Vector2 _Size;
    private int _Index = 0;

    void Start()
    {
        _Size = _Collider.bounds.size;
    }

    public int GetIndex()
    {
        return _Index;
    }

    public Vector2 GetDirection()
    {
        return _Direction;
    }

    public void SetDirection(Vector2 direction)
    {
        _Direction = direction;
    }

    public Vector2 GetTeleportPosition(Ball ball)
    {
        Vector2 newPos = transform.position;
        newPos.x += _Direction.x * ((_Size.x + ball.GetSize().x) / 2 + 0.03f);
        newPos.y += ball.transform.position.y - _LinkedPortal.transform.position.y;
        return newPos;
    }

    public void ChangeDirection()
    {
        _Direction.x *= -1;
        Debug.Log("Direction : " + _Direction);
    }

    public void Teleport(Ball ball)
    {
        ball.transform.position = GetTeleportPosition(ball);
        Debug.Log("Velocity" + ball.GetVelocity());

        Vector2 direction = Vector2.zero;
        if (ball.GetVelocity().x != 0) {
            direction.x = Mathf.Abs(ball.GetVelocity().x) / ball.GetVelocity().x;
        }

        if (ball.GetVelocity().y != 0) {
            direction.y = Mathf.Abs(ball.GetVelocity().y) / ball.GetVelocity().y;
        }

        Debug.Log("Portal 2 : " + direction);

        if (_Direction.x != 0 && Mathf.Sign(_Direction.x) != Mathf.Sign(direction.x)) {
            direction.x *= -1;
        }

        ball.SetDirection(direction);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball") {
            Ball ball = other.gameObject.GetComponent<Ball>();
            _LinkedPortal.Teleport(ball);
        }
    }
}
