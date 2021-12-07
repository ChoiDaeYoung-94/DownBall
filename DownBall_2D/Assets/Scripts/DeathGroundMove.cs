using UnityEngine;
using System.Collections;

public class DeathGroundMove : MonoBehaviour {

    public float DeathGroundSpeed;

    void Update()
    {
        transform.Translate(Vector2.up * DeathGroundSpeed * Time.deltaTime);

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
