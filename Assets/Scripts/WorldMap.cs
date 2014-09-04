using UnityEngine;
using System.Collections;

public class WorldMap : MonoBehaviour {
	public int worldSize = 10;
	public float groundWidth = 64.0f;
	public float groundHeight = 64.0f;
	public GameObject ground;

	private GameObject[,] world;
	// Use this for initialization
	void Start () {
		world = new GameObject[worldSize,worldSize];
		for (int i = 0; i < worldSize; i++) {
			for (int j = 0; j < worldSize; j++) {
				world[i,j] = (GameObject) Instantiate(ground);
				//world[i,j].renderer.position
			}
		}
	}
}
