using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin;
	public float xMax;
	public float zMin;
	public float zMax;
	public float rotateXMax,rotateZMax;
	public float rotateXMin;
	public float rotateZMin;
}

[System.Serializable]
public class ShootingRate
{
	public float firingRate;
	public float timeTillNextFire;
}

public class PlayerControl : MonoBehaviour {
	public float speed,pitchMultiplier,rollMultiplier,pitchLimiter,rollLimiter;
	public Boundary boundary;
	public ShootingRate shootingRate;
	Vector3 velocitor, rotator;
	public GameObject shot;
	public Transform shotSpawn;

	void Update()
	{
		if ( Input.GetButton("Fire1") && (Time.time > shootingRate.timeTillNextFire) )
		{
			shootingRate.timeTillNextFire = Time.time + shootingRate.firingRate;
			shotSpawn.rotation = Quaternion.Euler(0,0,0);
			GameObject laserInstance = Instantiate (shot,shotSpawn.position,shotSpawn.rotation) as GameObject;

			Destroy (laserInstance,1.25f);
		}
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal"); //get horizontal input
		float moveVertical = Input.GetAxis ("Vertical"); // get vertical input
		Vector3 movement1 = new Vector3(moveHorizontal,0.0f,moveVertical); // assign input to movement1
		rigidbody.velocity = (movement1*speed); // velocity is input * speed multiplier
		rotator = rotator + (movement1 * speed); //rotation is saved rotation plus rigidbody.velocity
		if (velocitor != rigidbody.velocity ) {//check if velocity is the same with previous velocity
			rotator = rotator /2f; // if not speeding, rotate to almost zero
		}
		velocitor = rigidbody.velocity; // save current velocity so we can compare it on next update.
		rigidbody.velocity = new Vector3(
			Mathf.Clamp(rigidbody.velocity.x, -10, 10),
			0.0f,
			Mathf.Clamp(rigidbody.velocity.z, -10, 10)
			);
		rigidbody.position = new Vector3(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
			);
		rigidbody.rotation = Quaternion.Euler (
			Mathf.Clamp(rotator.z*pitchMultiplier,-pitchLimiter,pitchLimiter), //limit rotation to 35 on each direction
			0.0f, 
			Mathf.Clamp(rotator.x*rollMultiplier*-1,-rollLimiter,rollLimiter)); //limit rotation to 35 on each direction
	}
}