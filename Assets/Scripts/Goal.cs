using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour {

	private int score = 0;
	public Text text;
	// Use this for initialization
	void Start () {
		updateScore ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Ball") {
			Debug.Log ("Goal!");
			score++;
			updateScore ();
		}
	}

	void updateScore() {
		text.text = score.ToString ();
	}
}
