using UnityEngine;

public class roomGenerator : MonoBehaviour
{

    public int x_size;
    public int y_size;

    public GameObject hexpole;


    void Start()
    {

        for (int i = 0; i < x_size; i++)
        {
           
           
            for (int j = 0; j < y_size; j++)
            {
                float posFixed = 0;
                if (j % 2 == 1)
                {
                    posFixed = 1.1f;
                }
                Vector3 position = new Vector3((i) * 2.2f + posFixed, (j) * 0.6f, 0);
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                GameObject x = Instantiate(hexpole, position, rotation);

                HexPoleScript pole = x.GetComponent<HexPoleScript>();

                pole.coordinates = new Vector2(i, j);
            }
        }
     


    }

   
    void Update()
    {
        
    }
}
