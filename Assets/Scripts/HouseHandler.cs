using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHandler : MonoBehaviour
{
    House house;

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }
    }

    public void PlaceHouse(Vector2 location, int contents)
    {
        RaycastHit2D[] pipes = Physics2D.BoxCastAll(location, new Vector2(0.5f, 0.5f), 0f, Vector2.zero);
        
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].transform.gameObject);
        }

        Instantiate(house, new Vector3(location.x, location.y, 0), Quaternion.identity, transform);
    }
}
