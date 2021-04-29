using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFallFromSky : MonoBehaviour
{
    [SerializeField] float minY, maxY;
    [SerializeField] float minX, maxX;
    [SerializeField] float speed;
    [SerializeField] float opacity;

    SpriteRenderer sr;
    Transform tr;
    Animator animator;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        SetToTop();
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, (-speed * Time.deltaTime), 0));

        if(transform.position.y <= minY)
        {
            SetToTop();
        }
    }

    private void SetToTop()
    {
        int rand = Random.Range(0, 8);

        switch (rand)
        {
            case 0:
                sr.color = new Color(opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x);
                sr.sortingOrder = 0;
                tr.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                animator.speed = 1;
                speed = 5;
                break;
            case 1:
                sr.color = new Color(opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x);
                sr.sortingOrder = 1;
                tr.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                animator.speed = 0.9f;
                speed = 4.5f;
                break;
            case 2:
                sr.color = new Color(opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x);
                sr.sortingOrder = 2;
                tr.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                animator.speed = 0.8f;
                speed = 4;
                break;
            case 3:
                sr.color = new Color(opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x);
                sr.sortingOrder = 3;
                tr.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                animator.speed = 0.7f;
                speed = 3.5f;
                break;
            case 4:
                sr.color = new Color(opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x);
                sr.sortingOrder = 4;
                tr.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                animator.speed = 0.6f;
                speed = 3;
                break;
            case 5:
                sr.color = new Color(opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x);
                sr.sortingOrder = 5;
                tr.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                animator.speed = 0.5f;
                speed = 2.5f;
                break;
            case 6:
                sr.color = new Color(opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x);
                sr.sortingOrder = 6;
                tr.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                animator.speed = 0.4f;
                speed = 2;
                break;
            case 7:
                sr.color = new Color(opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x);
                sr.sortingOrder = 7;
                tr.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                animator.speed = 0.3f;
                speed = 1.5f;
                break;
            case 8:
                sr.color = new Color(opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x, opacity * tr.localScale.x);
                sr.sortingOrder = 8;
                tr.localScale = new Vector3(1, 1, 1);
                animator.speed = 0.2f;
                speed = 1;
                break;
            default:
                break;
        }
        tr.position = new Vector3(Random.Range(minX, maxX), maxY, 0);
    }


}
