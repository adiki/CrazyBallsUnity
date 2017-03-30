using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ball : MonoBehaviour
{
	public Text pointsText;
	public GameObject resultPanel;

	public bool started;
	public Vector3 velocityCached;
	public Rigidbody rigidBody;
	public List<Hit> lastHits = new List<Hit>();
	public float movementFactor = 2;

	public int points;

	protected abstract Vector3 Movement ();

	public void AddPoint ()
	{
		points += 1;
		pointsText.text = points.ToString ();
	}

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		if (!started) {
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
		if (rigidBody.position.y > 0.01) {
			return;
		}

		Vector3 movement = Movement ();

		rigidBody.AddForce (movement, ForceMode.Impulse);
	}

	void OnCollisionEnter (Collision collision)
	{
		if (!collision.collider.gameObject.CompareTag ("Player")) {
			return;
		}

		Ball otherPlayer = collision.collider.gameObject.GetComponent<Ball> ();

		lastHits.Insert (0, new Hit(otherPlayer, DateTime.Now));
		if (lastHits.Count > 2) {
			lastHits.RemoveAt (2);
		}

		if (collision.contacts.Length == 0) {
			return;
		}

		Vector3 normal = collision.contacts [0].normal;

		Vector3 velocityCachedOther = otherPlayer.velocityCached;
		float angle = Vector3.Angle (-normal, velocityCachedOther);
		float appliedRecoil = Mathf.Max (0, (angle / 180 - 0.5f)) * 2;

		float factor = velocityCachedOther.magnitude * otherPlayer.rigidBody.mass / rigidBody.mass * 3;
		rigidBody.AddForce (normal * appliedRecoil * factor, ForceMode.Impulse);
	}

	private void CacheVelocity ()
	{
		velocityCached = new Vector3 (rigidBody.velocity.x, 0, rigidBody.velocity.z);
	}
}
