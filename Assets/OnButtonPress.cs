using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OnButtonPress : MonoBehaviour
{
  [SerializeField] public UnityEvent takeAction;


  void OnAttack(InputValue value) {
   	takeAction?.Invoke();
  }

  void OnJump(InputValue value) {
   	takeAction?.Invoke();
  }

}
