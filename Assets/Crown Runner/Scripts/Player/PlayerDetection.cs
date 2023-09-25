using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header("Elenment")]
    [SerializeField] CrowdSystem crowdSystem;

    [Header("Events")]
    public static Action onDoorHit;

    void Update()
    {
        if (GameManager.Instance.IsGameState())
            DectecCollier();
    }

    private void DectecCollier()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, crowdSystem.GetCrowdRadius());
        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                print("bem bem cua");
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.Disable();
                onDoorHit?.Invoke();
                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }

            else if (detectedColliders[i].tag == "Finish")
            {
                print("we are hit the finish line");
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
                //SceneManager.LoadScene(0);
            }

            else if (detectedColliders[i].tag == "Coin")
            {
                Destroy(detectedColliders[i].gameObject);

                DataManager.instance.AddCoins(1);

            }
        }
    }
}
