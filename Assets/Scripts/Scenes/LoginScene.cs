using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
  protected override void Init()
  {
    base.Init();

    SceneType = Define.Scene.Login;

    List<GameObject> list = new List<GameObject>();
    for (int i = 0; i < 2; i++)
    {
      GameObject go = Managers.Resource.Instantiate("UnityChan");
      list.Add(go);
    }

    foreach (GameObject obj in list)
      Managers.Resource.Destroy(obj);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      Managers.Scene.LoadScene(Define.Scene.Game);
    }
  }
  public override void Clear()
  {
    Debug.Log("LoginScene Clear!");
  }
}
