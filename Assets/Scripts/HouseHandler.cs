using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHandler : MonoBehaviour
{
    public GameObject house;
    private int activeHouses;
    private float activeTimer;

    public void Update()
    {
        while (activeTimer > 30f)
        {
            if (activeHouses < transform.childCount)
            {
                transform.GetChild(activeHouses).GetComponent<House>().AddDemands();
            }
            activeHouses += 1;
            activeTimer -= 30f;
        }
        
        activeTimer += Time.deltaTime;
    }

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void PlaceHouse(Vector2 location)
    {
        RaycastHit2D[] pipes = Physics2D.BoxCastAll((location + new Vector2(0.5f, 0.5f)) * 1.6f, new Vector2(1f, 1f) * 1.6f, 0f, Vector2.zero);
        
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].transform.gameObject);
        }

        Instantiate(house, new Vector3(location.x + 0.5f, location.y + 0.5f, 0) * 1.6f, Quaternion.identity, transform);
    }

    public void PlaceTile(Vector2 location)
    {
        for (int _ = 0; _<7; _++)
        {
            int x = Random.Range(0, 9), y = Random.Range(0, 9);
            bool collision = false;
            
            RaycastHit2D[] pipes = Physics2D.BoxCastAll((location + new Vector2(0.5f, 0.5f) + new Vector2(x, y)) * 1.6f, new Vector2(2f, 2f) * 1.6f, 0f, Vector2.zero);

            for (int i = 0; i < pipes.Length; i++)
            {
                if (pipes[i].transform.parent.tag == "Factory" || pipes[i].transform.parent.tag == "House")
                {
                    collision = true;
                }
            }

            if (!collision)
            {
                PlaceHouse(location + new Vector2(x, y));
            }
        }
    }
}
