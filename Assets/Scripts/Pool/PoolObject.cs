using UnityEngine;
using System.Collections;

public class PoolObject : MonoBehaviour {

  [HideInInspector]
  public Camera m_camera;
  private bool _visible;

	
  public bool checkVisible()
  {
    Vector3 bottomLeft = m_camera.ScreenToWorldPoint(new Vector3(0,0,0));
    Vector3 topRight = m_camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    if(topRight.x < this.transform.position.x)
    {
      _visible = false;
      return _visible;
    }

    if (topRight.y < this.transform.position.y)
    {
      _visible = false;
      return _visible;
    }

    if (bottomLeft.x > this.transform.position.x)
    {
      _visible = false;
      return _visible;
    }
    _visible = true;
    return _visible;
  }

}
