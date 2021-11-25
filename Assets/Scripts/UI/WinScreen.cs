using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
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

    public void ReplayGame()
    {
        _eventSystem.enabled = false;
        Invoke("PlayLoad", 0.2f);
    }
    public void MainMenu()
    {
        _eventSystem.enabled = false;
        Invoke("MainMenuLoad", 0.2f);
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
    private void MainMenuLoad()
    {
        PlayScene("Credits_Page");
    }

    public void PlayScene(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }
}
