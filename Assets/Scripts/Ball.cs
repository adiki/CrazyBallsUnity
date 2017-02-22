using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{

	public int id;
	public Vector3 velocityCached;
	protected Rigidbody rigidBody;

	protected abstract Vector3 Movement ();

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		if (rigidBody.position.y < -50) {
			ResetPosition ();
			return;
		}

		if (rigidBody.position.y < -1) {
			return;
		}

		Move ();
		CacheVelocity ();
	}

	void Move ()
	{
		Vector3 movement = Movement ();

		rigidBody.AddForce (movement, ForceMode.Impulse);
	}

	void OnCollisionEnter (Collision collision)
	{
		if (!collision.collider.gameObject.CompareTag ("Player")) {
			return;
		}

		if (collision.contacts.Length == 0) {
			return;
		}

		Vector3 normal = collision.contacts [0].normal;
		Vector3 position = rigidBody.position;
		position.y += 0.5f;

		Ball otherPlayer = collision.collider.gameObject.GetComponent<Ball> ();
		Vector3 velocityCachedOther = otherPlayer.velocityCached;
		float angle = Vector3.Angle (-normal, velocityCachedOther);
		float appliedRecoil = Mathf.Max (0, (angle / 180 - 0.5f)) * 2;

		float factor = velocityCachedOther.magnitude * otherPlayer.rigidBody.mass / rigidBody.mass * 4;
		rigidBody.AddForce (normal * appliedRecoil * factor, ForceMode.Impulse);
	}

	private void CacheVelocity ()
	{
		velocityCached = new Vector3 (rigidBody.velocity.x, 0, rigidBody.velocity.z);
	}

	private void ResetPosition ()
	{
		rigidBody.velocity = Vector3.zero;
		if (id == 0) {
			transform.position = new Vector3 (0, 1, 0);	
		} else if (id == 1) {
			transform.position = new Vector3 (3, 1, -2);	
		} else if (id == 2) {
			transform.position = new Vector3 (-3, 1, -2);	
		} else if (id == 3) {
			transform.position = new Vector3 (0, 1, 3);	
		}	
	}
}
