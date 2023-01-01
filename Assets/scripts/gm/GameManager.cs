using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject MainActor;
    MoveToDirection MainActorMoveToDirection;
    Movement MainActorMovement;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject MenuWin;
    [SerializeField] GameObject MenuLose;
    [SerializeField] private Transform road;
    [SerializeField] private Transform enemy;
    private bool attack;

    private void Start()
    {
        MainActorMoveToDirection = MainActor.GetComponent<MoveToDirection>();
        MainActorMovement = MainActor.GetComponent<Movement>();
    }
    public void StartGame()
    {
        MainActorMoveToDirection.StartMoveToDirection();
        CloseAllMenus();
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Win()
    {
        CloseAllMenus();
        MainActorMoveToDirection.StopMoveToDirection();
        MainActorMovement.StopMovement();
        MenuWin.SetActive(true);
    }
    public void Lose()
    {
        CloseAllMenus();
        MainActorMoveToDirection.StopMoveToDirection();
        MainActorMovement.StopMovement();
        MenuLose.SetActive(true);
    }
    void CloseAllMenus()
    {
        foreach (Transform child in Menu.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
