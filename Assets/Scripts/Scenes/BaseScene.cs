using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
  public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
  void Awake()
  {
    Init();
  }

  protected virtual void Init()
  {
    Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
    if (obj == null)
      Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
  }

  // 씬이 종료됐을때 날려줘야 하는 부분
  public abstract void Clear();
}
