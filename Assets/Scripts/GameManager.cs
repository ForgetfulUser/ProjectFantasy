using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager PlayerManager;

    void Start()
    {
        PlayerManager.StartManager();
    }

    void Update()
    {
        PlayerManager.UpdateManager();
    }

    private void FixedUpdate()
    {
        PlayerManager.FixedUpdateManager();
    }
}
