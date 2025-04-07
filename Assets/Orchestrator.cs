using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Orchestrator : MonoBehaviour {

  // level blocks for level gen
  public GameObject[] levelSegment;
  public GameObject[] bossSegments;
  public GameObject[] transition;
  public GameObject joinmsg;

  public int levelLength = 10;

  int levelHeight = 284;

  int currentSegmentIndex = 0;

  float startTime = 0;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
  }

  void OnEnable() {
    GameData.Reset();
    startTime = Time.fixedTime;

    foreach (Transform child in transform) {
      Debug.Log(child.name);
      if(child.gameObject.name != "ProgressMarker") {
        Destroy(child.gameObject);
      }
    }

    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Danger");

    foreach(GameObject go in enemies) {
      Destroy(go);
    }

    GenerateLevel();
  }

  // Update is called once per frame
  void FixedUpdate() {
    GameData.Get().timer = Time.fixedTime - startTime;
  }

  public void GenerateNextSegment() {
    currentSegmentIndex++;

    if(currentSegmentIndex % 10 == 0) {
      int choice = Random.Range(0, bossSegments.Length);
      GameObject segment = Instantiate(bossSegments[choice], transform);
      Vector2 newPos = segment.transform.position;
      newPos.y -= levelHeight * currentSegmentIndex;
      segment.transform.position = newPos;

      Transform tform = GetComponentInChildren<EventOnTrigger>().transform;
      tform.position = newPos;
    } else {
      int choice = Random.Range(0, levelSegment.Length);
      GameObject segment = Instantiate(levelSegment[choice], transform);
      Vector2 newPos = segment.transform.position;
      newPos.y -= levelHeight * currentSegmentIndex;
      segment.transform.position = newPos;

      Transform tform = GetComponentInChildren<EventOnTrigger>().transform;
      tform.position = newPos;
    }

  }

  void GenerateLevel() {
    currentSegmentIndex = 0;
    int choice = 1;
    GameObject segment = Instantiate(levelSegment[choice], transform);
    Transform tform = GetComponentInChildren<EventOnTrigger>().transform;
    tform.position = Vector2.zero;
  }


  public void DisableJoinFor(float t) {
    PlayerInputManager pim = GetComponent<PlayerInputManager>();
    pim.DisableJoining();
    StartCoroutine(ReenableJoin(pim, t));
  }

  IEnumerator ReenableJoin(PlayerInputManager pim, float t) {
    yield return new WaitForSecondsRealtime(t);
    pim.EnableJoining();
    if(joinmsg) joinmsg.SetActive(true);
    yield return null;
  }

}
