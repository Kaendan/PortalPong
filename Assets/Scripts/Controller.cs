using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class : Used by ControllerPlayer and ControllerAI - Controls a paddle and a portal
public abstract class Controller : MonoBehaviour
{
    // the paddle's Speed
    public float _Speed = 5;
    // Axis used to move the paddle : Only used for debugging on computer
    public string _Axis = "Horizontal";
    // Whether the paddle is at top or the bottom
    public bool _Top = false;
    // The portal used with this paddle
    public Portal _Portal;

    public Collider2D _Collider;

    // The Y direction of the paddle
    protected int _DirectionY;
    // Size of the collider bounds
    protected Vector2 _Size;

    // Paddle bounds : Used to restrain movements
    protected float _MinBound = -2.2f;
    protected float _MaxBound = 2.2f;

    void Start()
    {
        if (_Top) {
            _DirectionY = -1;
        } else {
            _DirectionY = 1;
        }

        _Size = _Collider.bounds.size;
    }

    void FixedUpdate()
    { 
        // Manages inputs
        float x = _Speed * ManageInputs() * Time.deltaTime;
        // Moves the paddle
        transform.Translate(x, 0, 0);

        // Prevents the paddle from going out of its bounds
        if (transform.position.x > _MaxBound) {
            transform.position = new Vector3(_MaxBound, transform.position.y, 0);
        } else if (transform.position.x < _MinBound) {
            transform.position = new Vector3(_MinBound, transform.position.y, 0);
        }
    }

    // Manages inputs and returns the new X position
    public abstract float ManageInputs();

    void OnCollisionEnter2D(Collision2D other)
    {
        // If the paddle hits a Ball : Changes the ball's velocity depending on its X coordinate
        if (other.gameObject.tag == "Ball") {
            float x = (other.transform.position.x - transform.position.x) / _Size.x;

            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.SetSpeed(ball.GetSpeed() * 1.1f); // Increases the ball's speed
            ball.SetVelocity(new Vector2(x, _DirectionY).normalized);
        }
    }

}
