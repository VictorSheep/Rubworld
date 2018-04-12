using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPickup : MonoBehaviour {

	public FountainFoodItem ffitem;

	public void GetIt() {
		GameManager.instance.PlayerGetItem (ffitem);
		RubStates.instance.GetReward ();
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider collider)
	{
		//SceneManager.LoadScene("test_scene02");
		if (collider.tag == "Player") {
			GetIt ();
		}
	}

}
