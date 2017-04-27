// <copyright file="TappedEventData.cs">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>TURCHI Nicolas</author>
// <date>04/27/2017 15:00 AM </date>
namespace Holosly.EventData
{
    /// <summary>
    /// Objet qui sera passé en paramètre d'un événement "Tap" soit un clique utilisateur sur Hololens
    /// </summary>
    public class TappedEventData : BaseEventData
    {
        /// <summary>
        /// Le nombre de 'Tap' effectués
        /// </summary>
        public int TapCount { get; set; }
    }
}

