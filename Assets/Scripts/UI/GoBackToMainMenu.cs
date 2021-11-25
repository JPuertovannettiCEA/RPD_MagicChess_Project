using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoBackToMainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _hoverSound;

    [SerializeField]
    private EventSystem _eventSystem;

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
