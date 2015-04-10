﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Transform[] muzzles;

	public GameObject bullet;
	public float bulletSpeed = 80;
	public float bulletSpread = 5;
	public int bulletDamage = 10;
	public int bulletAmount = 1;
	public float bulletRange;
	public Transform target;

	public float firerate;
	public float reloadTime;
	public float sequenceTime;

	private bool canFire = true;

	// TODO Implement projectile pooling

	IEnumerator DoFire () {

		Invoke ("ChamberBullet", firerate);
		canFire = false;

		for (int m = 0; m < muzzles.Length; m++) {
			for (int i = 0; i < bulletAmount; i++) {

				GameObject newBullet = (GameObject)Instantiate (bullet, new Vector3 (muzzles[m].position.x, muzzles[m].position.y, 0), muzzles[m].rotation);
				Projectile pro = newBullet.GetComponent<Projectile>();

				pro.velocity = muzzles[m].rotation * new Vector3 (bulletSpeed * Random.Range (0.9f, 1.1f), Random.Range (-bulletSpread, bulletSpread));
				pro.parent = gameObject;
				pro.damage = bulletDamage;
				pro.range = bulletRange;
				pro.target = target;

				Destroy (newBullet, bulletRange / bulletSpeed * 1.5f);
			
			}

			yield return new WaitForSeconds (sequenceTime);

		}

	}

	public void Fire () {
		if (canFire)
			StartCoroutine ("DoFire");
	}

	void ChamberBullet () {
		canFire = true;
	}
}