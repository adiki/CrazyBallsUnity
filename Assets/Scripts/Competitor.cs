﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor : Ball
{
	private int counter;
	private Vector3 movement;

	protected override Vector3 Movement ()
	{ 
		++counter;
		counter %= 20;
		if (counter != 1) {
			return movement;
		}

		float factor = 1.5f;

		float x = rigidBody.position.x;
		float z = rigidBody.position.z;

		float factorX = Random.Range (0.1f, 0.15f);
		if (x > factor) {
			x = -factorX;
		} else if (x < -factor) {
			x = factorX;
		} else {
			x = Random.Range (0.075f, 0.15f) * Randomness.Sign ();
		}
		float factorZ = Random.Range (0.1f, 0.15f);
		if (z > factor) {
			z = -factorZ;
		} else if (z < -factor) {
			z = factorZ;
		} else {
			z = Random.Range (0.075f, 0.15f) * Randomness.Sign ();
		}

		movement = new Vector3 (x, 0.0f, z);
		movement *= movementFactor;

		return movement;
	}
}
