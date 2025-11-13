using UnityEngine;

//NOTES: Cette interface definit les comportements de base que devrait avoir un personnage dans le jeu.
//Cependant, dépendamment du type de personnage (joueur, ennemi, PNJ, etc.), les implémentations de ces comportements peuvent varier.
//C'est une obligation d'implémenter l'interface pour chaque type de personnage différent.

public interface IPersonnage
{
    public void Die();
    public void Idle();
    public void TakeDamage(int damageAmount);
}
