using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
  PlayerStat _stat;
  Vector3 _destPos;



  void Start()
  {
    _stat = gameObject.GetComponent<PlayerStat>();

    // 실수로 다른 곳에서 Action을 이미 등록했다면 두번 등록이 되기 때문에 그것을 방지하기 위하여 한번 빼고 시작하는것이다.
    Managers.Input.MouseAction -= OnMouseClicked;
    Managers.Input.MouseAction += OnMouseClicked;
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
  }

  int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
  void OnMouseClicked(Define.MouseEvent evt)
  {
    if (_state == PlayerState.Die) return;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    // Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);



    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100.0f, _mask))
    {
      _destPos = hit.point;
      _state = PlayerState.Moving;
      //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");

      if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
      {
        Debug.Log("MonsterClick");
      }
      else
      {
        Debug.Log("GroundClick");
      }
    }
  }

}
