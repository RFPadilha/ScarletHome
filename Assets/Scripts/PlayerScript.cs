using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //component references
    Rigidbody2D m_Rigidbody;
    SoundManager soundManager;
    [SerializeField] GameObject switchRoomsText;

    //movement variables
    public float horMove = 0f;
    public float runSpeed;
    public float stealthSpeed;
    public float maxSpeed;

    //hiding variables
    SpriteRenderer rend;
    public bool canHide;
    public bool isHidden;

    //transition variables
    public bool canExitRoom;
    public bool facingRight;
    public Animator m_Animator;
    TeleportScript door;

    
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
        soundManager = SoundManager.instance;

        switchRoomsText.gameObject.SetActive(false);
        canHide = false;
        isHidden = false;


        FadeControl.instance.FadeIn(5);
        if (soundManager.IsPlaying("Burning")) soundManager.StopSound("Burning");
    }

    void Update()
    {
        horMove = Input.GetAxisRaw("Horizontal");
        facingRight = false;
        Hide();
    }

    void FixedUpdate()
    {
        Movement();
        FlipSprites();
        ExitRoom();
    }
    void Movement()
    {
        m_Rigidbody.velocity = new Vector2(horMove * maxSpeed, -10);
        if (horMove != 0 && !soundManager.IsPlaying("SmallStep"))
        {
            soundManager.PlaySound("SmallStep");
        }
        if(horMove == 0 && soundManager.IsPlaying("SmallStep"))
        {
            soundManager.StopSound("SmallStep");
        }

        m_Animator.SetFloat("Speed", Mathf.Abs(horMove));

        
    }

    void Hide()
    {
        if(!isHidden && canHide && Input.GetKeyDown(KeyCode.Space))
        {
            Physics2D.IgnoreLayerCollision(8, 9, true);
            rend.sortingOrder = 2;
            isHidden = true;
        }//se consegue se esconder, barra de espaço esconde
        else if(isHidden && canHide && Input.GetKeyDown(KeyCode.Space) || isHidden && !canHide)
        {
            Physics2D.IgnoreLayerCollision(8, 9, false);
            rend.sortingOrder = 5;
            isHidden = false;
        }//se está escondido, barra de espaço sai do esconderijo
    }

    void ExitRoom()
    {
        if (canExitRoom)
        {
            switchRoomsText.gameObject.SetActive(true);
        }else switchRoomsText.gameObject.SetActive(false);

        if (canExitRoom && door.destination != null && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TransportPlayer(door.destination));
        }
    }
    IEnumerator TransportPlayer(Transform destination)
    {
        FadeControl.instance.FadeToBlack(6);
        if(soundManager.IsPlaying("Monster")) soundManager.StopSound("Monster");
        if (soundManager.IsPlaying("EnemyWalk")) soundManager.StopSound("EnemyWalk");
        soundManager.PlayOneShot("OpenDoor");
        yield return new WaitForSeconds(1);
        transform.position = destination.position;
        FadeControl.instance.FadeIn(6);
    }
    void FlipSprites()
    {
        if(m_Rigidbody.velocity.x > 0)
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }else if (m_Rigidbody.velocity.x < 0)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hide"))
        {
            canHide = true;
        }

        if (other.gameObject.CompareTag("Transition"))
        {
            door = other.gameObject.GetComponent<TeleportScript>();
            canExitRoom = true;
        }

        if (other.gameObject.CompareTag("NoiseArea"))
        {
            if (!soundManager.IsPlaying("Dripping")){
                soundManager.PlayLooped("Dripping");
            }
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hide"))
        {
            canHide = false;
        }

        if (other.gameObject.CompareTag("Transition"))
        {
            door = null;
            canExitRoom = false;
        }

        if (other.gameObject.CompareTag("NoiseArea"))
        {
            if (soundManager.IsPlaying("Dripping")){ 
                soundManager.StopSound("Dripping");
            }
        }

    }
}
