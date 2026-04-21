using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerController PlayerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartManager()
    {
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PlayerController.StartController();
    }

    // Update is called once per frame
    public void UpdateManager()
    {
        PlayerController.UpdateController();
    }

    public void FixedUpdateManager()
    {
        PlayerController.FixedUpdateController();
    }
}
