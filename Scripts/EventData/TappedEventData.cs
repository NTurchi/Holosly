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

