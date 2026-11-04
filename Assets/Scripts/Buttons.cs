using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void ChangeScene()
    {
        Debug.Log("start Button clicked!");
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("quit Button clicked!");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        //Wystarczy samo Application.Quit(); ale wtedy nie wyłącza w unity play mode
    }


}
