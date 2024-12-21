using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecond : MonoBehaviour
{
    public int maxHealth = 1000;

    [SerializeField] GameObject monoSpawner;

    [SerializeField] GameObject keyPrefab;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] float flashCd = 1f;
    Sprite currentSprite;
    SpriteRenderer sr;
    public float ballsCd;
    public int currentHealth;
    [SerializeField] float ballSpeed = 5;
    bool isAtk1Over = true;
    bool isAtk2Over = true;
    bool isAtk3Over = true;

    bool canAttack = true;

    [SerializeField] GameObject rubberBall;

    public enum BossState { State1, State2, State3 }
    private BossState currentState;

    public float cooldown = 3.0f; // Initial cooldown between attacks
    private float attackTimer = 0.0f;

    void Start()
    {
        
        sr = GetComponentInChildren<SpriteRenderer>();
        currentSprite = sr.sprite;
        currentHealth = maxHealth;
        DetermineState();
        Attack1();
    }


    private void Attack1()
    {
        StartCoroutine(Atk1());
    }

    private void Attack2()
    {
        monoSpawner.SetActive(true);
    }

    private void Attack3()
    {
        GetComponentInChildren<CircularBomb>().StartExplosion();
        Debug.Log("Boss performs Attack 3!");
    }

    public void TakeDamage(int damage)
    {

        StartCoroutine(Flash(flashCd));
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        DetermineState();
    }

    private void DetermineState()
    {
        float healthPercentage = (float)currentHealth / maxHealth;

        if (healthPercentage == 0.66f)
        {
            currentState = BossState.State1;
        }
        else if (healthPercentage > 0.33f)
        {
            Attack2();
        }
        else
        {
            Attack3();
        }

        Debug.Log($"State changed to {currentState}, Cooldown: {cooldown}");
    }

    public void Atk2()
    {

    }

    IEnumerator Atk1()
    {
        GameObject ball1 = Instantiate(rubberBall, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Rigidbody2D rb1 = ball1.GetComponent<Rigidbody2D>();

        ball1.transform.rotation = Quaternion.Euler(0, 0, -45);

        rb1.AddForce(new Vector2(-1, 1).normalized * ballSpeed, ForceMode2D.Impulse);

        GameObject ball2 = Instantiate(rubberBall, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
        Rigidbody2D rb2 = ball2.GetComponent<Rigidbody2D>();

        ball2.transform.rotation = Quaternion.Euler(0, 0, 45);


        rb2.AddForce(new Vector2(-1, -1).normalized * ballSpeed, ForceMode2D.Impulse);


        yield return new WaitForSeconds(ballsCd);


        StartCoroutine(Atk1());

    }

    IEnumerator Flash(float invulnerabilityCd)
    {
        canAttack = false;
        sr.color = Color.white;
        yield return new WaitForSeconds(invulnerabilityCd);
        sr.color = GetHealthColor(currentHealth, maxHealth);
        canAttack = true;
    }

    public void ManageCanAttack(bool b)
    {
        canAttack = b;
       

    }

    public void Die()
    {

        GameObject key = Instantiate(keyPrefab, transform.position, Quaternion.identity);

        key.transform.parent = null;




        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RubberBall"))
        {
            if (collision.gameObject.GetComponent<RubberBall>().active)
            {
                Destroy(collision.gameObject);
                TakeDamage(1);

            }
        }
    }

    Color GetHealthColor(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth; 
        return Color.Lerp(Color.red, Color.yellow, healthPercentage);
    }

}
