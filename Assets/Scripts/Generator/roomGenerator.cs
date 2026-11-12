using UnityEngine;
using System.Collections.Generic;
using System;
// using System.Diagnostics;



public class roomGenerator : MonoBehaviour
{


    public int x_size;
    public int y_size;

    public GameObject hexpole;

    public BoxCollider2D rightWall;
    public BoxCollider2D leftWall;
    public BoxCollider2D topWall;
    public BoxCollider2D bottomWall;

    public GameObject enemyPrefab;

    public Dictionary<Vector2Int, GameObject> pola = new();

    // itemy
    public GameObject hat;


    public GameObject championPrefab;
    public GameObject currentChampion;


    public List<Pole> prefabsOverwrite = new List<Pole>();

    public string actionState = "Move";


    void Start()
    {
        // to jest potrzebne żeby się nie popsuło, można potem poszukać lepszego rozwiązania

        float rozmiar = x_size / 2;
        x_size = Mathf.CeilToInt(rozmiar);

        // 2 pętle generujące mape
        for (int i = 0; i < x_size; i++)
        {

            for (int j = 0; j < y_size; j++)
            {
                // posFixed sprawdza czy należy zmodyfikować pozycje
                float posFixed = 0;
                if (j % 2 == 1)
                {
                    posFixed = 1.1f;
                }
                Vector3 position = new Vector3((i) * 2.2f + posFixed, (j) * 0.6f, 0);
                // Quaternion rotation = Quaternion.Euler(0, 0, 0);  //usuwam tą zmienną bo jest niepotrzebna
                GameObject x = Instantiate(hexpole, position, Quaternion.Euler(0, 0, 0));

                HexPoleScript pole = x.GetComponent<HexPoleScript>();

                pole.coordinates = new Vector2Int(i * 2, j);
                if (j % 2 == 1)
                {
                    pole.coordinates.x += 1;

                }
                pole.generator = this;
                pola.Add(new Vector2Int(pole.coordinates.x, pole.coordinates.y), x);
                

                // spawn itemu(narazie 10%) z wyjątkiem pola startowego(0,0), bedzie trzeba zmienić jak będziemy chcieli dać inne miejsca do spawnowania się gracza
                Vector3 startPosition = new Vector3(0,0,0);
                if (position != startPosition)
                {
                    if (DrawAtRandom(0.1f))
                    {

                        GameObject item = Instantiate(hat, position, Quaternion.Euler(0, 0, 0));
                        //dodaje do hexpola item
                        pole.item = item;
                        ItemScript itemScript = item.GetComponent<ItemScript>();
                        itemScript.itemCords = pole.coordinates;//przekazanie koordynatów polu

                    }
                }



            }


        }

        // ROZMIARY ŚCIAN

        rightWall.size = new Vector2(1.0f, y_size + 8);
        leftWall.size = new Vector2(1.0f, y_size + 8);

        topWall.size = new Vector2(x_size * 2f + 8, 1.0f);
        bottomWall.size = new Vector2(x_size * 2f + 8, 1.0f);

        //POZYCJE ŚCIAN

        rightWall.offset = new Vector2(-2.0f, 0.0f);
        leftWall.offset = new Vector2((rozmiar - 1) * 2.2f + 2, 0.0f);

        bottomWall.offset = new Vector2((rozmiar * 2.2f) / 2, -2f);
        topWall.offset = new Vector2((rozmiar * 2.2f) / 2, ((y_size - 1) * 0.6f) + 2f);


        //przypisanie sąsiadów do każdego pola (wydaje mi się że się przyda)
        foreach (KeyValuePair<Vector2Int, GameObject> x in pola)
        {
            //Debug.Log($"{x.Key}: {x.Value}");
            List<GameObject> neighbors = new();
            List<Vector2Int> kordynaty = new();
            kordynaty.Add(new Vector2Int(-1, -1));
            kordynaty.Add(new Vector2Int(0, -2));
            kordynaty.Add(new Vector2Int(1, -1));
            kordynaty.Add(new Vector2Int(-1, 1));
            kordynaty.Add(new Vector2Int(0, 2));
            kordynaty.Add(new Vector2Int(1, 1));

            foreach (Vector2Int pos in kordynaty)
            {
                if (pola.ContainsKey(x.Key + pos))
                {
                    neighbors.Add(pola[x.Key + pos]);
                }

            }

            HexPoleScript pole = x.Value.GetComponent<HexPoleScript>();
            pole.neighbors = neighbors;
            pole.dostepneAkcje.Add("Move", true);
            pole.dostepneAkcje.Add("Occupated", false);
            //if (x.Key.x == 3)
            //{
            //    pole.dostepneAkcje["Move"]= false;
            //}

        }

        foreach(Pole x in prefabsOverwrite)
        {
            GameObject prefab = Instantiate(x.prefab);
            inreractiveObject prefabScrypt = prefab.GetComponent<inreractiveObject>();
            prefabScrypt.generator = this;
            prefabScrypt.inicialiate(x.koordynaty);
        }

        resetBoard();
        Vector2Int drzwiCord = new Vector2Int(0, 0);
        HexPoleScript drzwi = pola[drzwiCord].GetComponent<HexPoleScript>();
        drzwi.setActiveHex();
        currentChampion = Instantiate(championPrefab, new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 0));
        currentChampion.GetComponent<ChampionScript>().generator = this;
        currentChampion.GetComponent<ChampionScript>().przeniesNaPozycje(drzwiCord);
        currentChampion.GetComponent<ChampionScript>().currentPole = drzwi;
        drzwi.dostepneAkcje["Occupated"]= true;
    }

    // Losowanie szansa = 0.9f -> 90%, szansa = 0.02 -> 2%
    bool DrawAtRandom(float szansa)
    {
        return UnityEngine.Random.value < szansa;
    }


    public void resetBoard()
    {
        
        foreach (KeyValuePair<Vector2Int, GameObject> x in pola)
        {
            HexPoleScript pole = x.Value.GetComponent<HexPoleScript>();
            pole.enable();
        }
    }

    public void takeAction(Vector2Int cords)
    {
        currentChampion.GetComponent<ChampionScript>().actionToTake(cords);
    }
}
