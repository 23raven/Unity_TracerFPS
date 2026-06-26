using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerManager playerManager;

    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;
    }

    private void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {

    }
}