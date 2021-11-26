using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseButton, _mainMenuButton, _quitButton, _bg, _whiteTurnInfo, _blackturnInfo;

    [SerializeField]
    private EventSystem _eventSystem;

    private void Awake()
    {
        _pauseButton.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
        _bg.gameObject.SetActive(false);
        _eventSystem.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
        }

        if (Time.timeScale == 0)
        {
            _pauseButton.gameObject.SetActive(true);
            _mainMenuButton.gameObject.SetActive(true);
            _quitButton.gameObject.SetActive(true);
            _bg.gameObject.SetActive(true);
            _eventSystem.enabled = true;
        }
    }
    public void Resume()
    {
        _pauseButton.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
        _bg.gameObject.SetActive(false);
        _eventSystem.enabled = false;
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        Debug.Log($"mainmenuload");
        Invoke("MainMenuLoad", 0.2f);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void MainMenuLoad()
    {
        _eventSystem.enabled = false;
        SceneManager.LoadScene("MainMenu");
    }
}
