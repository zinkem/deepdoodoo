using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour {


	public GameObject dashFx;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		PlayerInput pi = GetComponent<PlayerInput>();
		InputAction inact = pi.actions.FindActionMap("Player").FindAction("Jump");
		inact.canceled += OnJumpReleased;

	}

	// Update is called once per frame
	void FixedUpdate() {
		if(jumpHeld) {
			transform.localScale = Vector3.one * .8f;
			Rigidbody2D body = GetComponent<Rigidbody2D>();
			body.AddForce(Vector2.down * body.mass * 350);
			if(dashFx) dashFx.SetActive(true);
		} else {
			transform.localScale = Vector3.one;
			if(dashFx) dashFx.SetActive(false);
		}
	}

	private bool jumpHeld = false;
	void OnJump(InputValue value) {
		Debug.Log("Jump pressed");
		jumpHeld = true;
	}

	float hitTime = 0;
	float jumpReleaseTime = 0;
	GameObject jumpingOn;
	void OnJumpReleased(InputAction.CallbackContext con) {
		// should account for if object is destroyed?
		if(!this.gameObject.scene.isLoaded) return;

		Debug.Log("Jump released");
		jumpReleaseTime = Time.fixedTime;
		jumpHeld = false;

		if(Time.fixedTime - hitTime < 0.5f) {
			Debug.Log("HIT SUCCESS");
			Rigidbody2D body = GetComponent<Rigidbody2D>();
			body.AddForce(body.linearVelocity.normalized * body.mass * 200, ForceMode2D.Impulse);
			if(jumpingOn) {
				DestroyFxScript des = jumpingOn.GetComponent<DestroyFxScript>();
				if(des) des.DestroyWithFx(true);
				jumpingOn = null;
			}
		}

	}

  void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag != "Danger") return;

		if(jumpHeld) {
			StartCoroutine(EvalHit());
			jumpingOn = collision.gameObject;
			hitTime = Time.fixedTime;
			jumpingOn.transform.localScale = new Vector3(1f, .5f, 1f);
		} else {
			TakeHit();
		}
  }

	int touchCounter = 0;
	void OnCollisionStay2D(Collision2D collision) {
		touchCounter++;

		if(touchCounter > 100) {
			Destroy(gameObject);
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		touchCounter = 0;
	}

	IEnumerator EvalHit() {
		yield return new WaitForSeconds(0.5f);
		if(jumpHeld && jumpingOn != null) {
			TakeHit();
			jumpingOn.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		}
	}

	void TakeHit() {
		GetComponent<PlayerMovement>().enabled = false;
			Rigidbody2D body = GetComponent<Rigidbody2D>();
			body.linearVelocity = Vector2.zero;
			StartCoroutine(Unstun(.1f));
	}

	IEnumerator Unstun(float t) {
		yield return new WaitForSeconds(t);
		GetComponent<PlayerMovement>().enabled = true;
		Destroy(gameObject);
	}

  void OnDestroy() {
		// clean up the listener so it does not attempt to run after object is destroyed
		PlayerInput pi = GetComponent<PlayerInput>();
    InputAction inact = pi.actions.FindActionMap("Player").FindAction("Jump");
		inact.canceled -= OnJumpReleased;
  }

}
