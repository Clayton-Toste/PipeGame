using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Toggle invertScrollToggle;
    private bool invertScroll;
    private Camera cam;
    private Vector3 mouseLock;

    public void Refresh()
    {
        invertScroll = invertScrollToggle.isOn;
        if (invertScroll)
        {
            PlayerPrefs.SetInt("invertScroll", 1);
        }
        else
        {
            PlayerPrefs.SetInt("invertScroll", 0);
        }
    }

    public void Start()
    {
        if (!PlayerPrefs.HasKey("invertScroll"))
        {
            PlayerPrefs.SetInt("invertScroll", 0);
        } 
        invertScroll = PlayerPrefs.GetInt("invertScroll") == 1;
        invertScrollToggle.isOn = invertScroll;

        cam = GetComponent<Camera>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mouseLock = Input.mousePosition;
        }
        
        if (Input.GetMouseButton(1))
        {
            transform.position += cam.ScreenToWorldPoint(mouseLock) - cam.ScreenToWorldPoint(Input.mousePosition);
            mouseLock = Input.mousePosition;
        }
        else
        {
            if (invertScroll)
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize+Input.mouseScrollDelta.y, 4, 7);
            else
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize-Input.mouseScrollDelta.y, 4, 7);
        }
    }
}
