using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LabPuzzle : MonoBehaviour
{
    public Transform finalDestination;
    public GameObject[] rbFlasks;
    public GameObject[] selectedFlasks = new GameObject[3];
    PlayerScript player;
    GameObject[] auxFlasks = new GameObject[3];
    Vector3 puzzlePos;
    readonly float incremento = 1.5f;
    public string[] colors = new string[3];
    public string trigger = "";
    int[] randFlask = new int[3];
    int i, w, x, y, z = 0;//auxiliares

    SoundManager soundManager;

    int[] repeat = new int[3];//always resets for some reason
    int[] correctSequence = new int[3];
    int[] index = new int[3];

    void Start()
    {
        puzzlePos = new Vector3(8.6f, 27.78f, 0f);//local manual dos frascos de laboratório
        player = FindObjectOfType<PlayerScript>();
        soundManager = SoundManager.instance;
        
        for (i = 0; i < 3; i++)
        {
            
            randFlask[i] = UnityEngine.Random.Range(0, 6);//aleatoriza frasco

            while(z < randFlask.Length)//para todos os frascos
            {
                if ((randFlask[i] == randFlask[z]))//se for igual a elemento diferente
                {
                    if (z != i)//e não é o mesmo elemento
                    {
                        do
                        {
                            randFlask[i] = UnityEngine.Random.Range(0, 6);//roda denovo

                        } while (randFlask[i] == randFlask[z]);//até ser diferente
                    }
                }
                z++;
            }
            z = 0;

            DetermineColor(i, randFlask[i]);
            selectedFlasks[i] = Instantiate(rbFlasks[randFlask[i]], puzzlePos, Quaternion.identity);//coloca em jogo os frascos aleatórios
            selectedFlasks[i].tag = colors[i];
            index[i] = randFlask[i];
            correctSequence[i] = index[i];

            
            puzzlePos.x += incremento;
        }
        Array.Sort(correctSequence);

        for(w=0; w < selectedFlasks.Length; w++)
        {
            auxFlasks[w] = Instantiate(selectedFlasks[w], this.gameObject.transform.position, Quaternion.identity);
            auxFlasks[w].tag = selectedFlasks[w].tag;
        }
    }

    void CheckCompletion()
    {
        if (index[0] <= correctSequence[0] && index[1] <= correctSequence[1] && index[2] <= correctSequence[2])
        {
            soundManager.PlayOneShot("VentWalk");
            player.gameObject.transform.position = finalDestination.position;
        }
    }

    public void DetermineColor(int i, int cor)
    {
        if (cor == 0)
        {
            colors[i] = "Red";
        }
        else if (cor == 1)
        {
            colors[i] = "Orange";
        }
        else if (cor == 2)
        {
            colors[i] = "Yellow";
        }
        else if (cor == 3)
        {
            colors[i] = "Green";
        }
        else if (cor == 4)
        {
            colors[i] = "Blue";
        }
        else if (cor == 5)
        {
            colors[i] = "Indigo";
        }
        else colors[i] = "Violet"; 
    }

    public void Cycle()
    {
        
        Vector2 pos;//salva a posição do objeto que triggerou o evento
        for(x = 0; x < selectedFlasks.Length; x++)//selectedFlasks é o array dos objetos em jogo
        {
            if (trigger.Equals(selectedFlasks[x].tag))//verifica qual triggerou evento
            {
                pos = selectedFlasks[x].transform.position;//salva posição do trigger

                repeat[x]++;//salva que foi ativado 1 vez
                if(repeat[x] >= selectedFlasks.Length)//se foi ativado mais vezes do que o tamanho do ciclo
                {
                    repeat[x] = 0;//reseta o ciclo
                }
                Destroy(selectedFlasks[x]);//destrói original
                if (x + repeat[x] >= auxFlasks.Length)//auxFlasks é um array auxiliar idêntico ao selectedFlasks
                {
                    y = 0 + repeat[x];//novo frasco a se mostrar
                }
                else y = x + repeat[x];//garante a repetição dos elementos pré-selecionados
                selectedFlasks[x] = Instantiate(auxFlasks[y], pos, Quaternion.identity);//instancia novo objeto
                index[x] = randFlask[y];
                selectedFlasks[x].tag = auxFlasks[x].tag;//coloca a mesma tag para permitir identificação
                Debug.Log("Flask: " + x + " Repeat: " + repeat[x] + "Index: " + index[x]);
                CheckCompletion();
            }
        }
    }

}
