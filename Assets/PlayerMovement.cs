using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
  public float speed = 30;

  public bool movementEnabled = true;

  Rigidbody2D body;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    body = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void FixedUpdate() {
    Vector2 newvel = body.linearVelocity;
    newvel.x = moveInput.x * speed;
    body.linearVelocity = newvel;

    GameData.Get().depth = Mathf.Max(-transform.position.y, GameData.Get().depth);
    GameData.maxDepth = Mathf.Max(GameData.maxDepth, -transform.position.y);
  }

  private Vector2 moveInput;
  void OnMove(InputValue value) {
    if(movementEnabled) {
      moveInput = value.Get<Vector2>();
    }

  }

}
