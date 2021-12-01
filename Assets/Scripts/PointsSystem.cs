using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
  private static PointsSystem _instance;
  public static PointsSystem Instance { get { return _instance; }}

  [SerializeField] Text text;
  private int points;
  [SerializeField] float increasePointCounter; 
  private float increasePointCurrent;

  private void Awake()
  {
    if(_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
    } else {
      _instance = this;
    }
  }

  void Start()
  {
    ResetPoints();
  }

  void Update()
  {
    increasePointCurrent += Time.deltaTime;

    if(increasePointCurrent > increasePointCounter)
    {
      increasePointCurrent = 0;
      IncreasePoints(1);
    }
  }

  void ResetPoints()
  {
    text.text = "0";
    points = 0;
  }

  public void IncreasePoints(int value)
  {
    points += value;
    text.text = points.ToString();
  }

  public int GetPoints()
  {
    return points;
  }
}
