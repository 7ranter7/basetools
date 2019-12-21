using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RanterTools.Base
{
    /// <summary>
    /// Healing dealer interface
    /// </summary>
    public interface IHealingDealer
    {
        #region Events
        /// <summary>
        /// Healing dealed event
        /// </summary>  
        event DamageReceivedDelegate HealingDealedEvent;
        #endregion Events
        #region Parameters
        /// <summary>
        /// Amount of healing
        /// </summary>
        /// <value>Amount of heling</value>
        float AmountOfHealing { get; set; }
        #endregion Parameters
        #region Methods
        /// <summary>
        /// Heal a damage receiver
        /// </summary>
        /// <param name="damageReceiver">Damage receiver</param>
        void Heal(IDamageReceiver damageReceiver);
        #endregion Methods
    }
}