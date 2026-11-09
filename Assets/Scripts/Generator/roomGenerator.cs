using UnityEngine;

public class roomGenerator : MonoBehaviour
{

    public int x_size;
    public int y_size;

    public GameObject hexpole;


    void Start()
    {
        Vector3 position = new Vector3(5f, 0f, 2f);
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        Instantiate(hexpole, position, rotation);
    }

   
    void Update()
    {
        
    }
}
