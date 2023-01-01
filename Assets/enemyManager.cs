using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class enemyManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro Countertext;
    [SerializeField] private GameObject Actor;

    [Range(0f, 1f)][SerializeField] private float DistanceFactor, Radius;


    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < Random.Range(1, 5); i++)
        {
            Instantiate(Actor, transform.position, new Quaternion(0f, 180f, 0f, 1f), transform);
        }

        Countertext.text = (transform.childCount).ToString();

        FormatActor();

    }



    // Update is called once per frame
    void Update()
    {

    }



    private void FormatActor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

            var NewPos = new Vector3(x, -0.55f, z);
            transform.transform.GetChild(i).localPosition = NewPos;
        }
    }
}
