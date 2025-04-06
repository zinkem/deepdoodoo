using UnityEngine;

public class CameraFollow : MonoBehaviour {

  public Vector2 offset;

  private GameObject sceneCamera;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    sceneCamera = GameObject.Find("Main Camera");
  }

  // Update is called once per frame
  void LateUpdate() {
    Vector3 newPos = sceneCamera.transform.position;
    newPos.y = transform.position.y;
    sceneCamera.transform.position = newPos + (Vector3) offset;
  }
}
