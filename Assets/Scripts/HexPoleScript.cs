// using System.Numerics;
// using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class HexPoleScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector2 coordinates;
    public bool isAvaliable;

    void Start()
    {
        coordinates = new Vector2(transform.position.x, transform.position.y);

    }

    public void DisplayCords()
    {
        Debug.Log(coordinates.x + ", " + coordinates.y);

    }
    private void OnMouseDown()
    {
        DisplayCords();
    }

}
