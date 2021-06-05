using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
  PlayerStat _stat;
  Vector3 _destPos;

  Texture2D _attackIcon;
  Texture2D _handIcon;

  enum CursorType
  {
    None,
    Attack,
    Hand,
  }

  CursorType _cursorType = CursorType.None;

  void Start()
  {
    _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
    _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
    _stat = gameObject.GetComponent<PlayerStat>();

    // 실수로 다른 곳에서 Action을 이미 등록했다면 두번 등록이 되기 때문에 그것을 방지하기 위하여 한번 빼고 시작하는것이다.
    Managers.Input.MouseAction -= OnMouseEvent;
    Managers.Input.MouseAction += OnMouseEvent;
  }

  public enum PlayerState
  {
    Die,
    Moving,
    Idle,
    Skill,
  }


  PlayerState _state = PlayerState.Idle;
  void UpdateDie()
  {
    // 아무것도 못함
  }
  void UpdateMoving()
  {
    Vector3 dir = _destPos - transform.position;
    // 도착
    if (dir.magnitude < 0.1f)
    {
      _state = PlayerState.Idle;
    }
    else
    {
      // TODO
      NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
      float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
      nma.Move(dir.normalized * moveDist);

      Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.magenta);
      if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
      {
        if (Input.GetMouseButton(0) == false)
          _state = PlayerState.Idle;
        return;
      }

      // transform.position += dir.normalized * moveDist;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
    }

    // 애니메이션
    Animator anim = GetComponent<Animator>();
    // 현재 게임 상태에 대한 정보를 넘겨준다
    anim.SetFloat("speed", _stat.MoveSpeed);
  }
  void UpdateIdle()
  {
    Animator anim = GetComponent<Animator>();
    anim.SetFloat("speed", 0);
  }

  void Update()
  {
    switch (_state)
    {
      case PlayerState.Die:
        UpdateDie();
        break;
      case PlayerState.Moving:
        UpdateMoving();
        break;
      case PlayerState.Idle:
        UpdateIdle();
        break;
    }
    UpdateMouseCursor();
  }

  void UpdateMouseCursor()
  {
    if (Input.GetMouseButton(0))
      return;

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100.0f, _mask))
    {
      if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
      {
        if (_cursorType != CursorType.Attack)
        {
          Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
          _cursorType = CursorType.Attack;
        }
      }
      else
      {
        if (_cursorType != CursorType.Hand)
        {
          Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
          _cursorType = CursorType.Hand;
        }
      }
    }
  }

  // ground와 monster 레이어에 대해서만 raycasting
  int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
  GameObject _lockTarget;
  void OnMouseEvent(Define.MouseEvent evt)
  {
    // 플레이어가 죽은 상태라면 retrun;
    if (_state == PlayerState.Die) return;

    // Raycast를 받는 부분.
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
    // Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);


    #region 리팩토링 전
    // switch (evt)
    // {
    //   case Define.MouseEvent.PointerDown:
    //     {
    //       if (raycastHit)
    //       {
    //         _destPos = hit.point;
    //         _state = PlayerState.Moving;

    //         if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
    //           _lockTarget = hit.collider.gameObject;
    //         else
    //           _lockTarget = null;
    //       }
    //     }
    //     break;
    //   case Define.MouseEvent.Press:
    //     {
    //       if (_lockTarget != null)
    //         _destPos = _lockTarget.transform.position;
    //       else if (raycastHit)
    //         _destPos = hit.point;
    //     }
    //     break;
    //   case Define.MouseEvent.PointerUp:
    //     _lockTarget = null;
    //     break;
    // }
    #endregion

    if (raycastHit)
    {
      bool isHitMonster = hit.collider.gameObject.layer == (int)Define.Layer.Monster ? true : false;
      _state = PlayerState.Moving;
      _lockTarget = evt switch
      {
        Define.MouseEvent.PointerDown => isHitMonster ? hit.collider.gameObject : null,
        Define.MouseEvent.Press => _lockTarget,
        Define.MouseEvent.PointerUp => _lockTarget,
        _ => null,
      };
    }
    _destPos = _lockTarget == null ? hit.point : _lockTarget.transform.position;
  }

}
