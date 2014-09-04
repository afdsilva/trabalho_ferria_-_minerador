using UnityEngine;
using System.Collections;

public class ChaoBehavior : MonoBehaviour {
	private int marcador = 0;
	public float decayTime = 10.0f; //a cada x segundos reduz o marcador
	private float lastTime;
	// Use this for initialization
	void Start () {
		lastTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float actualTime = Time.time;
		if (marcador > 0 && actualTime - lastTime > decayTime ) {
			marcador--;
			lastTime = Time.time; //vai atualizar o lastTime, somente se o evento de reduzir o marcador for ativado
			Debug.Log("Marcador = " + marcador);
		} else if (marcador == 0) {
			lastTime = Time.time; //atualiza o lastTime se o marcador for = 0
		}

	}
	public int getMarker() {
		return marcador;
	}
	public void addMarker() {
		marcador++;
	}
}
