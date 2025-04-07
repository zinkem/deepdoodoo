using TMPro;
using UnityEngine;

public enum StatMode {
  GOLD,
  DEPTH,
  SHOTS
}

public class StatDisplay : MonoBehaviour
{
  public StatMode statToPoll;

  TMP_Text tmp;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
      tmp = GetComponent<TMP_Text>();
  }

  // Update is called once per frame
  void FixedUpdate() {
    tmp.text = statToPoll switch {
      StatMode.GOLD => GameData.Get().gold.ToString(),
      StatMode.DEPTH => GameData.Get().depth.ToString(),
      StatMode.SHOTS => Mathf.Floor(GameData.Get().attackShotsMod + 1).ToString(),
      _ => ""
    };
  }
}
