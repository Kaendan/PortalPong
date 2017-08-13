using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePlayer : Paddle
{
    public float _TouchLimit;
    public float _PortalLimit;

    public override float GetDirection()
    {
        float x = Input.GetAxisRaw(_Axis);

        Vector2 pos = Vector2.zero;
        for (int i = 0; i < Input.touchCount; i++) {
            pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            // Si dans la zone pour bouger le paddle
            if (_Top && pos.y > _TouchLimit || !_Top && pos.y < _TouchLimit) {
                if (pos.x < 0) {
                    x = -1;
                } else {
                    x = 1;
                }
                break;
            } else if (_Top && pos.y > _PortalLimit || !_Top && pos.y < _PortalLimit) {
                Vector2 newPos = _Portal.transform.position;
                newPos.y = pos.y;
                if (Mathf.Sign(newPos.x) != Mathf.Sign(pos.x)) {
                    newPos.x *= -1f;
                    _Portal.ChangeDirection();
                }

                _Portal.transform.position = newPos;
            }
        }

        return x;
    }
}
