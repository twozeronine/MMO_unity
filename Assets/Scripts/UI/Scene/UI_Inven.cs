using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
  enum GameObjects
  {
    GridPanel
  }
  void Start()
  {
    Init();
  }
  public override void Init()
  {
    base.Init();

    Bind<GameObject>(typeof(GameObjects));

    GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
    foreach (Transform child in gridPanel.transform)
      Managers.Resource.Destroy(child.gameObject);


    // 실제로는 인벤토리 정보를 참고해서 아이템을 추가.
    // 지금은 임시로 추가
    for (int i = 0; i < 8; i++)
    {
      GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item");
      item.transform.SetParent(gridPanel.transform);

      UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
      invenItem.SetInfo($"집행검{i}번");
    }
  }

}
