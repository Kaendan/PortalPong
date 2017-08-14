using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Paddle controlled by a player
public class PaddlePlayer : Paddle
{
    // Y Limit to move the paddle
    public float _PaddleLimit;
    // Y Limit to move the portal
    public float _PortalLimit;

    // Manages inputs and returns the new X position
    public override float ManageInputs()
    {
        float x = Input.GetAxisRaw(_Axis);

        Vector2 pos = Vector2.zero;
        for (int i = 0; i < Input.touchCount; i++) {
            pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            // If there is a touch in the "paddle zone"
            if (_Top && pos.y > _PaddleLimit || !_Top && pos.y < _PaddleLimit) {
                if (pos.x < 0) { // Moves it to the left if the left side of the screen is touched
                    x = -1;
                } else { // Moves it to the right if the right side of the screen is touched
                    x = 1;
                }
                break;
            } else if (_Top && pos.y > _PortalLimit || !_Top && pos.y < _PortalLimit) { // If there is a touch in the "portal zone"
                // Compute the portal's new position
                Vector2 newPos = _Portal.transform.position;
                newPos.y = pos.y;
                // Moves the portal to the touched side of the screen (if it's not already there)
                if (Mathf.Sign(newPos.x) != Mathf.Sign(pos.x)) {
                    newPos.x *= -1f;
                    _Portal.ChangeDirection(); // Changes the portal's direction
                }

                _Portal.transform.position = newPos;
            }
        }

        return x;
    }
}
