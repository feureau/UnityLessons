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

public class PlayerControl : MonoBehaviour {
	public float speed,pitchMultiplier,rollMultiplier;

	public Boundary boundary;

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement1 = new Vector3(moveHorizontal,0.0f,moveVertical);
		rigidbody.velocity = movement1*speed;
		rigidbody.position = new Vector3(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
			);
		rigidbody.rotation = Quaternion.Euler (
			rigidbody.velocity.z*pitchMultiplier, 
			0.0f, 
			rigidbody.velocity.x*rollMultiplier*-1);
	}
}