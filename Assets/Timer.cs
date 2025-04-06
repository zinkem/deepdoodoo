using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
  public static string zeroPad(string toPad, int targetLength){

    if(toPad.Length >= targetLength)
      return toPad;

    int padSize = targetLength - toPad.Length;
    return new string('0', padSize) + toPad;

  }
  public static string TimeToString(float time) {
    int minutes = (int) (time / 60);
    string minutesString = zeroPad(minutes.ToString(), 2);

    int seconds = (int) (time - minutes * 60);
    string secondsString = zeroPad(seconds.ToString(), 2);

    int rest = (int)((time % 1) * 100);
    string restString = zeroPad(rest.ToString(), 2);

    return minutesString + ":" + secondsString + ":" + restString;

  }

  //end static

  TMP_Text tmp;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
      tmp = GetComponent<TMP_Text>();
  }

  // Update is called once per frame
  void Update() {
    tmp.text = TimeToString(Time.fixedTime);
  }
}
