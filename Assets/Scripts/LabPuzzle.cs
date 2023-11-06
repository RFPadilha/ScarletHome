using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPuzzle : MonoBehaviour
{
    [SerializeField] Transform finalDestination;
    [SerializeField] CycleColor[] labFlasks;

    GameObject player;
    SoundManager soundManager;

    bool solved = false;
    Dictionary<int, Sprite> flaskSprites;

    private void Awake()
    {
        flaskSprites = new Dictionary<int, Sprite>();
    }

    void Start()
    {
        LoadSprites();
        player = GameObject.FindGameObjectWithTag("Player");
        soundManager = SoundManager.instance;
        RandomizePuzzle();
    }

    private void Update()
    {
        if (!solved)
        {
            if (labFlasks[0].color < labFlasks[1].color && labFlasks[1].color < labFlasks[2].color)
            {
                solved = true;
                StartCoroutine(TransportPlayer());
            }
        }
    }

    IEnumerator TransportPlayer()
    {
        soundManager.PlayOneShot("OpenVent");
        FadeControl.instance.FadeToBlack(3);
        yield return new WaitForSeconds(4);
        player.transform.position = finalDestination.position;
        FadeControl.instance.FadeIn(3);
    }

    void RandomizePuzzle()
    {
        TryRandomizeIncompletePuzzle();
        while (labFlasks[0].color < labFlasks[1].color && labFlasks[1].color < labFlasks[2].color)
        {
            TryRandomizeIncompletePuzzle();
        }
    }
    void TryRandomizeIncompletePuzzle()
    {
        for (int i = 0; i < labFlasks.Length; i++)
        {
            SetFlaskColor(labFlasks[i], Mathf.FloorToInt(Random.Range(0, Mathf.FloorToInt(7 / (i + 1)))));
        }
    }

    public void NextColor(CycleColor flask)
    {
        int nextColor;
        if (flask.color >= 6) nextColor = 0;
        else if (flask.color < 0) nextColor = 6;
        else nextColor = flask.color + 1;

        SetFlaskColor(flask, nextColor);
    }

    void SetFlaskColor(CycleColor flask, int value)
    {
        Sprite sprite;
        flaskSprites.TryGetValue(value, out sprite);
        flask.spriteRenderer.sprite = sprite;
        flask.color = value;
    }
    void LoadSprites()
    {
        flaskSprites.Add(0, Resources.Load<Sprite>("Sprites/AlchemyFlasks/Red"));
        flaskSprites.Add(1, Resources.Load<Sprite>("Sprites/AlchemyFlasks/Orange"));
        flaskSprites.Add(2, Resources.Load<Sprite>("Sprites/AlchemyFlasks/Yellow"));
        flaskSprites.Add(3, Resources.Load<Sprite>("Sprites/AlchemyFlasks/Green"));
        flaskSprites.Add(4, Resources.Load<Sprite>("Sprites/AlchemyFlasks/Blue"));
        flaskSprites.Add(5, Resources.Load<Sprite>("Sprites/AlchemyFlasks/Indigo"));
        flaskSprites.Add(6, Resources.Load<Sprite>("Sprites/AlchemyFlasks/Violet"));
    }
    
}
