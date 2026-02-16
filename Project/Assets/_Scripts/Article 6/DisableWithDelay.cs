using UnityEngine;

public class DisableWithDelay : MonoBehaviour
{
    [SerializeField]
    private float m_delay = 5;
    void Start()
    {
        Invoke("Disable", m_delay);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
