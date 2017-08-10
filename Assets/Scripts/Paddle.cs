using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed = 30;
    public string axis = "Horizontal";
    public Collider2D collider;

    public bool top = false;

    Ray ray;
    RaycastHit2D hit;

    void Update()
    { 
        float h = Input.GetAxisRaw(axis);

        int index = -1;
        Vector2 pos = Vector2.zero;
        for (int i = 0; i < Input.touchCount; i++) {
            pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            if (top && pos.y > 0 || !top && pos.y < 0) {
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

        GetComponent<Rigidbody2D>().velocity = new Vector2(h, 0) * speed;
    }

}
