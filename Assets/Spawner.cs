using UnityEngine;

public class Spawner : MonoBehaviour {
  public float frequency;
  public GameObject objectToSpawn;

  public float lastSpawn = 0;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {

  }

  // Update is called once per frame
  void FixedUpdate() {

    if((Time.fixedTime - lastSpawn) > 1/frequency) {
      Instantiate(objectToSpawn, transform);
      lastSpawn = Time.fixedTime;
    }




  }
}
