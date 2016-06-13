using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


  private static GameManager _instance;
  public static GameManager getInstance()
  {
      return _instance;
  }

  public Camera _camera;
  public float initialHeight = 0.5f;
  public float separationHeight = 3f;

  private Vector3 bottomLeft;
  private Vector3 topRight;
  private Vector3 sizeGround;
  private Vector3 sizeDead;
  private Vector3 initialPosition;

  private int actualLevel = 0;
  private float lastPositionY;

  public int totalPlayers = 30;
  public List<GameObject> m_players;

  // Use this for initialization
  void Start () {
	  if(_instance != null)
    {
        Destroy(this.gameObject);
        return;
    }
    _instance = this;
    reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  private void reset()
  {
    calculateInitialInformation();
    initialBuild();
    setInitialCameraPosition();
    instantiatePlayers();
    actualLevel = 0;
  }
  private void calculateInitialInformation()
  {
    bottomLeft = _camera.ScreenToWorldPoint(Vector3.zero);
    topRight = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    sizeGround = Pool.getInstance().Ground.GetComponent<Renderer>().bounds.size;
    sizeDead = Pool.getInstance().Dead.GetComponent<Renderer>().bounds.size;
    initialPosition = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height * initialHeight, 0));
  }

  #region BUILD
  private void initialBuild()
  {
    buildLateralDead();
    buildInitialStart();
    buildSaveBox();
    buildFirstLevel();
    builSecondLevel();
  }

  private void buildLateralDead()
  {
    int totalIterations = (int)separationHeight * 2;

    //left part
    float actualY = initialPosition.y - sizeDead.y;
    float actualX = bottomLeft.x + sizeDead.x / 2f;
    for(int i = 0; i < totalIterations; i++)
    {
      GameObject go = Pool.getGameObject(Pool.Type.DEAD);
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualY -= sizeDead.y;
      go.transform.Rotate(0, 180, 0);
    }

    //right part
    actualY = initialPosition.y - sizeDead.y - separationHeight * sizeDead.y;
    actualX = topRight.x - sizeDead.x / 2f;
    for (int i = 0; i < totalIterations - separationHeight; i++)
    {
      GameObject go = Pool.getGameObject(Pool.Type.DEAD);
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualY -= sizeDead.y;
    }

  }
  private void buildSaveBox()
  {
    buildSaveBoxTop();
    buildSaveBoxLateral();
  }
  private void buildSaveBoxTop()
  {
    Vector3 realInitialPosition = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

    float actualX = bottomLeft.x + sizeGround.x / 2;
    float actualY = realInitialPosition.y - sizeGround.y / 2;

    int totalIterations = Mathf.RoundToInt(((topRight.x) - (bottomLeft.x)) / sizeGround.x);
    ++totalIterations;
    for (int i = 0; i < totalIterations; i++)
    {
      GameObject go = Pool.getGameObject(Pool.Type.GROUND);
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualX += sizeGround.x;
    }
  }
  private void buildSaveBoxLateral()
  {
    int totalIterations = Mathf.RoundToInt((topRight.y - initialPosition.y) / sizeGround.y);
    ++totalIterations;
    //left
    float actualX = bottomLeft.x + sizeDead.x/2;
    float actualY = initialPosition.y;
    for(int i = 0; i < totalIterations; i++)
    {
      GameObject go = Pool.getGameObject(Pool.Type.GROUND);
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualY += sizeGround.y;
    }

    //right
    actualX = topRight.x - sizeDead.x / 2;
    actualY = initialPosition.y - separationHeight * sizeGround.y;
    for (int i = 0; i < totalIterations + separationHeight; i++)
    {
      GameObject go = Pool.getGameObject(Pool.Type.GROUND);
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualY += sizeGround.y;
    }

  }
  private void buildInitialStart()
  {
    float actualX = bottomLeft.x + sizeDead.x + sizeGround.x / 2;
    float actualY = initialPosition.y;

    int totalIterations = Mathf.RoundToInt(((topRight.x - sizeGround.x) - (bottomLeft.x + sizeGround.x)) / sizeGround.x);
    totalIterations -= 4;

    for (int i = 0; i < totalIterations; i++)
    {
      GameObject go = Pool.getGameObject(Pool.Type.GROUND);
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualX += sizeGround.x;
    }
    lastPositionY = actualY;
  }
  private void buildFirstLevel()
  {
    float actualX = topRight.x - sizeDead.x - sizeGround.x / 2;
    float actualY = initialPosition.y - (sizeGround.y * separationHeight);

    int totalIterations = Mathf.RoundToInt(((topRight.x - sizeGround.x) - (bottomLeft.x + sizeGround.x)) / sizeGround.x);
    totalIterations -= 4;

    for (int i = 0; i < totalIterations; i++)
    {
      GameObject go = Pool.getGameObject(Pool.Type.GROUND);
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualX -= sizeGround.x;
    }
    lastPositionY = actualY;
  }
  private void builSecondLevel()
  {
    float actualX = bottomLeft.x + sizeDead.x + sizeGround.x / 2;
    float actualY = lastPositionY - (sizeGround.y * separationHeight);

    int totalIterations = Mathf.RoundToInt(((topRight.x - sizeGround.x) - (bottomLeft.x + sizeGround.x)) / sizeGround.x);
    totalIterations -= 4;

    for (int i = 0; i < totalIterations; i++)
    {
      GameObject go = Pool.getGameObject(Pool.Type.GROUND);
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualX += sizeGround.x;
    }
    lastPositionY = actualY;
  }

  private void increaseLevel()
  {

  }

  #endregion
  #region CAMERA
  private void setInitialCameraPosition()
  {

  }
  private void updateCamera()
  {

  }
  #endregion
  #region PLAYERS
  private void instantiatePlayers()
  {
    StartCoroutine(instantiatePlayersWithTime(0.2f));

  }

  private IEnumerator instantiatePlayersWithTime(float time)
  {
    float posY = initialPosition.y + separationHeight / 2;
    float posX = -sizeGround.x * 3;
    Vector2 margin = new Vector2(sizeGround.x * 2, sizeGround.y);
    m_players = new List<GameObject>();

    for (int i = 0; i < totalPlayers; i++)
    {
      GameObject go = Pool.getGameObject(Pool.Type.PLAYER);
      Vector2 newPos = new Vector2(posX + Random.Range(0, margin.y), posY + Random.Range(0, margin.y));
      go.transform.position = new Vector3(newPos.x, newPos.y, 0);
      m_players.Add(go);
      yield return new WaitForSeconds(time);
    }
  }

  #endregion

}
