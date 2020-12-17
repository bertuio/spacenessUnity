using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Create a custom struct and apply [Serializable] attribute to it
    [Serializable]
    public struct PlayerStats
    {
        public int movementSpeed;
        public int hitPoints;
        public bool hasHealthPotion;
    }

    //Make the private field of our PlayerStats struct visible in the Inspector
    //by applying [SerializeField] attribute to it
    [SerializeField]
    private PlayerStats stats;
}