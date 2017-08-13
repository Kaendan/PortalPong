using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAuto : Paddle
{
    GameObject _Ball;
    Rigidbody2D _BallBody;
    Vector2 _BallVelocity;

    Vector2 _PortalPos = Vector2.zero;

    public override float GetDirection()
    {
        float x = 0;

        if (_Ball == null) {
            _Ball = GameObject.FindGameObjectWithTag("Ball");
            if (_Ball != null) {
                _BallBody = _Ball.GetComponent<Rigidbody2D>();
            }
        }

        if (_Ball != null) {
            if (_BallVelocity != _BallBody.velocity) {
                _BallVelocity = _BallBody.velocity;

                RaycastHit2D hit = Physics2D.Raycast(_Ball.transform.position, _BallVelocity, 10f, 1 << LayerMask.NameToLayer("PortalWall"));
                if (hit.collider != null) {
 
                    Vector2 newPos = _Portal.transform.position;
                    newPos.y = hit.point.y;
                    if (Mathf.Sign(newPos.x) != Mathf.Sign(hit.point.x)) {
                        newPos.x *= -1f;
                    }

                    _PortalPos = newPos;
                    StopCoroutine("MovePortal");
                    StartCoroutine("MovePortal");
                }
            }

            if (_BallBody.velocity.y > 0) {
                if (_Ball.transform.position.x > this.transform.position.x + _Size.x / 2) {
                    x = 1;
                } else if (_Ball.transform.position.x < this.transform.position.x - _Size.x / 2) {
                    x = -1;
                }

            }
        } else {
            if (0 > this.transform.position.x + 0.1f) {
                x = 1;
            } else if (0 < this.transform.position.x - 0.1f) {
                x = -1;
            }
        }

        return x;
    }

    IEnumerator MovePortal()
    {
        yield return new WaitForSeconds(0.5f);

        if (Mathf.Sign(_PortalPos.x) != Mathf.Sign(_Portal.transform.position.x)) {
            _Portal.ChangeDirection();
        }
        _Portal.transform.position = _PortalPos;
        _PortalPos = Vector2.zero;
    }
}
