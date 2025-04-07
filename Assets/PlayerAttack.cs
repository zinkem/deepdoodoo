using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerAttack : MonoBehaviour {

  public GameObject ammo;
  public Light2D atkRadius;
  public float attackCooldown = .2f;
  public float attackRadius = 64f;

  private float lastAttackTime = 0;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    PlayerInput pi = GetComponent<PlayerInput>();
    InputAction inact = pi.actions.FindActionMap("Player").FindAction("Attack");
    inact.canceled += OnAttackReleased;
  }

  private float CurrentAttackRadius() {
    return attackRadius * GameData.Get().attackRadiusMod;
  }

  // Update is called once per frame
  void FixedUpdate() {

    GameData data = GameData.Get();
    data.attackRadiusMod = 1 + (data.gold/1000f);
    data.attackShotsMod = data.gold/100f;

    if(atkRadius) {
      atkRadius.pointLightOuterRadius = CurrentAttackRadius();
      atkRadius.pointLightInnerRadius = CurrentAttackRadius();
    }

    if(attackHeld && (Time.fixedTime - lastAttackTime) > attackCooldown ) {

      RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, CurrentAttackRadius(), Vector2.down, 0f);
      int targets_hit = 0;
      Transform targetLock = null;
      foreach(RaycastHit2D hit in hits){
        if(hit.transform.gameObject.tag == "Danger") {
          targetLock = hit.transform;
          targets_hit++;

          GameObject attack = Instantiate(ammo, transform);

          LineRenderer lr = attack.GetComponent<LineRenderer>();
          if(lr) {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetLock.position);
          }

          StartCoroutine(Hit(attack, targetLock.gameObject, 0.1f));

          if(targets_hit > GameData.Get().attackShotsMod) {
            break;
          }
        }
      }

      lastAttackTime = Time.fixedTime;
    }

    if(attackHeld) {
      Rigidbody2D body = GetComponent<Rigidbody2D>();
      body.AddForce(Vector2.up * body.mass * 200);
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
      victim.GetComponent<DestroyFxScript>().DestroyWithFx(false);
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
