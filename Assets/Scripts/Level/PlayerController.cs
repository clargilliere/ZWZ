using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float m_speed = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Move(float deltaTime) {
		float dist = m_speed * deltaTime;
		Vector3 pos = transform.position;
		pos.z += dist;
		transform.position = pos;
	}

	public void SetSpeed(float speed) {
		m_speed = speed;
	}

	public void OnTriggerEnter(Collider other) {
		Debug.Log ("OnTriggerEnter");
		GameItem item = other.GetComponent<GameItem> ();
		if (item != null) {
			LevelManager.ManageCollision(item);
		}
	}
}
