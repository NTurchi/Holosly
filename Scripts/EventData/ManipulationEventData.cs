// <copyright file="ManipulationEventData.cs">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>TURCHI Nicolas</author>
// <date>04/27/2017 15:00 AM </date>

using UnityEngine;

namespace Holosly.EventData
{
    /// <summary>
    /// Objet qui sera passé en paramètre d'un événement "Manipulation" soit un clique long utilisateur sur Hololens
    /// </summary>
    public class ManipulationEventData : BaseEventData
    {
        /// <summary>
        /// Distance parcourue entre le début de la manipulation et son statut actuel (Ex: Position de la main)
        /// </summary>
        public Vector3 CumulativeDelta { get; set; }
    }
}
