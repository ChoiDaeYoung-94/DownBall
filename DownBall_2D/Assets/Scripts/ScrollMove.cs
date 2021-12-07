using UnityEngine;
using System.Collections;

public class ScrollMove : MonoBehaviour {

    float scrollSpeedDown, scrollSpeedLeft;
    Material BackGound;

    void Start()
    {
        BackGound = GetComponent<Renderer>().material;
    }

    void Update()
    {
        scrollSpeedDown = BackGound.mainTextureOffset.y + -0.003f * Time.deltaTime;
        scrollSpeedLeft = BackGound.mainTextureOffset.x + 0.005f * Time.deltaTime;
        Vector2 newoffset = new Vector2(scrollSpeedLeft, scrollSpeedDown);
        BackGound.mainTextureOffset = newoffset;
    }
}
