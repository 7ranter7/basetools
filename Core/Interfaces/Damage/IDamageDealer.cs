using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RanterTools.Base
{
    /// <summary>
    /// Damage dealer interface
    /// </summary>
    public interface IDamageDealer
    {
        #region Events 
        /// <summary>
        /// Damage dealed event
        /// </summary>  
        event DamageReceivedDelegate DamageDealedEvent;
        #endregion Events
        #region Parameters
        /// <summary>
        /// Damage or amount
        /// </summary>
        /// <value>Damage or amount</value>
        float Damage { get; set; }
        #endregion Parameters
        #region Methods
        /// <summary>
        /// Deal damage
        /// </summary>
        /// <param name="receiver">Damage receiver</param>
        void DealDamage(IDamageReceiver damageReceiver);
        #endregion Methods
    }
}