using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _playSound;

    [SerializeField]
    private EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem.enabled = true;
        Time.timeScale = 1;
    }

    public void PlayGame()
    {
        _eventSystem.enabled = false;
        Invoke("PlayLoad", 0.2f);
    }
    public void Credits()
    {
        _eventSystem.enabled = false;
        Invoke("CreditsLoad", 0.2f);
    }
    public void Instructions()
    {
        _eventSystem.enabled = false;
        Invoke("InstructionssLoad", 0.2f);
    }
    public void QuitGame()
    {
        _eventSystem.enabled = false;
        Application.Quit();
    }
    public void PlaySound()
    {
        Instantiate(_playSound, transform.position, Quaternion.identity);
    }

    private void PlayLoad()
    {
        PlayScene("main_scene");
    }
    private void CreditsLoad()
    {
        PlayScene("Credits");
    }
    private void InstructionssLoad()
    {
        PlayScene("Instructions");
    }

    public void PlayScene(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }
}
