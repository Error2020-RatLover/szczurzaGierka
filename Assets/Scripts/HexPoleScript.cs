// using System.Numerics;
// using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections.Generic;

public class HexPoleScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Vector2Int coordinates;
    public bool isAvaliable;

    public List<GameObject> neighbors = new();

    public CapsuleCollider2D button;

    public SpriteRenderer sprite;

    public roomGenerator generator;

    void Start()
    {
        //coordinates = new Vector2(transform.position.x, transform.position.y);

    }

    public void DisplayCords()
    {
        //Debug.Log(coordinates.x + ", " + coordinates.y);
        foreach(GameObject x in neighbors)
        {
            Debug.Log(x);

        }
            
    }

    private void OnMouseDown()
    {
        generator.resetBoard();
        //DisplayCords();
        setActiveHex();
        enable();
    }

    public void unable()
    {
        sprite.enabled = true;
        button.enabled = true;
        Debug.Log(coordinates);
    }

    public void enable()
    {
        sprite.enabled = false;
        button.enabled = false;
    }

    public void setActiveHex(int signalStrength = 1)
    {
        
        signalStrength--;
        
        foreach (GameObject x in neighbors)
        {
            HexPoleScript pole = x.GetComponent<HexPoleScript>();
            pole.unable();
           
            if (signalStrength > 0)
            {
                pole.setActiveHex(signalStrength);
            }
        }

        
    }

}
