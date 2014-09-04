using UnityEngine;
using System.Collections;

public class MineradorBehavior : MonoBehaviour {
	public GameObject central;

	private bool lastMineDepleted = false;
	private bool loadedCargo = false;
	private bool mining = false;
	private int maxCargo = 10;
	private int qtCargo = 0;
	private GameObject lastMine;

	public float maxMineradorSpeed = 5.0f;
	private Vector2 pathOrigin;
	private Vector2 pathDestination;

	public float changeCourseTime = 1.0f;
	public float mineTiming = 5.0f;
	private float lastTime;
	private float lastTimeMining;
	public Camera cam;

	void Start() {
		lastTime = Time.time;
		lastTimeMining = Time.time;
		pathOrigin = pathDestination = rigidbody2D.position;
	}
	void Update () {
		pathOrigin = rigidbody2D.position;
		if (Input.GetMouseButton(0)) {
			pathDestination = cam.ScreenToWorldPoint(Input.mousePosition);

		}
		OnMovement ();
		//Vector2 centralPos = new Vector2 (central.transform.position.x, central.transform.position.y);
		if (Time.time - lastTimeMining < mineTiming) {
			if (qtCargo < maxCargo) {
				if (lastMine) {
					MinaBehavior mina = lastMine.GetComponent<MinaBehavior>();
					if (mina.GetMineral()) {
						Debug.Log ("Minerando");
						qtCargo++;
					} else {
						lastMineDepleted = true;
					}
				}
			} else {
				mining = false;
				loadedCargo = true;
			}
			lastTimeMining = Time.time;
		}
		if (!loadedCargo || lastMineDepleted) {
			//anda aleatoriamente a cada changeCourseTime segundos (default 1)
			if (Time.time - lastTime > changeCourseTime && !mining) {
				float newx = Random.Range(0,Screen.width);
				float newy = Random.Range (0,Screen.height);
				pathDestination = cam.ScreenToWorldPoint(new Vector2(newx, newy));
				lastTime = Time.time;
			}
		} else {
			//retorna base
		}
	}
	void OnMovement() {
		if (rigidbody2D.position != pathDestination) {
			Vector2 forces = pathDestination - pathOrigin;
			rigidbody2D.MovePosition (rigidbody2D.position + forces.normalized * maxMineradorSpeed * Time.deltaTime);
			//rigidbody2D.AddForce(forces.normalized * maxMineradorSpeed * Time.deltaTime);

			if (Mathf.Abs(forces.x) < 0.05f && Mathf.Abs(forces.y) < 0.05f)
				rigidbody2D.MovePosition(pathDestination);
		} else {
			pathOrigin = pathDestination;
		}

	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Central") {
			collider2D.isTrigger = true;
		} else {
			pathDestination = pathOrigin;
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Minerador trigger: " + other.tag);
		if (other.tag == "Mina") {
			lastMineDepleted = false;
			lastMine = other.gameObject;
			pathDestination = other.transform.position;
			mining = true;
		} else if (other.tag == "Parede") {

		}
	}
	void OnTriggerExit2D(Collider2D other) {
		collider2D.isTrigger = false;
	}
}
