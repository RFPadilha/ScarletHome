using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanicMinigame : MonoBehaviour
{
    //target variables
    [Header("Minigame Position Objects")]
    public  GameObject panicUI;
    public GameObject panicTutorial;
    [SerializeField] Transform leftPivot;
    [SerializeField] Transform rightPivot;//limits of targets' position
    [SerializeField] Transform target;//the moving target itself

    SoundManager soundManager;
    
    [Header("Target Control Variables")]
    float targetPosition;
    float targetDestination;
    float targetTimer;
    [SerializeField] float timeMultiplier = 3f;
    float targetSpeed;
    [SerializeField] float smoothMotion = 1f;
    //end of target variables
    
    [Header("Player Control Variables")]
    [SerializeField] Transform playerBar;//referência a barra que vai ser movida
    float barPosition;
    [SerializeField] float barSize;
    float barMoveVelocity;//a velocidade da barra em si
    [SerializeField] float barMovePower = 0.03f;//a "força" aplicada para a barra se mexer

    //Variáveis de progresso de panico
    [SerializeField] Transform progressBar;//referência a barra de progresso
    public float startingBarProgress;
    float barProgress;
    [SerializeField] float barPower = 0.0000001f;//determina a velocidade de progresso
    [SerializeField] float barDegradationPower = 0.05f;//a velocidade que a barra de progresso retrocede
    //fim das variáveis de progresso de panico

    //variáveis auxiliares
    public static bool activePanic;
    bool activeGame = false;
    public float calmDuration = 10f;
    float elapsedCalm = 0f;
    bool firstPanic = true;

    void Start()
    {
        firstPanic = true;
        soundManager = SoundManager.instance;
        barSize = playerBar.localScale.x;
        activePanic = false;
        panicUI.SetActive(false);
        panicTutorial.SetActive(false);
    }

    void Update()
    {
        if (activePanic && elapsedCalm <= 0)
        {
            if (!activeGame)
            {
                if (firstPanic)
                {
                    ShowPanicTutorial();
                }
                barProgress = startingBarProgress;
                activeGame = true;
                panicUI.SetActive(true);
            }
            if (!soundManager.IsPlaying("Breathing"))
            {
                soundManager.PlayLooped("Breathing");
            }
            RandomizeTargetMovement();
            PanicBarControl();
            ProgressCheck();
        }else if (soundManager.IsPlaying("Breathing"))
        {
            soundManager.StopSound("Breathing");
        }
        if (elapsedCalm > 0)
        {
            activePanic = false;
            elapsedCalm -= Time.deltaTime;
        }
        
    }
    public void ClosePanicTutorial()
    {
        Time.timeScale = 1f;
        firstPanic = false;
        panicTutorial.SetActive(false);
    }
    public void ShowPanicTutorial()
    {
        panicTutorial.SetActive(true);
        Time.timeScale = 0f;
    }
    void PanicBarControl()
    {
        if (Input.GetMouseButton(0))//move para a direita
        {
            barMoveVelocity -= barMovePower * Time.deltaTime;
        } 
        if (Input.GetMouseButton(1))//move para a esquerda
        {
            barMoveVelocity += barMovePower * Time.deltaTime;
        }


        barPosition += barMoveVelocity;

        if((barPosition - (barSize / 2) <= 0 && barMoveVelocity < 0) || (barPosition + (barSize / 2) >= 1 && barMoveVelocity > 0))//se está tentando mover a barra além dos limites
        {
            barMoveVelocity = 0;//limita velocidade
        }

        barPosition = Mathf.Clamp(barPosition, barSize / 2, 1 - (barSize / 2));
        playerBar.position = Vector2.Lerp(leftPivot.position, rightPivot.position, barPosition);
    }

    void RandomizeTargetMovement()
    {
        targetTimer -= Time.deltaTime;
        if (targetTimer < 0f)
        {
            targetTimer = UnityEngine.Random.value * timeMultiplier;//move o peixe em uma direção por uma quantidade aleatória de tempo

            targetDestination = UnityEngine.Random.value;
        }
        targetPosition = Mathf.SmoothDamp(targetPosition, targetDestination, ref targetSpeed, smoothMotion);
        target.position = Vector2.Lerp(leftPivot.position, rightPivot.position, targetPosition);
    }

    void ProgressCheck()
    {
        Vector2 localScale = progressBar.localScale;
        localScale.x = barProgress;
        progressBar.localScale = localScale;

        float min = barPosition - barSize / 2;
        float max = barPosition + barSize / 2;//limites de onde a barra alcança

        if(min < targetPosition && targetPosition < max)//se alvo está dentro da barra controlada
        {
            barProgress += barPower * Time.deltaTime;//acalma
        }
        else
        {
            barProgress -= barDegradationPower * Time.deltaTime;//se não, panica
            
            if (barProgress <= 0)
            {
                Panic();
            }

        }

        if(barProgress >= 1)
        {
            StayCalm();
        }

        barProgress = Mathf.Clamp(barProgress, 0, 1);
    }

    void StayCalm()
    {
        activePanic = false;
        barProgress = 0;
        panicUI.SetActive(false);
        activeGame = false;
        elapsedCalm = calmDuration;
    }

    void Panic()
    {
        activePanic = false;
        SceneManager.LoadScene(5);
        Debug.Log("Game should end because player panicked");
    }

    public static void TriggerMinigame()
    {
        activePanic = true;
    }
}
