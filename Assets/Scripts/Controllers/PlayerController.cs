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

  bool _moveToDest = false;
  Vector3 _destPos;
  void Start()
  {

    // 실수로 다른 곳에서 OnKeyboard를 이미 등록했다면 두번 등록이 되기 때문에 그것을 방지하기 위하여 한번 빼고 시작하는것이다.
    Managers.Input.KeyAction -= OnKeyboard;
    Managers.Input.KeyAction += OnKeyboard;
    Managers.Input.MouseAction -= OnMouseClicked;
    Managers.Input.MouseAction += OnMouseClicked;
  }

  void Update()
  {
    if (_moveToDest)
    {
      Vector3 dir = _destPos - transform.position;
      // 도착
      if (dir.magnitude < 0.0001f)
      {
        _moveToDest = false;
      }
      else
      {
        float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
        transform.position += dir.normalized * moveDist;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
      }
    }
  }

  void OnKeyboard()
  {
    if (Input.GetKey(KeyCode.W))
    {
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
      transform.position += Vector3.forward * Time.deltaTime * _speed;
    }
    if (Input.GetKey(KeyCode.S))
    {

      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
      transform.position += Vector3.back * Time.deltaTime * _speed;
    }
    if (Input.GetKey(KeyCode.A))
    {

      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
      transform.position += Vector3.left * Time.deltaTime * _speed;
    }
    if (Input.GetKey(KeyCode.D))
    {
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
      transform.position += Vector3.right * Time.deltaTime * _speed;
    }

    _moveToDest = false;
  }

  void OnMouseClicked(Define.MouseEvent evt)
  {
    if (evt != Define.MouseEvent.Click)
      return;

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

    LayerMask mask = LayerMask.GetMask("Wall");

    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100.0f, mask))
    {
      _destPos = hit.point;
      _moveToDest = true;
      //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
    }
  }

}
