using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  float _speed = 10.0f;
  void Start()
  {

  }

  //GameObject (Player)
  //Transform
  //PlayerController (*)
  void Update()
  {
    // Local -> World
    // TransformDirection

    // World -> Local
    // InverseTransformDirection

    //tranform.Translate 플레이어가 바라보는 방향으로 계산해서 이동 

    if (Input.GetKey(KeyCode.W))
      transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    if (Input.GetKey(KeyCode.S))
      transform.Translate(Vector3.back * Time.deltaTime * _speed);
    if (Input.GetKey(KeyCode.A))
      transform.Translate(Vector3.left * Time.deltaTime * _speed);
    if (Input.GetKey(KeyCode.D))
      transform.Translate(Vector3.right * Time.deltaTime * _speed);
  }
}
