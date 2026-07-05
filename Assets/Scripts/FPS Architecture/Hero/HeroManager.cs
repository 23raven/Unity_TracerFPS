using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private HeroDefinition currentHero;

    public HeroDefinition CurrentHero => currentHero;
}