// using System.Numerics;
// using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections.Generic;

public class HexPoleScript : MonoBehaviour
{
   

    public Vector2Int coordinates;
    public bool isAvaliable;

    public List<GameObject> neighbors = new();

    public CapsuleCollider2D button;

    public SpriteRenderer sprite;

    public roomGenerator generator;

    public Dictionary<string, bool> dostepneAkcje = new();


    void Start()
    {
        
        //coordinates = new Vector2(transform.position.x, transform.position.y);
        
    }


    public void DisplayCords()
    {
        //Debug.Log(coordinates.x + ", " + coordinates.y);
        //foreach(GameObject x in neighbors)
        //{
        //    Debug.Log(x);

        //}
            
    }

    private void OnMouseDown()
    {
        
        //DisplayCords();
        //setActiveHex();
        enable();
        generator.takeAction(coordinates);
    }

    public void disable()
    {
        if (dostepneAkcje[generator.actionState])
        {
            sprite.enabled = true;
            button.enabled = true;
        }
  
        //Debug.Log(coordinates);
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
            pole.disable();
           
            if (signalStrength > 0)
            {
                pole.setActiveHex(signalStrength);
            }
        }

        
    }

}
