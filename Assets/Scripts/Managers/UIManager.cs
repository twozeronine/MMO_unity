using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
  int _order = 0;

  Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

  public T ShowPopupUI<T>(string name = null) where T : UI_Popup
  {
    //string을 안넣었을경우 Type으로 받아온다.
    //Type과 string을 일치 시켰기때문에 가능함.
    // ex ) UI_Button 프리팹에 붙어있는 스크립트는 UI_Button 
    if (string.IsNullOrEmpty(name))
      name = typeof(T).Name;

    GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
    // 혹시라도 Prefab에 컴포넌트를 안붙여놨을경우
    T popup = Util.GetOrAddComponent<T>(go);
    _popupStack.Push(popup);
    return popup;
  }



  // 제일 마지막으로 뜬 팝업창을 꺼줌.
  // 스택에 쌓인 순서대로 팝업이 켜졌을테니 순서대로 종료됌.
  public void ClosePopupUI()
  {
    if (_popupStack.Count == 0) return;

    UI_Popup popup = _popupStack.Pop();
    // popup 컴포넌트를 가진 게임오브젝트를 삭제.
    Managers.Resource.Destroy(popup.gameObject);
    popup = null;
  }

  // 혹시라도 다른 스크립트에서 팝업을 종료했을시
  // 해당 팝업이 아닌 다른 팝업을 종료할 수 도있기 때문에 안전하게 제거하기 위한 메소드
  public void ClosePopupUI(UI_Popup popup)
  {
    if (_popupStack.Count == 0) return;

    if (_popupStack.Peek() != popup)
    {
      Debug.Log("Close Popup Falied!");
      return;
    }
    ClosePopupUI();
  }

  public void CloseAllPopupUI()
  {
    while (_popupStack.Count > 0)
      ClosePopupUI();
  }


}