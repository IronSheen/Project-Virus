﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector3 velocity;
	public int damage;
	public GameObject parent;
	public float range;
	public Transform target;

	public Enemy.Type effectiveAgainst;

	// Update is called once per frame
	void FixedUpdate () {

		CastRay ();
		transform.position += velocity * Time.fixedDeltaTime;

	}

	public void CastRay () {
		Ray ray = new Ray (transform.position, transform.right * velocity.magnitude * Time.fixedDeltaTime);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, velocity.magnitude * Time.fixedDeltaTime)) {

			if (hit.collider.gameObject.layer != parent.layer) {

				hit.collider.SendMessage ("OnTakeDamage", new Projectile.Damage (damage, effectiveAgainst), SendMessageOptions.DontRequireReceiver);
				Destroy (gameObject);

			}

		}
	}

	public class Damage {
		public int damage;
		public Enemy.Type effectiveAgainst;

		public Damage (int damage, Enemy.Type effectiveAgainst) {
			this.damage = damage;
			this.effectiveAgainst = effectiveAgainst;
		}
	}
}