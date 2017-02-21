using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{

	public int id;
	//	public Vector3 velocityCached;
	protected Rigidbody rigidBody;

	//	private const float SIZE = 0.5f;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		if (rigidBody.position.y < -50) {
			resetPosition ();
			return;
		}

		if (rigidBody.position.y < -1) {
			return;
		}

		Move ();
//		cacheVelocity ();
	}

	void Move ()
	{
		Vector3 movement = Movement ();

		rigidBody.AddForce (movement, ForceMode.Impulse);
	}

	protected abstract Vector3 Movement ();

	private void resetPosition ()
	{
		rigidBody.velocity = Vector3.zero;
		if (id == 0) {
			transform.position = new Vector3 (3, 1, 0);	
		} else if (id == 1) {
			transform.position = new Vector3 (0, 1, -3);	
		} else if (id == 2) {
			transform.position = new Vector3 (-3, 1, 0);	
		} else if (id == 3) {
			transform.position = new Vector3 (0, 1, 3);	
		}	
	}
}
