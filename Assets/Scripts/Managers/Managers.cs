using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
  static Managers s_instance; // 유일성이 보장된다
  static Managers Instance { get { init(); return s_instance; } } // 유일한 매니저를 갖고온다

  #region Contents
  GameManager _game = new GameManager();

  public static GameManager Game { get => Instance._game; }
  #endregion
  #region Core
  DataManager _data = new DataManager();
  InputManager _input = new InputManager();
  PoolManager _pool = new PoolManager();
  ResourceManager _resource = new ResourceManager();
  SceneManagerEx _scene = new SceneManagerEx();
  SoundManager _sound = new SoundManager();
  UIManager _ui = new UIManager();

  //이 값은 Managers.cs 인스턴스의 _값들을 불러오는 프로퍼티이다. (싱글톤 불러다 주는 static 함수와 같은 역할. 프로퍼티로 구현한 것 뿐이다.)
  public static DataManager Data { get => Instance._data; }
  public static InputManager Input { get { return Instance._input; } }
  public static PoolManager Pool { get { return Instance._pool; } }
  public static ResourceManager Resource { get { return Instance._resource; } }

  public static SceneManagerEx Scene { get { return Instance._scene; } }
  public static SoundManager Sound { get { return Instance._sound; } }
  public static UIManager UI { get { return Instance._ui; } }
  #endregion
  void Start()
  {
    init();
  }

  void Update()
  {
    _input.OnUpdate();
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

      s_instance._data.Init();
      s_instance._pool.Init();
      s_instance._sound.Init();
    }
  }

  public static void Clear()
  {
    Input.Clear();
    Sound.Clear();
    Scene.Clear();
    UI.Clear();

    Pool.Clear();
  }
}
