using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public int vertices;
    
    public int maxHp;
    
    public int damage;
    public int speed;
    public int shotSpeed;

}

