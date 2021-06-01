using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 위치 벡터
// 2. 방향 벡터
struct MyVector
{
  public float x;
  public float y;
  public float z;

  //          +
  //     +    +
  // +--------+
  public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
  public MyVector normailzed { get { return new MyVector(x / magnitude, y / magnitude, z / magnitude); } }

  public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }

  public static MyVector operator +(MyVector a, MyVector b)
  {
    return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
  }
  public static MyVector operator -(MyVector a, MyVector b)
  {
    return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
  }

  public static MyVector operator *(MyVector a, float d)
  {
    return new MyVector(a.x * d, a.y * d, a.z * d);
  }
}
public class PlayerController : MonoBehaviour
{
  [SerializeField]
  float _speed = 10.0f;
  Vector3 _destPos;



  void Start()
  {

    // 실수로 다른 곳에서 Action을 이미 등록했다면 두번 등록이 되기 때문에 그것을 방지하기 위하여 한번 빼고 시작하는것이다.
    Managers.Input.MouseAction -= OnMouseClicked;
    Managers.Input.MouseAction += OnMouseClicked;
  }

  float wait_run_ratio = 0;
  public enum PlayerState
  {
    Die,
    Moving,
    Idle,
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
    if (dir.magnitude < 0.0001f)
    {
      _state = PlayerState.Idle;
    }
    else
    {
      float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
      transform.position += dir.normalized * moveDist;

      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
    }

    // 애니메이션
    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
    Animator anim = GetComponent<Animator>();
    anim.SetFloat("wait_run_ratio", wait_run_ratio);
    anim.Play("WAIT_RUN");
  }
  void UpdateIdle()
  {
    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
    Animator anim = GetComponent<Animator>();
    anim.SetFloat("wait_run_ratio", wait_run_ratio);
    anim.Play("WAIT_RUN");
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

  void OnMouseClicked(Define.MouseEvent evt)
  {
    if (_state == PlayerState.Die) return;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

    LayerMask mask = LayerMask.GetMask("Wall");

    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100.0f, mask))
    {
      _destPos = hit.point;
      _state = PlayerState.Moving;
      //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
    }
  }

}
