using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public GameObject sightDirection;
    public float aggroRange;
    public float speed;

    public GameObject text;

    float currentRange;
    Rigidbody2D m_Rigidbody;

    void Start()
    {
        text.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentRange = Vector2.Distance(sightDirection.transform.position, Player.transform.position);//calcula a distancia entre player e inimigo
        if (currentRange <= aggroRange && !Player.GetComponent<PlayerScript>().isHidden)//se está dentro da distância de aggro e o jogador não está escondido:
        {
            Vector2 velocity = new Vector2((transform.position.x - Player.transform.position.x), 0);
            m_Rigidbody.velocity = -velocity.normalized * speed * 2; ;//corre em direção ao jogador
        }else m_Rigidbody.velocity = new Vector2(speed, 0);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sightDirection.transform.position, aggroRange);//utilidade para visualizar range de aggro
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
        }
    }
}
