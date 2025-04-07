using UnityEngine;


public class GameData {

  private static GameData instance;

  public static GameData Get() {
    instance ??= new GameData();
    return instance;
  }

  public int gold;
  public float attackRadiusMod;
  public float collectRadiusMod;
  public float attackShotsMod;

  public GameData() {
    gold = 0;
    attackRadiusMod = 1;
    collectRadiusMod = 1;
    attackShotsMod = 1;
  }

}