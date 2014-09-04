using UnityEngine;
using System.Collections;

public class MinaBehavior : MonoBehaviour {
	public int minerals = 10;
	private bool depleted = false;
	void Start () {
	
	}
	
	void Update () {
	
	}
	public bool GetMineral() {
		if (minerals > 0) {
			minerals--;
			if (minerals == 0)
				depleted = true;
			return true;
		}
		return false;

	}
	public bool Status() {
		return depleted;
	}
}
