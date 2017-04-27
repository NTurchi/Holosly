// <copyright file="BaseEventData.cs">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>TURCHI Nicolas</author>
// <date>04/27/2017 15:00 AM </date>

using UnityEngine;
using UnityEngine.VR.WSA.Input;


namespace Holosly.EventData
{
    /// <summary>
    /// Classe de base pour les données passant en paramètre des évenements
    /// </summary>
    public class BaseEventData : MonoBehaviour
    {
        /// <summary>
        /// Source de l'évenement. Cela peut être les mains comme la voix etc...
        /// </summary>
        public InteractionSourceKind Source { get; set; }

        /// <summary>
        /// Rayon venant du regard de l'utilisateur
        /// </summary>
        public Ray HeadRay { get; set; }

    }
}

