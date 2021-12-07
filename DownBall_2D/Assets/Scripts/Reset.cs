#pragma warning disable 0618

using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

    public string Name;

    void OnMouseDown()
    {
        Application.LoadLevel(Name);
    }
}