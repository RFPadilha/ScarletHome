using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float panicRange;
    public float currentRange;
    
    GameObject Player;
    
    SpriteRenderer rend;
    Rigidbody2D m_Rigidbody;
    SoundManager soundManager;
    

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");//referencia o jogador
        soundManager = SoundManager.instance;
        m_Rigidbody = GetComponent<Rigidbody2D>();//componentes do unity
        rend = GetComponentInChildren<SpriteRenderer>();
        rend.sortingOrder = 4;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRange = Vector2.Distance(transform.position, Player.transform.position);//calcula a distancia entre player e inimigo
        if (currentRange <= panicRange)//ativa barra de panico se a criatura estiver muito perto
        {
            PanicMinigame.TriggerMinigame();
        }

        if (currentRange <= 10)
        {
            if (!soundManager.IsPlaying("EnemyWalk")) soundManager.PlayOneShot("EnemyWalk");

            m_Rigidbody.velocity = new Vector2(speed, 0);

            if (m_Rigidbody.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (m_Rigidbody.velocity.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }

        }
        else m_Rigidbody.velocity = Vector2.zero;//limita movimentação do inimigo somente quando jogador estiver perto


    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, panicRange);//utilidade visual do editor
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bounds"))
        {
            speed = -speed;
        }//inverte movimento se não tiver pra onde ir

        if (other.gameObject.CompareTag("Player"))
        {
            FadeControl.instance.FadeToBlack(1);
            GameManager.LoadScene("Death");//getting caught means game over
            Debug.Log("Game should end because player died");
        }//game over
    }
}
