using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{


  // Init 호출 부분이 없어도 BaseScene에 Awake가 실행되면서 Init을 실행하게 되는데
  // 가상함수의 특성상 가상 함수 테이블을 참조해서 override 된 함수가 있으면 그버전으로 먼저 호출해준다.
  // 따라서 GameScene의 Init이 호출된다.


  protected override void Init()
  {
    base.Init();
    SceneType = Define.Scene.Game;
    // Managers.UI.ShowSceneUI<UI_Inven>();
    Dictionary<int, Data.Stat> Statdict = Managers.Data.StatDict;
    gameObject.GetOrAddComponent<CursorController>();


    GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
    Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
    Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
  }


  public override void Clear()
  {

  }
}
