using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHandler : MonoBehaviour
{
    public GameObject pipe;

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void PlaceTile(Vector2 location)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(pipe, new Vector3(location.x+i,  location.y+j, 0) * 1.6f, Quaternion.identity, transform);
            }
        }
    }
    
    public void RefreshAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Pipe>().Refresh();
        }
    }
}
