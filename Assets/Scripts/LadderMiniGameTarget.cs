using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMiniGameTarget : MonoBehaviour
{

    public float timeToVisualizeSucess = 0.5f;
    public Color successColor = Color.green;
    public AudioClip pointSound;

    private LadderMiniGameButton heldButton = null;
    private SpriteRenderer spriteRenderer;
    private float successVisualizationTimeLeft = 0f;
    private AudioSource audioSource;
    private Hero hero;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        hero = FindObjectOfType<Hero>();
    }

    void Update()
    {
        if (successVisualizationTimeLeft > 0)
        {
            spriteRenderer.color = Color.Lerp(Color.white, successColor, successVisualizationTimeLeft);
            successVisualizationTimeLeft -= Time.deltaTime;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<LadderMiniGameButton>())
        {
            heldButton = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<LadderMiniGameButton>())
        {
            if (heldButton != null)
                Debug.LogError("Zwolnij itemy albo zmniejsz czestotliwosc.");

            heldButton = other.GetComponent<LadderMiniGameButton>();
        }
    }

    public LadderMiniGameButton GetHeldButton()
    {
        return heldButton;
    }

    public void VisualizeGood()
    {
        spriteRenderer.color = successColor;
        successVisualizationTimeLeft = timeToVisualizeSucess;
        PlaySound(pointSound);
    }

    public void PlaySound(AudioClip clip)
    {
        FindObjectOfType<SoundPlayer>().PlaySound(clip);
    }
}
