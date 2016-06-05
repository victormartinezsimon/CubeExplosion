using UnityEngine;
using System.Collections;

public class ExplosionForce : MonoBehaviour {

  public Camera _camera;
  public float explosionForce = 10;
  public float explosionRadius = 2;


    // Update is called once per frame
  void Update () {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 position = Input.mousePosition;
        //    Vector3 world_position = _camera.ScreenToWorldPoint(position);
        //    world_position.z = 0;
        //    GameObject[] cubes =  GameManager.getInstance().cubes;

        //    for(int i = 0; i < cubes.Length; i++)
        //    {
        //        cubes[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, world_position, explosionRadius);
        //        Vector3 rot = cubes[i].transform.rotation.eulerAngles;
        //        rot.y = 0;
        //        cubes[i].transform.rotation = Quaternion.Euler(rot);
        //    }

        //}
	}
}
