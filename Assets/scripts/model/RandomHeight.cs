using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeight : MonoBehaviour
{
    [SerializeField] float maximum, minimum;
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.transform.position = new Vector3(child.position.x, Random.Range(minimum, maximum), child.position.z);
        }
    }
}
