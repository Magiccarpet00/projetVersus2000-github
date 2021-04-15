using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public enum Ressources
{
    Hat,
    Ghost,
    Explosion,

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
    }


}