using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
  public T Load<T>(string path) where T : Object
  {
    return Resources.Load<T>(path);
  }

  public GameObject Instantiate(string path, Transform parent = null)
  {

    // 1. original 이미 들고 있으면 바로 사용
    GameObject prefab = Load<GameObject>($"Prefabs/{path}");
    if (prefab == null)
    {
      Debug.Log($"Failed to load prefab : {path}");
      return null;
    }

    // 2. 혹시 풀링된 애가 있을까?
    // 생성되는 Prefab에서 Clone 이름 삭제
    GameObject go = Object.Instantiate(prefab, parent);
    go.name = prefab.name;

    // int index = go.name.IndexOf("(Clone)");
    // if (index > 0)
    //   go.name = go.name.Substring(0, index);

    return go;
  }

  public void Destroy(GameObject go)
  {
    if (go == null) return;

    // 만약에 풀링이 필요한 아이라면 -> 풀링 매니저 한테 보냄.

    Object.Destroy(go);
  }
}
