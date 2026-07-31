using UnityEngine;

public class BotAnimationEvents : MonoBehaviour
{
    [SerializeField] private ShootingBot shootingBot;

    public void SpawnBullet()
    {
        shootingBot.SpawnBullet();
    }
}