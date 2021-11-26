using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    [SerializeField]
    private float _timer = 0.2f;

    private void Awake()
    {
        Destroy(gameObject, _timer);
    }
}
