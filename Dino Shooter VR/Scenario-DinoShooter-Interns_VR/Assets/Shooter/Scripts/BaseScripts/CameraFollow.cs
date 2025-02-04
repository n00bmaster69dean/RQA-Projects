﻿using UnityEngine;

namespace U4K.BaseScripts
{
	public class CameraFollow : MonoBehaviour
	{

		[SerializeField] public GameObject player; //Public variable to store a reference to the player game object
		private Vector3 pOffset; //Private variable to store the offset distance between the player and camera
		public bool isFP;

		// Use this for initialization
		void Start()
		{
			//Calculate and store the offset value by getting the distance between the player's position and camera's position.
			if (player != null && !isFP)
			{
				pOffset = transform.position - player.transform.position;
			}
		}

		// LateUpdate is called after Update each frame
		void LateUpdate()
		{
			// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
			if (player != null && !isFP)
			{
				transform.position = player.transform.position + pOffset;
			}
		}
	}
}
