using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{
    bool selected = false;

    private Transform buttonTransform;
    private Vector3 normalSize;
    private Vector3 selectedSize;
    private float lerpSizeMulti = 0.09f;
    private float lerpTimeMulti = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        buttonTransform = base.transform;
        normalSize = buttonTransform.localScale;
        selectedSize = new Vector3(buttonTransform.localScale.x + lerpSizeMulti, buttonTransform.localScale.y + lerpSizeMulti, buttonTransform.localScale.z + lerpSizeMulti);
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            buttonTransform.localScale = new Vector3(Mathf.Lerp(selectedSize.x, buttonTransform.localScale.x, lerpTimeMulti), Mathf.Lerp(selectedSize.y, buttonTransform.localScale.y, lerpTimeMulti), Mathf.Lerp(selectedSize.z, buttonTransform.localScale.z, lerpTimeMulti));
        }
        else
        {
            buttonTransform.localScale = new Vector3(Mathf.Lerp(normalSize.x, buttonTransform.localScale.x, lerpTimeMulti), Mathf.Lerp(normalSize.y, buttonTransform.localScale.y, lerpTimeMulti), Mathf.Lerp(normalSize.z, buttonTransform.localScale.z, lerpTimeMulti));
        }
    }

    public void PointerEnter() {selected = true;}
    public void PointerExit() {selected = false;}
}
