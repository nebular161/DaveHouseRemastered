
using UnityEngine;
public class ItemAnim : MonoBehaviour
{
    Transform item;
    void Start()
    {
        item = GetComponent<Transform>();
    }
    void Update()
    {
        item.localPosition = new Vector3(0, Mathf.Sin(Time.frameCount * 0.0174532924f) / 2 + 1, 0);
    }
}
