using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public Transform player;
    public float collectDistance;
    public GameManager gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit) && (raycastHit.transform.tag == "Present" & Vector3.Distance(player.position, transform.position) < collectDistance))
            {
                transform.position = new Vector3(transform.position.x, -20, transform.position.z);
                gameManager.CollectPresent();
            }
        }
    }
}
