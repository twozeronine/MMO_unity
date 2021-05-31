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
  void Start()
  {

    Managers.Input.KeyAction -= OnKeyboard;
    Managers.Input.KeyAction += OnKeyboard;
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
  }
}
