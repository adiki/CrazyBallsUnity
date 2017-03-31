using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ball
{

	protected override Vector3 Movement ()
	{
		float x, z;
		float limit = 0.3f;
		x = Input.acceleration.x;
		x = Mathf.Min (Mathf.Max (-limit, x), limit);
		z = Input.acceleration.y;
		z = Mathf.Min (Mathf.Max (-limit, z), limit);

		Vector3 movement = new Vector3 (x, 0.0f, z);

		float x2, z2;
		x2 = Input.GetAxis ("Horizontal");
		x2 = Mathf.Min (Mathf.Max (-limit, x2), limit);
		z2 = Input.GetAxis ("Vertical");
		z2 = Mathf.Min (Mathf.Max (-limit, z2), limit);

		if (x2 != 0 || z2 != 0) {
			movement = new Vector3 (x2, 0.0f, z2);
		}

		return movement * 4f;
	}
}
