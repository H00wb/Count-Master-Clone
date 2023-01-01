using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class HumanCreator : MonoBehaviour
{
    enum Operation { Sum, Multiplication };
    [SerializeField] Operation op;
    [SerializeField] float value;
    [SerializeField] TMP_Text text;
    [SerializeField] private Transform enemy;
    private bool moveByTouch;
    private bool attack;
    private Camera camera;
    HumanCreatorController humanCreatorController;
    float count;
    private void Start()
    {
        humanCreatorController = transform.parent.GetComponent<HumanCreatorController>();
        if (op == Operation.Sum)
        {
            text.text = "+";
        }
        else if (op == Operation.Multiplication)
        {
            text.text = "x";
        }
        text.text += value.ToString();
    }

void Update()
    {
        if(attack)
        {
var enemyDirection = new Vector3(enemy.position.x,transform.position.y,enemy.position.z) - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * 3f);

            if (enemy.GetChild(1).childCount > 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var Distance = enemy.GetChild(1).GetChild(0).position - transform.GetChild(i).position;
            
                if(Distance.magnitude < 1.5f)
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                            new Vector3(enemy.GetChild(1).GetChild(0).position.x, transform.GetChild(i).position.y, enemy.GetChild(1).GetChild(0).position.z), Time.deltaTime * 3f);
                    }
                }
            
            }

        }
        else
        {

        }
    }

   
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("enemy") && other.transform.parent.childCount > 0)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        switch(other.tag)
        {
            case "enemy":
                if(other.transform.parent.childCount > 0)
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                break;
            case "jump":
                transform.DOJump(transform.position, 1f, 1, 1f).SetEase(Ease.Flash);
                    break;
        }
        float randomPositionRange = 0.5f;
        if (!humanCreatorController.GetIsTaken())
        {
            Transform parent = other.transform.parent;
            if (op == Operation.Sum)
            {
                count = value;
            }
            else if (op == Operation.Multiplication)
            {
                count = parent.GetComponent<TeamLeader>().humanCount * value;
            }
            for (int i = 0; i < count; i++)
            {
                Vector3 position = new Vector3(Random.Range(parent.position.x - randomPositionRange, parent.position.x + randomPositionRange),
                                                parent.position.y,
                                                Random.Range(parent.position.z - randomPositionRange, parent.position.z + randomPositionRange));

                GameObject human = ObjectPooler.SharedInstance.GetPooledObject(Constants.HUMAN_TAG);
                human.transform.position = position;
                human.transform.SetParent(parent);
                human.GetComponent<TeamMember>().JoinTeam();
                human.SetActive(true);
            }
            humanCreatorController.Take();
        }
        if (other.CompareTag("enemy"))
        {
            enemy = other.transform;
            attack = true;
        }
    }
}
