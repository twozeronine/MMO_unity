using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
  Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
  public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
  public void Init()
  {
    StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
  }

  // T라는 제너릭 형식이 Loader라고 이름만 바뀌었을뿐임
  Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
  {
    TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
    return JsonUtility.FromJson<Loader>(textAsset.text);
  }
}
