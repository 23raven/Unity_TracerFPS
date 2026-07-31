using UnityEngine;

public class BotAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int Shoot =
        Animator.StringToHash("Shoot");

    public void PlayShoot()
    {
        animator.SetTrigger(Shoot);
    }
}