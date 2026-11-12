using UnityEngine;

public class wall : MonoBehaviour
{
    public roomGenerator generator;
    public Vector2Int cords;
    public HexPoleScript pole;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void inicialiate(Vector2Int cord)
    {
        pole = generator.pola[cord].GetComponent<HexPoleScript>();
        transform.position = generator.pola[cord].transform.position;
        cords = cord;
        pole.dostepneAkcje["Move"] = false;
    }
}
