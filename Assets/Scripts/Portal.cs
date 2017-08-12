using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public Vector2 _Direction;
    public Portal _LinkedPortal;
    public Collider2D _Collider;

    private Vector2 _Size;

    void Start()
    {
        _Size = _Collider.bounds.size;
    }

    public Vector3 GetTeleportPosition(Ball ball)
    {
        Vector3 newPos = transform.position;
        newPos.x += _Direction.x * (_Size.x + ball.GetSize().x) / 2;
        return newPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball") {
            Debug.Log("Portal");
            Ball ball = other.GetComponent<Ball>();
            ball.transform.position = _LinkedPortal.GetTeleportPosition(ball);

            Vector2 newVelocity = ball.GetVelocity();
            if (_Direction.x != 0 && Mathf.Sign(_Direction.x) != Mathf.Sign(newVelocity.x)) {
                newVelocity.x *= -1;
            }

            if (_Direction.y != 0 && Mathf.Sign(_Direction.y) != Mathf.Sign(newVelocity.y)) {
                newVelocity.y *= -1;
            }

            ball.SetVelocity(newVelocity);
        }
    }
}
