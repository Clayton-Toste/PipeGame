using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryHandler : MonoBehaviour
{
    public GameObject factory;

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void PlaceFactory(Vector2 location, int contents)
    {
        RaycastHit2D[] pipes = Physics2D.BoxCastAll(location * 1.6f, new Vector2(1.5f, 1.5f) * 1.6f, 0f, Vector2.zero);
        
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].transform.gameObject);
        }

        Instantiate(factory, new Vector3(location.x, location.y, 0) * 1.6f, Quaternion.identity, transform).GetComponent<Factory>().Contents = contents;
    }
}
