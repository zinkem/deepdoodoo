using UnityEngine;

public class CollectRewards : MonoBehaviour {

  public float collectRadius = 48;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {

  }

  // Update is called once per frame
  void FixedUpdate() {
    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, collectRadius, Vector2.down, 0f);

    foreach(RaycastHit2D hit in hits) {
      if( hit.transform.gameObject.tag == "Reward") {
        Vector2 diff = (transform.position - hit.transform.position);
        float distance = diff.magnitude;

        Vector2 dir = diff.normalized;

        Rigidbody2D otherBody = hit.transform.GetComponent<Rigidbody2D>();
        otherBody.AddForce(dir * 100, ForceMode2D.Impulse);
      }
    }
  }


  void OnCollisionEnter2D(Collision2D collision) {
    if(collision.gameObject.tag != "Reward") return;

    DestroyFxScript destroy = collision.gameObject.GetComponent<DestroyFxScript>();
    destroy.DestroyWithFx();
    GameData.Get().gold++;
  }
}
