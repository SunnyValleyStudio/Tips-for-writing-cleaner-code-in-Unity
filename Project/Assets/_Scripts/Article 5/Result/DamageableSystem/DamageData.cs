using UnityEngine;

namespace Tips.Part_5_End
{
    /// <summary>
    /// Data object that holds information about the damage that was dealt.
    /// This make it easy to pass more parameters without having to modify the
    /// definition of a TakeDamage(...) method from the IDamagable interface 
    /// </summary>
    public class DamageData
    {
        public GameObject Sender { get; set; }
        public int DamageAmount { get; set; }
    }
}