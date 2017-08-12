using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float _Speed = 5;
    public string _Axis = "Horizontal";
    public bool _Top = false;
    public Collider2D _Collider;
    public Rigidbody2D _Body;

    void Update()
    { 
        float h = Input.GetAxisRaw(_Axis);

        int index = -1;
        Vector2 pos = Vector2.zero;
        for (int i = 0; i < Input.touchCount; i++) {
            pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            if (_Top && pos.y > 0 || !_Top && pos.y < 0) {
                index = i;
                break;
            } 
        }
      
        if (index != -1) {
            if (pos.x < 0) {
                h = -1;
            } else {
                h = 1;
            }
        }

        _Body.velocity = new Vector2(h, 0) * _Speed;
    }

}
