using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
  static Managers s_instance; // 유일성이 보장된다
  public static Managers Instance { get { init(); return s_instance; } } // 유일한 매니저를 갖고온다

  void Start()
  {
    init();
  }

  public void hello()
  {
    Debug.Log("hello");
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
