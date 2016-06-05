﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


  private static GameManager _instance;
  public static GameManager getInstance()
  {
      return _instance;
  }

  private int actualHeight = 0;

  public Camera _camera;
  public float initialHeight = 0.5f;

  [Header("GameObjects that will be instantiated")]
  public GameObject _Player;
  public GameObject _Dead;
  public GameObject _Ground;

  private Vector3 bottomLeft;
  private Vector3 topRight;
  private Vector3 sizeGround;
  private Vector3 sizeDead;
  private Vector3 initialPosition;

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
    actualHeight = 0;
  }
  private void calculateInitialInformation()
  {
    bottomLeft = _camera.ScreenToWorldPoint(Vector3.zero);
    topRight = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    sizeGround = _Ground.GetComponent<Renderer>().bounds.size;
    sizeDead = _Dead.GetComponent<Renderer>().bounds.size;
    initialPosition = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height * initialHeight, 0));
  }

  #region BUILD
  private void initialBuild()
  {
    buildLateralDead();
    buildInitialStart();
    buildSaveBox();
  }

  private void buildLateralDead()
  {
    int totalIterations = Mathf.RoundToInt((initialPosition.y - bottomLeft.y) / sizeDead.y) + 4;// extra 4 just to allways have more

    //left part
    float actualY = initialPosition.y - sizeDead.y * 1.5f;
    float actualX = bottomLeft.x + sizeDead.x / 2f;
    for(int i = 0; i < totalIterations; i++)
    {
      GameObject go = Instantiate(_Dead) as GameObject;
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualY -= sizeDead.y;
      go.transform.Rotate(0, 180, 0);
    }

    //right part
    actualY = initialPosition.y - sizeDead.y * 1.5f;
    actualX = topRight.x - sizeDead.x / 2f;
    for (int i = 0; i < totalIterations; i++)
    {
      GameObject go = Instantiate(_Dead) as GameObject;
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualY -= sizeDead.y;
    }

  }

  private void buildInitialStart()
  {

    float actualX = bottomLeft.x + sizeDead.x + sizeGround.x/2;
    float actualY = initialPosition.y;

    int totalIterations = Mathf.RoundToInt(((topRight.x - sizeGround.x) - (bottomLeft.x + sizeGround.x))/sizeGround.x);
    totalIterations -= 4;

    for(int i = 0; i < totalIterations; i++)
    {
      GameObject go = Instantiate(_Ground) as GameObject;
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualX += sizeGround.x;
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
      GameObject go = Instantiate(_Ground) as GameObject;
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualX += sizeGround.x;
    }
  }
  private void buildSaveBoxLateral()
  {
    int totalIterations = Mathf.RoundToInt((topRight.x - initialPosition.x) / sizeGround.x);
    ++totalIterations;
    //left
    float actualX = bottomLeft.x + sizeDead.x/2;
    float actualY = initialPosition.y;
    for(int i = 0; i < totalIterations; i++)
    {
      GameObject go = Instantiate(_Ground) as GameObject;
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualY += sizeGround.y;
    }

    //right
    actualX = topRight.x - sizeDead.x / 2;
    actualY = initialPosition.y;
    for (int i = 0; i < totalIterations; i++)
    {
      GameObject go = Instantiate(_Ground) as GameObject;
      go.transform.position = new Vector3(actualX, actualY, 0);
      actualY += sizeGround.y;
    }

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

  }
  #endregion

}
