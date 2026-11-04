
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class SettingsOpenerInGame : MonoBehaviour
{
    public GameObject settingsObject;

    void Update()
{
    if (Keyboard.current != null &&
        Keyboard.current.escapeKey.wasPressedThisFrame)
    {
        settingsObject.SetActive(!settingsObject.activeSelf);
        Debug.Log("Escape clicked");
    }
}
    public void BackToMenu()
    {
        Debug.Log("back to menu Button clicked!");
        SceneManager.LoadScene("Menu");
    }
}
