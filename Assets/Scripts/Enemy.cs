using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    public float panicRange;
    public GameObject minigame;
    SoundManager soundManager = SoundManager.instance;
    SpriteRenderer rend;
    public FadeControl fade;

    public GameObject text;

    public float currentRange;
    Rigidbody2D m_Rigidbody;

    void Start()
    {
        text.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        m_Rigidbody = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();

        fade = FindObjectOfType<FadeControl>();
        rend.sortingOrder = 4;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRange = Vector2.Distance(transform.position, Player.transform.position);//calcula a distancia entre player e inimigo
        if (currentRange <= panicRange)//ativa barra de panico se a criatura estiver muito perto
        {
            minigame.SetActive(true);
            minigame.GetComponent<PanicMinigame>().pause = false;
        }

        if (currentRange <= 15 && !soundManager.IsPlaying("EnemyWalk"))
        {
            soundManager.PlayOneShot("EnemyWalk");
        }

        




        m_Rigidbody.velocity = new Vector2(speed, 0);
        
        if (m_Rigidbody.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (m_Rigidbody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        
    }
    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, panicRange);//utilidade pra saber quando jogador deve panickar
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bounds"))
        {
            speed = -speed;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
            fade.Fade();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
