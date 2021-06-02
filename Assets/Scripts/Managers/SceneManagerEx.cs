using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
  public BaseScene CurrentScene { get => GameObject.FindObjectOfType<BaseScene>(); }

  public void LoadScene(Define.Scene type)
  {
    Managers.Clear();
    SceneManager.LoadScene(GetSceneName(type));
  }

  string GetSceneName(Define.Scene type) => System.Enum.GetName(typeof(Define.Scene), type);


  public void Clear()
  {
    CurrentScene.Clear();
  }
}
