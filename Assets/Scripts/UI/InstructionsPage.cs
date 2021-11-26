using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InstructionsPage : MonoBehaviour
{
    [SerializeField]
    private GameObject _hoverSound;

    [SerializeField]
    private EventSystem _eventSystem;

    [SerializeField]
    private GameObject _GoBack, _bg1, _bg2, _nextButton;

    private void Awake()
    {
        _GoBack.gameObject.SetActive(false);
        _bg2.gameObject.SetActive(false);
        _bg1.gameObject.SetActive(true);
        _nextButton.gameObject.SetActive(true);
    }

    public void Next()
    {
        _GoBack.gameObject.SetActive(true);
        _bg2.gameObject.SetActive(true);
        _bg1.gameObject.SetActive(false);
        _nextButton.gameObject.SetActive(false);
    }
    public void GoBack()
    {
        _eventSystem.enabled = false;
        Invoke("GoBackLoad", 0.2f);
    }

    public void hover()
    {
        Instantiate(_hoverSound, transform.position, Quaternion.identity);
    }

    private void GoBackLoad()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
