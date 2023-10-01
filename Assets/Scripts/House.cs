using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Pipe up, right, down, left;
    public GameObject[] alerts;
    public bool[] demands;
    public float patience = 60f;
    public bool dead = false;
    public Animator animator;

    public void Update()
    {
        if (dead)
        {
            return;
        }

        bool satisfied = true;

        for (int i = 0; i < demands.Length; i++)
        {
            if (!demands[i])
            {
                continue;
            }

            if (up.contents == i+1 || right.contents == i+1 || down.contents == i+1 || left.contents == i+1)
            {
                alerts[i].SetActive(false);
            }
            else
            {
                alerts[i].SetActive(true);
                satisfied = false;
            }
        }

        if (satisfied)
        {
            patience = 60f;
        }
        else
        {
            patience -= Time.deltaTime;
        }

        if (patience <= 0f)
        {
            animator.SetTrigger("Explode");
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Hit();
            alerts[0].SetActive(false);
            alerts[1].SetActive(false);
            alerts[2].SetActive(false);
            alerts[3].SetActive(false);
            dead = true;
        }
    }

    public void AddDemands()
    {
        demands[(int)Random.Range(0, 4)] = true;
    }
}
