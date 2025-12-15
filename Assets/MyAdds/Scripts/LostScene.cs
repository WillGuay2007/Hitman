using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LostScene : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("Playground");
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            PlayAgain(); //Je preferais le vieux systeme d'input.
        }
    }

}
