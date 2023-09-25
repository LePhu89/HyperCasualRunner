using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float roadWidth;
    private bool canMove;

    [Header("Control")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
    private void Start()
    {
        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }
    void Update()
    {
        if (canMove)
        {
            MoveForward();
            ManageController();
        }        
    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.Game)
        {
            StartMoving();
        }
        else if(gameState == GameManager.GameState.GameOver)
            StopMoving();
        else if(gameState == GameManager.GameState.LevelComplete)
            StopMoving();
    }

    private void StartMoving()
    {
        canMove = true;

        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;

        playerAnimator.Idle();
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }
    private void ManageController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;
            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;
            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(),
                roadWidth / 2 - crowdSystem.GetCrowdRadius());
            transform.position = position;  

            //transform.position = clickedPlayerPosition + Vector3.right * xScreenDifference;
        }
        
    }
}
