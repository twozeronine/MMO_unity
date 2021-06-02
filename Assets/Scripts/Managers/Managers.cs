using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
  static Managers s_instance; // 유일성이 보장된다
  static Managers Instance { get { init(); return s_instance; } } // 유일한 매니저를 갖고온다

  InputManager _input = new InputManager();
  ResourceManager _resource = new ResourceManager();
  UIManager _ui = new UIManager();

  //Input은 Managers.cs 인스턴스의 _input을 불러오는 프로퍼티이다. (싱글톤 불러다 주는 static 함수와 같은 역할. 프로퍼티로 구현한 것 뿐이다.)
  public static InputManager Input { get { return Instance._input; } }
  public static ResourceManager Resource { get { return Instance._resource; } }
  public static UIManager UI { get { return Instance._ui; } }
  void Start()
  {
    init();
  }

  void Update()
  {
    _input.OnUpdate();
  }

  static void init()
  {
    if (s_instance == null)
    {
      GameObject go = GameObject.Find("@Managers");
      if (go == null)
      {
        go = new GameObject { name = "@Managers" };
        go.AddComponent<Managers>();
      }

      DontDestroyOnLoad(go);
      s_instance = go.GetComponent<Managers>();
    }
  }
}
