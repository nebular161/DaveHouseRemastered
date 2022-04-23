using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderPoints : MonoBehaviour
{
    public static WanderPoints Instance;
    Transform[] wanderPoints;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        wanderPoints = GetComponentsInChildren<Transform>();
    }
    public Transform GetWanderPoint()
    {
        return wanderPoints[Random.Range(0, wanderPoints.Length)];
    }
}
