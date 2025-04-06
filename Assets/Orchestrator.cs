using UnityEngine;

public class Orchestrator : MonoBehaviour {

  // level blocks for level gen
  public GameObject[] levelSegment;
  public int levelLength = 10;

  int levelHeight = 284;


  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
  }

  void OnEnable() {
    foreach (Transform child in transform) {
      Destroy(child.gameObject);
    }

    GenerateLevel();
  }

  // Update is called once per frame
  void FixedUpdate() {

  }


  void GenerateLevel() {
    int choice = 1;
    for(int i = 0; i < levelLength; i++) {
      GameObject segment = Instantiate(levelSegment[choice], transform);
      Vector2 newPos = segment.transform.position;
      newPos.y -= levelHeight * i;
      segment.transform.position = newPos;
      choice = Random.Range(0, levelSegment.Length);
    }
  }

  void OnPlayerJoined() {
    Debug.Log("PLAYER JOINED!");
  }

}
