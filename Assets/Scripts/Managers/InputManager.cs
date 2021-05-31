using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
  //옵저버 패턴
  public Action KeyAction = null;
  public void OnUpdate()
  {
    if (Input.anyKey == false) return;

    if (KeyAction != null)
      KeyAction.Invoke();
  }
}
