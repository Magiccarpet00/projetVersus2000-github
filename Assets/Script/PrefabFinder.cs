using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public enum Ressources
{
    Hat,
    Ghost,
    Explosion,
    Detection_player,
    Ghost_bullet,
    Pentagrame,
    Bubble,
    RangeAttackRetrit
}
public static class PrefabFinder
{
    public static Dictionary<Ressources, string> RessourcesToURI; 

    public static void Init()
    {
        RessourcesToURI = new Dictionary<Ressources, string> ();
        RessourcesToURI[Ressources.Hat] = "sousdossier/hat";
        RessourcesToURI[Ressources.Ghost] = "pti_fantom"; // *regard désapprobateur* pour ce nom
        RessourcesToURI[Ressources.Explosion] = "explosion";
        RessourcesToURI[Ressources.Detection_player] = "detecteur_player";
        RessourcesToURI[Ressources.Ghost_bullet] = "fantomball";
        RessourcesToURI[Ressources.Pentagrame] = "invocation_fantom";
        RessourcesToURI[Ressources.Bubble] = "bubule";
        RessourcesToURI[Ressources.RangeAttackRetrit] = "range_attaque_retrit";
    }


}