using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour {

	public GameObject _player;
	public Transform _respawnPoint;

	private RubController rub;

	void Start() {
		rub = _player.GetComponent<RubController>();
	}

	void OnTriggerEnter(Collider other) {
		rub.Die ();
	}
}
