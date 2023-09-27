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
            Destroy(transform.GetChild(i));
        }
    }

    public void Expand(int size)
    {
        for (int i = -size; i < size; i++)
        {
            Instantiate(pipe, new Vector3(i,  size-1, 0), Quaternion.identity, transform);
            Instantiate(pipe, new Vector3(i, -size, 0), Quaternion.identity, transform);
        }
        for (int i = 1-size; i < size-1; i++)
        {
            Instantiate(pipe, new Vector3( size-1, i, 0), Quaternion.identity, transform);
            Instantiate(pipe, new Vector3(-size, i, 0), Quaternion.identity, transform);
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
