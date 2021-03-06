﻿using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour
{

		public int acceleration = 5;
		public int maxSpeed = 5;
		public int jumpForce = 70;
		private bool isOnGround = false;
		public int playerNumber = 0;
		public Sprite[] walkingRightSprites;
		public Sprite[] walkingLeftSprites;
		public Sprite[] idlingRightSprites;
		public Sprite[] idlingLeftSprites;
		private SpriteRenderer spriteRenderer;
		public float framesPerSecond;
		int direction = 0; //-1 left, 0 stand, 1 right
		int lastDireciton = 1;

		// Use this for initialization
		void Start ()
		{
				spriteRenderer = renderer as SpriteRenderer;
		}
	
		// Update is called once per frame
		void Update ()
		{
				animate ();
				bool jump = Input.GetButton ("JumpKey");

				switch (playerNumber) {
				case 1:
						bool playerOneLeft = Input.GetButton ("PlayerOneLeftKey");
						bool playerOneRight = Input.GetButton ("PlayerOneRightKey");
			
						if (playerOneRight) {
								//this.transform.Translate (Vector2.right * speed * Time.deltaTime);
								moveRight ();
						} else if (playerOneLeft) {
								//this.transform.Translate (-Vector2.right * speed * Time.deltaTime);
								moveLeft ();
						} else {
								if (this.rigidbody2D.velocity.magnitude == 0) {
										direction = 0;
								}

						}

						break;
				case 2:
						bool playerTwoLeft = Input.GetButton ("PlayerTwoLeftKey");
						bool playerTwoRight = Input.GetButton ("PlayerTwoRightKey");
			
						if (playerTwoRight) {
								//this.transform.Translate (Vector2.right * speed * Time.deltaTime);
								moveRight ();
						} else if (playerTwoLeft) {
								//this.transform.Translate (-Vector2.right * speed * Time.deltaTime);
								moveLeft ();
						} else {
								if (this.rigidbody2D.velocity.magnitude == 0) {
										direction = 0;
								}
				
						}
						break;
				}

				if (this.rigidbody2D.velocity.x > maxSpeed) {
						this.rigidbody2D.velocity = new Vector2 (maxSpeed, this.rigidbody2D.velocity.y);
				} else if (this.rigidbody2D.velocity.x < -maxSpeed) {
						this.rigidbody2D.velocity = new Vector2 (-maxSpeed, this.rigidbody2D.velocity.y);
				}
				
				float distance = (this.GetComponent<BoxCollider2D> ().size.y * 0.5f * this.transform.localScale.x) + 0.02f;
				Collider2D[] hits = Physics2D.OverlapPointAll (new Vector2 (this.transform.position.x, this.transform.position.y - distance));
				this.isOnGround = checkIfContainsJumpable (hits);		


				/*if (hits.Length >= 2) {
						this.isOnGround = true;
				} else {
						this.isOnGround = false;
				}
				//print (hit.tag);
				//Debug.DrawRay (new Vector3 (0, 0, 0), new Vector3 (this.transform.position.x, this.transform.position.y - distance, 0));
				 Vector2 origin = (Vector2)this.transform.position + (-Vector2.up * distance);
				
				RaycastHit2D hit = Physics2D.Raycast (origin, -Vector2.up, 0.01f);
				Debug.DrawRay (origin, (-Vector2.up * 0.01f));

				Debug.Log (hit.transform.tag);
				if (hit.transform.tag == "Platform") {
						this.isOnGround = true;
				} else {
						this.isOnGround = false;
				} */

				if (jump && this.isOnGround) {
						this.transform.Translate (Vector2.up * 0.016f);
						this.rigidbody2D.AddForce (new Vector2 (0, jumpForce));
						this.isOnGround = false;
				}
		}
		
		bool checkIfContainsJumpable (Collider2D[] hits)
		{
				foreach (Collider2D c in hits) {
						if (c.tag == "Platform" || c.tag == "floor" || c.tag == "Door") {
								return true;
						}
				}
				return false;
		}

		void moveRight ()
		{
				this.rigidbody2D.AddForce (Vector2.right * acceleration);
				direction = 1;
				lastDireciton = 1;
		}

		void moveLeft ()
		{
				this.rigidbody2D.AddForce (Vector2.right * -acceleration);
				direction = -1;
				lastDireciton = -1;
		}

		void animate ()
		{
				if (direction == 1) {
						int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
						index = index % walkingRightSprites.Length;
						spriteRenderer.sprite = walkingRightSprites [index];
				} else if (direction == -1) {
						int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
						index = index % walkingLeftSprites.Length;
						spriteRenderer.sprite = walkingLeftSprites [index];
				} else if (direction == 0 && lastDireciton == 1) {
						int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
						index = index % idlingRightSprites.Length;
						spriteRenderer.sprite = idlingRightSprites [index];
				} else if (direction == 0 && lastDireciton == -1) {
						int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
						index = index % idlingLeftSprites.Length;
						spriteRenderer.sprite = idlingLeftSprites [index];
				}
		}

}
	