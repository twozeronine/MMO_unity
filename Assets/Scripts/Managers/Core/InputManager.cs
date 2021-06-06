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
  float _pressedTime = 0;
  public void OnUpdate()
  {
    // UI가 클릭된 상황이라면  return
    if (EventSystem.current.IsPointerOverGameObject())
      return;

    // 등록된 키보드 액션이 없으면서 키가 안눌렸으면 실행하지 않음
    if (Input.anyKey && KeyAction != null)
      KeyAction.Invoke();

    // 등록된 마우스 액션이 없으면 실행하지 않음
    if (MouseAction != null)
    {
      // 왼쪽 마우스가 눌렸을시
      if (Input.GetMouseButton(0))
      {
        // 마우스가 클릭 안돼있었다면
        if (!_pressed)
        {
          MouseAction.Invoke(Define.MouseEvent.PointerDown);
          _pressedTime = Time.time;
        }

        // 마우스를 계속 누르고있는 상태였다면
        MouseAction.Invoke(Define.MouseEvent.Press);
        _pressed = true;
      }

      // 마우스를 안눌렀을시 혹은 누른후 땟을 때
      else
      {
        if (_pressed)
        {
          // 클릭한지 0.2초가 안되게 지났으면 클릭이다.
          if (Time.time < _pressedTime + 0.2f)
            MouseAction.Invoke(Define.MouseEvent.Click);

          // 0.2초 이상 누른후 땟으므로 클릭이 아니다.
          MouseAction.Invoke(Define.MouseEvent.PointerUp);
        }
        _pressed = false;
        _pressedTime = 0;
      }
    }
  }

  public void Clear()
  {
    KeyAction = null;
    MouseAction = null;
  }
}
