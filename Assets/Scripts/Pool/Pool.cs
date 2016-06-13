using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour {

  public enum Type { PLAYER, GROUND, DEAD};
  public int initialSize = 30;

  public GameObject Player;
  public GameObject Ground;
  public GameObject Dead;

  private List<GameObject> players;
  private List<GameObject> grounds;
  private List<GameObject> deads;

  private static Pool _instance;

  private GameObject playersParent;
  private GameObject groundsParent;
  private GameObject deadsParent;

  public Camera m_camera;

  public static Pool getInstance()
  {
    return _instance;
  }

  void Awake()
  {
    if(_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
      return;
    }
    _instance = this;

    playersParent = new GameObject("PlayersParent");
    groundsParent = new GameObject("GroundParent");
    deadsParent = new GameObject("DeadsParent");

    players = new List<GameObject>();
    grounds = new List<GameObject>();
    deads = new List<GameObject>();

    for(int i = 0; i < initialSize; i++)
    {

      addPlayer();
      addGround();
      addDead();
    }

    Debug.Log("pool filled");
  }

  public static GameObject getGameObject(Type t)
  {
    GameObject go = null;
    switch (t) 
    {
      case Type.DEAD: go = _instance.getElementInList(_instance.deads, t); break;
      case Type.GROUND: go = _instance.getElementInList(_instance.grounds, t); break;
      case Type.PLAYER: go = _instance.getElementInList(_instance.players, t); break;
    }
    go.GetComponent<Renderer>().enabled = true;
    go.GetComponent<Collider>().enabled = true;
    return go;
  }

  private GameObject getElementInList(List<GameObject> list, Type t)
  {
    for(int i = 0; i < list.Count; i++)
    {
      PoolObject po = list[i].GetComponent<PoolObject>();
      if(!po.checkVisible())
      {
        return list[i];
      }
    }
    switch(t)
    {
      case Type.DEAD: return addDead();
      case Type.GROUND: return addGround();
      case Type.PLAYER: return addPlayer();
    }

    return list[list.Count - 1];
  }

	private GameObject addGameObjectToList(List<GameObject> list, GameObject go)
  {
    go = configureGameObject(go, false);
    list.Add(go);
    return go;
  }

  private GameObject configureGameObject(GameObject go, bool enabled)
  {
    go.GetComponent<Renderer>().enabled = enabled;
    go.GetComponent<Collider>().enabled = enabled;
    go.transform.position = new Vector3(1000, 1000, 1000);
    return go;
  }

  private GameObject addPlayer()
  {
    GameObject go = Instantiate(Player);
    go = addGameObjectToList(players, go);
    go.transform.parent = playersParent.transform;
    go.GetComponent<PoolObject>().m_camera = m_camera;
    return go;
  }

  private GameObject addGround()
  {
    GameObject go = Instantiate(Ground);
    go = addGameObjectToList(grounds, go);
    go.transform.parent = groundsParent.transform;
    go.GetComponent<PoolObject>().m_camera = m_camera;
    return go;
  }

  private GameObject addDead()
  {
    GameObject go = Instantiate(Dead);
    go = addGameObjectToList(deads, go);
    go.transform.parent = deadsParent.transform;
    go.GetComponent<PoolObject>().m_camera = m_camera;
    return go;
  }
}
