using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
  //옵저버 패턴
  public Action KeyAction = null;
  public Action<Define.MouseEvent> MouseAction = null;

  bool _pressed = false;
  public void OnUpdate()
  {
    // UI가 클릭된 상황이라면  return
    if (EventSystem.current.IsPointerOverGameObject())
      return;

    if (Input.anyKey && KeyAction != null)
      KeyAction.Invoke();

    if (MouseAction != null)
    {
      if (Input.GetMouseButton(0))
      {
        MouseAction.Invoke(Define.MouseEvent.Press);
        _pressed = true;
      }
      else
      {
        if (_pressed)
          MouseAction.Invoke(Define.MouseEvent.Click);
        _pressed = false;
      }
    }
  }
}
