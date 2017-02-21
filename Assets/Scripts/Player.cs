using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ball {

	protected override Vector3 Movement() {
		float x, z;
		x = Input.acceleration.x;
		z = Input.acceleration.y;

		Vector3 movement = new Vector3 (x, 0.0f, z);

		float x2, z2;
		x2 = Input.GetAxis ("Horizontal");
		z2 = Input.GetAxis ("Vertical");

		if (x2 != 0 || z2 != 0) {
			movement = new Vector3 (x2, 0.0f, z2);
		}

		return movement;
	}
}
