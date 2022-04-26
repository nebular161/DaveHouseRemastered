using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderPoints : MonoBehaviour
{
    public static WanderPoints Instance;
    Transform[] wanderPoints;

    public GameObject coin;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        wanderPoints = GetComponentsInChildren<Transform>();

        int chance;
        for (int i = 0; i < wanderPoints.Length; i++)
        {
            chance = Random.Range(0, 4);
            if(chance == 1)
            {
                Vector3 newPos = wanderPoints[i].position;
                newPos.y = 5;
                Instantiate(ItemManager.Instance.itemDrop[Random.Range(0, ItemManager.Instance.itemDrop.Length - 1)], newPos, coin.transform.rotation, wanderPoints[i]);
            }
        }
    }
    public Transform GetWanderPoint()
    {
        return wanderPoints[Random.Range(0, wanderPoints.Length)];
    }
}
