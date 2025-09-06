using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects")]
public class EnemyData : ScriptableObject
{
    public int maxHP = 5;

    [Header("Forward")]
    public float speed = 10;
    [Header("Spinning")]
    public float angleSpeed = 1;
    [Header("Back to center")]
    public float backAngleSpeed = 1;
    [Header("AttackSpeed")]
    public float attackSpeed = 15;
    public float minAttackDistance = 2.0f;

    public float bulletSpeed = 10.0f;
    
    [Header("State Duration")]
    public float minStateDuration = 2.0f;
    public float maxStateDuaration = 5.0f;
}
