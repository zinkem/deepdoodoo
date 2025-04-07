using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour {

	[SerializeField] public UnityEvent takeAction;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

  void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("Triggered!");
    if(collision.gameObject.tag == "Player") {
    	takeAction?.Invoke();
		}
  }
}
