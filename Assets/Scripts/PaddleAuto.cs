using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAuto : MonoBehaviour {

	//Speed of the enemy
	public float speed = 1.75F;

	//the ball
	Transform ball;

	//the ball's rigidbody 2D
	Rigidbody2D ballRig2D;

	//bounds of enemy
	public float leftBound = 4.5F;
	public float rightBound = -4.5F;

	// Use this for initialization
	void Start () {
		//Continously Invokes Move every x seconds (values may differ)
		InvokeRepeating("Move", .02F, .02F);
	}

	// Movement for the paddle
	void Move () {

		//finding the ball
		if(ball == null){
			ball = GameObject.FindGameObjectWithTag("Ball").transform;
		}

		//setting the ball's rigidbody to a variable
		ballRig2D = ball.GetComponent<Rigidbody2D>();

		//checking x direction of the ball
		if(ballRig2D.velocity.y > 0){

			//checking y direction of ball
			if(ball.position.x > this.transform.position.x+.3F){
				//move ball down if lower than paddle
				transform.Translate(Vector3.right*speed*Time.deltaTime);
			} else if(ball.position.x < this.transform.position.x-.3F){
				//move ball up if higher than paddle
				transform.Translate(Vector3.left*speed*Time.deltaTime);
			}

		}

		//set bounds of enemy
		if(transform.position.x > leftBound){
			transform.position = new Vector3(transform.position.y, leftBound, 0);
		} else if(transform.position.x < rightBound){
			transform.position = new Vector3(transform.position.y, rightBound, 0);
		}
	}
}
