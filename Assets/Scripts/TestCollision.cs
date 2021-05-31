using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  }

  void Update()
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
