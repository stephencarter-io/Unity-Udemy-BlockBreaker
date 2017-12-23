using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
	
	public bool autoPlay = false;
	private Ball ball;
	private float paddleWidth;

	void Start() {
		ball = GameObject.FindObjectOfType<Ball>();
		paddleWidth = this.GetComponent<BoxCollider2D>().size.x;
		print (paddleWidth);
	}

	// Update is called once per frame
	void Update () {
		if (!autoPlay) {
			MoveWithMouse();
		} else {
			AutoPlay();
		}
	}

	void MoveWithMouse () {
		Vector3 paddlePos = new Vector3 (0.5f, this.transform.position.y, 0);
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
		paddlePos.x = Mathf.Clamp(mousePosInBlocks, 0.0f, 15.0f);
		this.transform.position = paddlePos;
	}

	void AutoPlay() {
		Vector3 paddlePos = new Vector3 (0.5f, this.transform.position.y, 0);
		Vector3 ballPos = ball.transform.position;
		paddlePos.x = Mathf.Clamp(ballPos.x - (paddleWidth / 2), 0.0f, 15.0f);
		this.transform.position = paddlePos;
	}
}
