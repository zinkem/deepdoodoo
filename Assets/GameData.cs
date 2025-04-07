using UnityEngine;


public class GameData {

  private static GameData instance;

  public static GameData Get() {
    instance ??= new GameData();
    return instance;
  }

  public static GameData Reset() {
    instance = new GameData();
    return instance;
  }

  public static void AddGold(int amount) {
    GameData gd = Get();
    gd.gold += amount;
  }

  public static int Gold() {
    GameData gd = Get();
    return gd.gold;
  }

  public int gold;
  public float attackRadiusMod;
  public float collectRadiusMod;
  public float attackShotsMod;

  public float depth;

  public GameData() {
    gold = 0;
    attackRadiusMod = 1;
    collectRadiusMod = 1;
    attackShotsMod = 1;
  }



}