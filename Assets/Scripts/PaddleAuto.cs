using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Paddle controlled by AI
public class PaddleAuto : Paddle
{
    GameObject _Ball;
    Rigidbody2D _BallBody;
    Vector2 _BallVelocity;

    Vector2 _PortalPos = Vector2.zero;

    // Manages inputs and returns the new X position
    public override float ManageInputs()
    {
        float x = 0;

        if (_Ball == null) {
            // Finds a ball - I should assign it directly from the spawner it would be better. FindGameObjectWithTag() has a cost...
            _Ball = GameObject.FindGameObjectWithTag("Ball");
            if (_Ball != null) {
                _BallBody = _Ball.GetComponent<Rigidbody2D>();
            }
        }

        if (_Ball != null) {
            // If the ball's velocity has changed since the last time we checked it
            if (_BallVelocity != _BallBody.velocity) {
                // Register the ball's velocity
                _BallVelocity = _BallBody.velocity;

                // Look for a "PortalWall" in the ball's direction
                RaycastHit2D hit = Physics2D.Raycast(_Ball.transform.position, _BallVelocity, 10f, 1 << LayerMask.NameToLayer("PortalWall"));
                if (hit.collider != null) {
                    // Compute the portal's new position
                    Vector2 newPos = _Portal.transform.position;
                    newPos.y = hit.point.y;
                    if (Mathf.Sign(newPos.x) != Mathf.Sign(hit.point.x)) {
                        newPos.x *= -1f;
                    }

                    // Saves the new position for later
                    _PortalPos = newPos;

                    // If we planned to spawn a portal, cancel this action
                    StopCoroutine("MovePortal");
                    // Plan to spawn a portal after some time
                    StartCoroutine("MovePortal");
                }
            }

            // If the ball is going up
            if (_BallBody.velocity.y > 0) {
                // Follow the ball's position so that the paddle is able to hit the ball
                // I could have done that with raycast too. To guess where the ball will hit the paddle and plan to move the paddle there with a delay (so that it's not unbeatable)
                if (_Ball.transform.position.x > this.transform.position.x + _Size.x / 2) {
                    x = 1;
                } else if (_Ball.transform.position.x < this.transform.position.x - _Size.x / 2) {
                    x = -1;
                }

            }
        } else {
            // If there is no ball move the paddle to the center of screen
            if (0 > this.transform.position.x + 0.1f) { // 
                x = 1;
            } else if (0 < this.transform.position.x - 0.1f) {
                x = -1;
            }
        }

        return x;
    }

    // Moves the portal to the _PortalPos after some time
    IEnumerator MovePortal()
    {
        yield return new WaitForSeconds(0.5f);

        // if the portal is not on the good wall
        if (Mathf.Sign(_PortalPos.x) != Mathf.Sign(_Portal.transform.position.x)) {
            // Changes the portal's direction
            _Portal.ChangeDirection();
        }
        // Changes the portal's position to the previously guessed position
        _Portal.transform.position = _PortalPos;
        _PortalPos = Vector2.zero;
    }
}
