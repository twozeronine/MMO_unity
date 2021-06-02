using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
  public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
  {
    if (go == null) return null;

    // 재귀적으로 찾아야되나 즉, 부모의 자식의 자식으로 계속 찾아나갈 것 인지
    if (recursive == false)
    {
      for (int i = 0; i < go.transform.childCount; i++)
      {
        Transform transform = go.transform.GetChild(i);
        if (string.IsNullOrEmpty(name) || transform.name == name)
        {
          T component = transform.GetComponent<T>();
          if (component != null)
            return component;
        }
      }
    }
    else
    {
      foreach (T component in go.GetComponentsInChildren<T>())
      {
        if (string.IsNullOrEmpty(name) || component.name == name)
          return component;
      }
    }

    return null;
  }
}
