using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin;
	public float xMax;
	public float zMin;
	public float zMax;
}

public class PlayerControl : MonoBehaviour {
	public float speed,pitchMultiplier,rollMultiplier;

	public Boundary boundary;

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement1 = new Vector3(moveHorizontal,0.0f,moveVertical);
		rigidbody.velocity = movement1*speed;
		rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),0.0f,Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax));



		if (Input.anyKey==false) {
			rigidbody.AddRelativeTorque(rigidbody.rotation.x*-10, 0.0f, rigidbody.rotation.z*-10, ForceMode.VelocityChange); // if no button pressed, return to default rotation, eliminating torque
		}else{
			rigidbody.AddRelativeTorque(rigidbody.velocity.z*pitchMultiplier, 0.0f, rigidbody.velocity.x*-rollMultiplier,ForceMode.VelocityChange); //rotate roll and pitch in relation of movement
		}
		rigidbody.rotation = Quaternion.Euler( new Vector3(Mathf.Clamp(rigidbody.rotation.x, 0, 45),0.0f,Mathf.Clamp(rigidbody.rotation.z, 0, 45)));
	}
}