using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {
    public float StartSpeed;

	void Update () {
        this.transform.Translate(Vector2.up * StartSpeed * Time.deltaTime);

        if (this.transform.position.y > 7.5f)
        {
            Destroy(this.gameObject);
        }
    }
    void OnEnable()
    {
        this.transform.position = new Vector3(0, -3, 0);
    }
}
