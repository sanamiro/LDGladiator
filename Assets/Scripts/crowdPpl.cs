using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdPpl : MonoBehaviour
{
    public Sprite[] head = new Sprite[3];
    public int amountOfBodyAnimation = 3;

    private SpriteRenderer spriteRendererBody;
    public List<Color32> couleursRandomPourBody = new List<Color32>();
    private SpriteRenderer spriteRendererHead;

    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRendererBody = GetComponent<SpriteRenderer>();
        spriteRendererHead = transform.Find("Crowd head").GetComponent<SpriteRenderer>();
        spriteRendererHead.sprite = head[Random.Range(0, head.Length)];
        spriteRendererBody.color = couleursRandomPourBody[Random.Range(0, couleursRandomPourBody.Count)];
        int randAnimationBody = Random.Range(0, amountOfBodyAnimation);
        if (randAnimationBody+1 == 1)
        {
            animator.SetBool("corps 1", true);
        }
        else if (randAnimationBody + 1 == 2)
        {
            animator.SetBool("corps 2", true);
        }
        else if (randAnimationBody + 1 == 3)
        {
            animator.SetBool("corps 3", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
