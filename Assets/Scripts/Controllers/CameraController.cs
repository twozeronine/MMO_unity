using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField]
  Define.CameraMode _mode = Define.CameraMode.QuaterView;

  [SerializeField]
  Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

  [SerializeField]
  GameObject _player = null;
  void Start()
  {

  }

  void LateUpdate()
  {
    if (_mode == Define.CameraMode.QuaterView)
    {
      RaycastHit hit;
      //플레이어 위치에서 카메라 위치로 카메라 벡터의 크기 만큼의 Ray를 쏨
      if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
      {
        float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
        // 플레이어와 벽 사이의 거리에서 0.8정도 곱한 거리에 카메라를 위치시킴.
        transform.position = _player.transform.position + _delta.normalized * dist;
      }
      else
      {
        transform.position = _player.transform.position + _delta;
        transform.LookAt(_player.transform);

      }
    }
  }

  public void SetQuaterView(Vector3 delta)
  {
    _mode = Define.CameraMode.QuaterView;
    _delta = delta;
  }
}
