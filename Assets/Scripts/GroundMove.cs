using UnityEngine;
using System.Collections;

public class GroundMove : MonoBehaviour {

    public float GroundSpeed;

    void Update()
    {
        transform.Translate(Vector2.up * GroundSpeed * Time.deltaTime);

        if (this.transform.position.y > 7.5f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnEnable()
    {
        this.transform.position = new Vector3(Random.Range(-2.85f, 2.85f), -8, 0);
    }
}
