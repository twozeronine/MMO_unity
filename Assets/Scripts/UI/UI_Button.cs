using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
  Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
  enum Buttons
  {
    PointButton
  }

  enum Texts
  {
    PointText,
    ScoreText
  }

  private void Start()
  {
    Bind<Button>(typeof(Buttons));
    Bind<Text>(typeof(Texts));
  }

  void Bind<T>(Type type) where T : UnityEngine.Object
  {
    // Enum 타입에서 이름을 string으로 얻어옴
    string[] names = Enum.GetNames(type);
    // Enum에서 선언한 갯수만큼 object배열을 만듬
    UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
    // 해당 배열을 딕셔너리에 넣음 나중에 Type을 통해서 배열 접근 가능.
    _objects.Add(typeof(T), objects);

    for (int i = 0; i < names.Length; i++)
    {
      objects[i] = Util.FindChild<T>(gameObject, names[i], true);
    }
  }

  int _score = 0;
  public void OnButtonClicked()
  {
    _score++;
  }
}
