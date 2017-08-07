using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip sound1;
	public AudioClip sound2;
	
	public float speed = 30;

	void Start() {
		audioSource = GetComponent<AudioSource> ();

		// Initial Velocity
		GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
	}

	float hitFactor(Vector2 ballPos, Vector2 racketPos,
		float racketHeight) {
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return (ballPos.x - racketPos.x) / racketHeight;
	}

	void OnCollisionEnter2D(Collision2D col) {
		// Note: 'col' holds the collision information. If the
		// Ball collided with a racket, then:
		//   col.gameObject is the racket
		//   col.transform.position is the racket's position
		//   col.collider is the racket's collider

		// Hit the left Racket?
		if (col.gameObject.name == "Paddle1") {
			// Calculate hit Factor
			float x = hitFactor(transform.position,
				col.transform.position,
				col.collider.bounds.size.x);

			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(x, 1).normalized;

			// Set Velocity with dir * speed
			GetComponent<Rigidbody2D>().velocity = dir * speed;
			audioSource.PlayOneShot (sound1);
		}

		// Hit the right Racket?
		if (col.gameObject.name == "Paddle2") {
			// Calculate hit Factor
			float x = hitFactor(transform.position,
				col.transform.position,
				col.collider.bounds.size.x);

			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(x, -1).normalized;

			// Set Velocity with dir * speed
			GetComponent<Rigidbody2D>().velocity = dir * speed;
			audioSource.PlayOneShot (sound1);
		}

		if (col.gameObject.tag == "Wall") {
			audioSource.PlayOneShot (sound2);
		}
	}
}
