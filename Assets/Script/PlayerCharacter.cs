using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public GameObject closeAttackPrefab;
    public GameObject rangeAttackPrefab;
    public GameObject versusAttackPrefab;

    public Character character; // On definie dans unity

    // Blue capacity
    public GameObject blueBullet;
    public int blueDamageBullet;
    public float blueAccuracyBullet;
    public float blueSpeedBullet;
    public float blueIntervalSpeedBullet;
    public int blueCountBullet;

}
