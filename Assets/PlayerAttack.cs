using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour {

  public GameObject ammo;
  public float attackCooldown = .2f;
  public float attackRadius = 64f;

  private float lastAttackTime = 0;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    PlayerInput pi = GetComponent<PlayerInput>();
    InputAction inact = pi.actions.FindActionMap("Player").FindAction("Attack");
    inact.canceled += OnAttackReleased;
  }

  // Update is called once per frame
  void FixedUpdate() {
    if(attackHeld && (Time.fixedTime - lastAttackTime) > attackCooldown ) {

      RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRadius, Vector2.down, 0f);

      Transform targetLock = null;
      foreach(RaycastHit2D hit in hits){
        if(hit.transform.gameObject.tag == "Danger") {
          targetLock = hit.transform;
          break;
        }
      }

      if(targetLock) {
        GameObject attack = Instantiate(ammo, transform);

        LineRenderer lr = attack.GetComponent<LineRenderer>();
        if(lr) {
          lr.SetPosition(0, transform.position);
          lr.SetPosition(1, targetLock.position);
        }

        StartCoroutine(Hit(attack, targetLock.gameObject, 0.1f));
      }

      lastAttackTime = Time.fixedTime;
    }
  }

  IEnumerator Hit(GameObject attackProjectile, GameObject victim, float t) {
    yield return new WaitForSeconds(t);
    if(attackProjectile.GetComponent<DestroyFxScript>()) {
      attackProjectile.GetComponent<DestroyFxScript>().DestroyWithFx();
    } else {
      Destroy(attackProjectile);
    }

    if(victim != null && victim.GetComponent<DestroyFxScript>()) {
      victim.GetComponent<DestroyFxScript>().DestroyWithFx(true);
    } else {
      Destroy(victim);
    }
  }

  private bool attackHeld = false;
  void OnAttack(InputValue value) {
    attackHeld = true;
  }

  void OnAttackReleased(InputAction.CallbackContext con) {
    attackHeld = false;
  }
}
