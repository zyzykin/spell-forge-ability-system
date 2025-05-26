using UnityEngine;
using UnityEngine.Serialization;

namespace SpellForge.Scripts.AbilitySystem
{
    [CreateAssetMenu(menuName="AbilitySystem/Stat")]
    public class Stat : ScriptableObject { public float Value; }
}