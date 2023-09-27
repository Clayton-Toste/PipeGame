using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public Pipe up, right, down, left;

    public int Contents {
        set {
            up.contents = value;
            right.contents = value;
            down.contents = value;
            left.contents = value;
        }
        get {
            return up.contents;
        }
    }
}
