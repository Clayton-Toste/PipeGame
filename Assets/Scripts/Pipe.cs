using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    #region Constants
    public Sprite[] sprites;
    public Color[] colors;
    public GameObject particlesUp, particlesRight, particlesDown, particlesLeft, particlesCenter;
    public bool visualize = true;
    #endregion

    #region Context
    protected Pipe up, left, down, right;
    #endregion

    #region Components
    private SpriteRenderer spriteRenderer;
    private ParticleSystem.MainModule particlesUpMain, particlesRightMain, particlesDownMain, particlesLeftMain, particlesCenterMain;
    #endregion

    #region Status
    public const int EMPTY = 2147483647;
    public int distance = EMPTY;
    public int contents = 0;
    #endregion

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        particlesUpMain = particlesUp.GetComponent<ParticleSystem>().main;
        particlesRightMain = particlesRight.GetComponent<ParticleSystem>().main;
        particlesDownMain = particlesDown.GetComponent<ParticleSystem>().main;
        particlesLeftMain = particlesLeft.GetComponent<ParticleSystem>().main;
        particlesCenterMain = particlesCenter.GetComponent<ParticleSystem>().main;

    }

    public bool Connect(Pipe other, bool force, bool connect)
    {
        if (other.contents != 0 && contents != 0 && other.contents != contents)
        {
            return false;
        }

        connect = UpdateConnection(other, force, connect);
        int systemContents = contents != 0 ? contents : other.contents; 
        UpdateContents(systemContents, connect);
        other.UpdateContents(systemContents, connect);

        FindObjectOfType<SoundManager>().Play(connect ? "AddPipe" : "RemovePipe");

        return connect;
    }

    protected bool UpdateConnection(Pipe other, bool force, bool connect)
    {
        if (other.transform.position.y > transform.position.y + 0.1f)
        {
            if (!force || connect == (up == null))
            {
                connect = (up == null);
                up = up ? null : other;
                other.down = other.down ? null : this;
            }
        }
        else if (other.transform.position.x + 0.1f < transform.position.x)
        {
            if (!force || connect == (left == null))
            {
                connect = (left == null);
                left = left ? null : other;
                other.right = other.right ? null : this;
            }
        }
        else if (other.transform.position.y + 0.1f < transform.position.y)
        {
            if (!force || connect == (down == null))
            {
                connect = (down == null);
                down = down ? null : other;
                other.up = other.up ? null : this;
            }
        }
        else if (other.transform.position.x > transform.position.x + 0.1f)
        {
            if (!force || connect == (right==null))
            {
                connect = (right==null);
                right = right ? null : other;
                other.left = other.left ? null : this;
            }
        }

        return connect;
    }

    protected void UpdateContents(int systemContents, bool connect)
    {
        if (distance == 0)
        {
            return;
        }

        Queue<Pipe> emptying = new Queue<Pipe>(), filling = new Queue<Pipe>(), toUpdate = new Queue<Pipe>();
        int minDistance = distance;

        if (!connect)
        {
            emptying.Enqueue(this);
        }
        filling.Enqueue(this);

        while (emptying.Count != 0)
        {
            Pipe checking = emptying.Dequeue();

            if (checking.distance > minDistance || checking == this)
            {
                checking.contents = 0;
                checking.distance = EMPTY;

                if (checking.up && checking.up.distance != EMPTY)
                {
                    emptying.Enqueue(checking.up);
                }
                if (checking.left && checking.left.distance != EMPTY)
                {
                    emptying.Enqueue(checking.left);
                }
                if (checking.down && checking.down.distance != EMPTY)
                {
                    emptying.Enqueue(checking.down);
                }
                if (checking.right && checking.right.distance != EMPTY)
                {
                    emptying.Enqueue(checking.right);
                }
            }
            else
            {
                filling.Enqueue(checking); 
            }
        }
        
        while (filling.Count != 0)
        {
            Pipe checking = filling.Dequeue();

            int newDistance = EMPTY;
            if (checking.up && checking.up.distance < newDistance)
            {
                newDistance = checking.up.distance;
            }
            if (checking.left && checking.left.distance < newDistance)
            {
                newDistance = checking.left.distance;
            }
            if (checking.down && checking.down.distance < newDistance)
            {
                newDistance = checking.down.distance;
            }
            if (checking.right && checking.right.distance < newDistance)
            {
                newDistance = checking.right.distance;
            }

            if (newDistance < checking.distance)
            {
                checking.distance = newDistance+1;
                checking.contents = systemContents;

                if (checking.up && checking.up.distance > newDistance+2)
                {
                    filling.Enqueue(checking.up);
                }
                if (checking.left && checking.left.distance > newDistance+2)
                {
                    filling.Enqueue(checking.left);
                }
                if (checking.down && checking.down.distance > newDistance+2)
                {
                    filling.Enqueue(checking.down);
                }
                if (checking.right && checking.right.distance > newDistance+2)
                {
                    filling.Enqueue(checking.right);
                }
            }
        }
    }

    public void Refresh()
    {
        UpdateSprite();
        UpdateParticles();
    }

    protected void UpdateSprite()
    {
        if (!visualize)
        {
            spriteRenderer.enabled = false;
            return;
        }

        int index = 0;
        if (up)
        {
            index += 8;
        }

        if (right)
        {
            index += 4;
        }
        
        if (down)
        {
            index += 2;
        }

        if (left)
        {
            index += 1;
        }

        spriteRenderer.sprite = sprites[index];
    }

    protected void UpdateParticles()
    {
        if (contents == 0 || !visualize)
        {
            particlesUp.SetActive(false);
            particlesRight.SetActive(false);
            particlesDown.SetActive(false);
            particlesLeft.SetActive(false);
            particlesCenter.SetActive(false);
            return;
        }
        particlesUp.SetActive(up);
        particlesUpMain.startColor = colors[contents-1];
        particlesRight.SetActive(right);
        particlesRightMain.startColor = colors[contents-1];
        particlesDown.SetActive(down);
        particlesDownMain.startColor = colors[contents-1];
        particlesLeft.SetActive(left);
        particlesLeftMain.startColor = colors[contents-1];
        particlesCenter.SetActive(up || right || down || left);
        particlesCenterMain.startColor = colors[contents-1];
    }
}
