using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

	public float speed = 30;
	public string axis = "Horizontal";
	public Collider2D collider;

	Ray ray;
	RaycastHit2D hit;

	void Update () {
		float h = Input.GetAxisRaw(axis);

		if (Input.GetMouseButton (0)) {
			Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (pos.x < 0) {
				h = -1;
			} else {
				h = 1;
			}
		}

		GetComponent<Rigidbody2D>().velocity = new Vector2(h, 0) * speed;
	}

}
