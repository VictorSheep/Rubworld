using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
		UpdatePosition ();
	}

	// Set his position to current player position
	public void UpdatePosition() {
		transform.position = player.position;
	}
}
