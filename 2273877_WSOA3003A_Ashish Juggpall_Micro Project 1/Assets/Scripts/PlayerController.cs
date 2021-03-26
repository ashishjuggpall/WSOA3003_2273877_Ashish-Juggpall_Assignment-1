using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int PlayerHealth = 15;
    public int PlayerHealthItem = 3;
    public Text PlayerHealthDisplay;
    public Text PlayerHealthItemDisplay;

    public int EnemyHealth = 30;
    public int EnemyHealthItem = 1;
    public Text EnemyHealthDisplay;

    public float EnemyTimeRemaining = 3;
    public bool EnemyTimeRunning = false;

    private bool IsPlayersTurn = true;
    private bool IsEnemyTurn = false;

    private bool IsPlayerBlockActive = false;
    private bool IsEnemyBlockActive = false;

    private bool BattleWon = false;
    private bool BattleLost = false;

    public GameObject Player;
    public GameObject Enemy;

    public GameObject PlayerBattleUI;
    public GameObject PlayerHealthUI;
    public GameObject EnemyHealthUI;
    public GameObject BattleWonUI;
    public GameObject BattleLostUI;

    private EnemyShaker enemyShaker;
    private PlayerShake playershake;
    private void Start()
    {

        enemyShaker = GameObject.FindGameObjectWithTag("EnemyShake").GetComponent<EnemyShaker>();
        playershake = GameObject.FindGameObjectWithTag("PlayerShake").GetComponent<PlayerShake>();

        Player.SetActive(true);
        Enemy.SetActive(true);

        IsPlayersTurn = false;
        IsEnemyTurn = true;

        BattleWon = false;
        BattleLost = false;

        PlayerHealthUI.SetActive(true);
        EnemyHealthUI.SetActive(true);

        BattleWonUI.SetActive(false);
        BattleWonUI.SetActive(false);

    }

    private void Update()
    {
        PlayerHealthDisplay.text = PlayerHealth.ToString();
        PlayerHealthItemDisplay.text = PlayerHealthItem.ToString();

        EnemyHealthDisplay.text = EnemyHealth.ToString();

        if (PlayerHealth > 0 && EnemyHealth > 0)
        {
            if (IsEnemyTurn == true)
            {
                EnemyTimeRunning = true;
                PlayerBattleUI.SetActive(false);
                if (EnemyHealth <= 10)
                {
                    if (EnemyHealthItem > 0)
                    {
                        if (EnemyTimeRunning)
                        {
                            if (EnemyTimeRemaining > 0)
                            {
                                EnemyTimeRemaining -= Time.deltaTime;
                            }

                            else
                            {
                                EnemyTimeRemaining = 3;
                                EnemyTimeRunning = false;
                                EnemyHeal();
                            }
                        }

                    }

                    else
                    {
                        if (IsPlayerBlockActive == true)
                        {
                            if (EnemyTimeRunning)
                            {
                                if (EnemyTimeRemaining > 0)
                                {
                                    EnemyTimeRemaining -= Time.deltaTime;
                                }

                                else
                                {
                                    EnemyTimeRemaining = 3;
                                    EnemyTimeRunning = false;
                                    IsPlayerBlockActive = false;
                                    IsEnemyTurn = false;
                                    PlayerBattleUI.SetActive(true);
                                    IsPlayersTurn = true;
                                }

                            }
                        }

                        else
                        {
                            if (EnemyTimeRunning)
                            {
                                if (EnemyTimeRemaining > 0)
                                {
                                    EnemyTimeRemaining -= Time.deltaTime;
                                }

                                else
                                {
                                    EnemyTimeRemaining = 3;
                                    EnemyTimeRunning = false;
                                    EnemyAttack();
                                }
                            }

                        }

                    }
                }

                else
                {
                    if (EnemyTimeRunning)
                    {
                        if (EnemyTimeRemaining > 0)
                        {
                            EnemyTimeRemaining -= Time.deltaTime;
                        }

                        else
                        {
                            EnemyTimeRemaining = 3;
                            EnemyTimeRunning = false;
                            EnemyAttack();
                        }
                    }
                }

            }

            if (IsPlayersTurn == true)
            {
                PlayerBattleUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.X))
                {
                    PlayerAttack();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    PlayerHeal();
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    IsPlayerBlockActive = true;
                    IsPlayersTurn = false;
                    PlayerBattleUI.SetActive(false);
                    IsEnemyTurn = true;
                }
            }
        }

        else
        {
            if (PlayerHealth <= 0)
            {
                WinGame();
            }

            if (EnemyHealth <=0)
            {
                LoseGame();
            }
        }
    }

    public void PlayerAttack()
    {
        EnemyHealth-=2;
        IsPlayersTurn = false;
        PlayerBattleUI.SetActive(false);
        IsEnemyTurn = true;
        enemyShaker.EnemyShake();
    }

    public void EnemyAttack()
    {
        PlayerHealth--;
        IsEnemyTurn = false;
        PlayerBattleUI.SetActive(true);
        IsPlayersTurn = true;
        playershake.PlayerShaker();
    }

    public void PlayerHeal()
    {
        PlayerHealth+=10;
        PlayerHealthItem--;
        IsPlayersTurn = false;
        PlayerBattleUI.SetActive(false);
        IsEnemyTurn = true;
    }
    public void EnemyHeal()
    {
        EnemyHealth+=5;
        EnemyHealthItem--;
        IsEnemyTurn = false;
        PlayerBattleUI.SetActive(true);
        IsPlayersTurn = true;
    }

    public void WinGame()
    {
        BattleWon = true;
        PlayerHealthUI.SetActive(false);
        EnemyHealthUI.SetActive(false);
        IsPlayersTurn = false;
        IsEnemyTurn = false;
        Enemy.SetActive(false);
    }

    public void LoseGame()
    {
        BattleLost = true;
        PlayerHealthUI.SetActive(false);
        EnemyHealthUI.SetActive(false);
        IsPlayersTurn = false;
        IsEnemyTurn = false;
        Enemy.SetActive(false);

    }
   /// public void RandomGenerator()
   //RandomNumber = Random.Range(1, 4);

}
