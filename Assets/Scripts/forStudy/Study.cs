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

public class TestCollision : MonoBehaviour
{
  // 1) 나 혹은 상대한테 RigidBody 있어야한다 (IsKinematic : Off)
  // 2) 나한테 Collider가 있어야 한다 (IsTrigger : Off)
  // 3) 상대한테 Collider가 있어야 한다 (IsTrigger : Off)
  void OnCollisionEnter(Collision collision)
  {
    Debug.Log($"Collision ! {collision.gameObject.name}");
  }

  // 1) 둘 다 Collider가 있어야 한다
  // 2) 둘 중 하나는 IsTrigger : On
  // 3) 둘 중 하나는 RigidBody가 있어야 한다
  void OnTriggerEnter(Collider other)
  {
    Debug.Log($"Trigger ! {other.gameObject.name}");
  }

  void Start()
  {
    co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
    StartCoroutine("CoStopExplode", 2.0f);
  }


  Coroutine co;
  IEnumerator CoStopExplode(float seconds)
  {
    Debug.Log("Stop Enter");
    yield return new WaitForSeconds(seconds);
    Debug.Log("Stop Execute!!!");
    if (co != null)
    {
      StopCoroutine(co);
      co = null;
    }
  }

  IEnumerator ExplodeAfterSeconds(float seconds)
  {
    Debug.Log("Explode Enter");
    yield return new WaitForSeconds(seconds);
    Debug.Log("Explode Execute!!");
    co = null;
  }

  void Update()
  {
    // Local <-> World <-> ( Viewport <-> Screen ) ( 화면 )
    // 한 픽셀좌표를 기준
    // Debug.Log(Input.mousePosition); // Screen
    // 픽셀에 상관없이 화면 비율에 대해서 얼마나 차지하는가에 대한 비율
    // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // Viewport

    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


      Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

      // 첫번째 비트를 8번째 까지 왼쪽으로 시프트
      //   int mask = (1 << 8) | (1 << 9);
      LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");

      RaycastHit hit;
      if (Physics.Raycast(ray, out hit, 100.0f, mask))
      {
        Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
      }
    }
    // if (Input.GetMouseButtonDown(0))
    // {
    //   Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    //   Vector3 dir = mousePos - Camera.main.transform.position;
    //   dir = dir.normalized;

    //   Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

    //   RaycastHit hit;
    //   if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
    //   {
    //     Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
    //   }
    // }
  }

  void Raycasting()
  {
    Vector3 look = transform.TransformDirection(Vector3.forward);
    Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

    RaycastHit[] hits;
    hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);

    foreach (RaycastHit hit in hits)
    {
      Debug.Log($"RayCast! {hit.collider.gameObject.name}");
    }
  }
}
