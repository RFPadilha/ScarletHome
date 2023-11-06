using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;//referencias a UI onde aparecem nome e texto

    public Animator animator;//referencia a animação de aparição da caixa

    private Queue<string> sentences;//referencia as frases dos objetos interagiveis
    public bool isTyping = false;
    string sentence;
    void Start()
    {
        sentences = new Queue<string>();//inicializa
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);//anima a entrada

        nameText.text = dialogue.name;//nome da fonte de dialogo

        sentences.Clear();//limpeza de codigo

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);//encadeia as frases do interagivel
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 && !isTyping)
        {
            EndDialogue();//encerra dialogo se nao existem mais frases
            return;
        }
        if (!isTyping)
        {
            sentence = sentences.Dequeue();//remove frase dita
            StopAllCoroutines();//garante que não há frases sendo digitadas
            StartCoroutine(TypeSentence(sentence));//digita nova frase
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = sentence;
            isTyping = false;
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;//adiciona letras uma a uma ao vetor
            yield return null;
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);//anima a saída
    }
}
