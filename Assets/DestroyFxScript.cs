using UnityEngine;

public class DestroyFxScript : MonoBehaviour {

  public GameObject explosion;
  public GameObject reward;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  public void DestroyWithFx(bool r = false) {
    if(!this.gameObject.scene.isLoaded) return;

    GameObject newg = Instantiate(explosion, transform.position, transform.rotation);
    newg.transform.SetParent(transform.parent);

    if(reward && r) {
      GameObject newr = Instantiate(reward, transform.position, transform.rotation);
    }

    Rigidbody2D myBody = GetComponent<Rigidbody2D>();
    Rigidbody2D[] bodies = newg.GetComponentsInChildren<Rigidbody2D>();
    foreach(Rigidbody2D body in bodies) {
      body.linearVelocity = myBody.linearVelocity;
    }

    Destroy(gameObject);
  }
}
