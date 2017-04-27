using UnityEngine;

namespace Holosly
{
    /// <summary>
    /// Classe Singleton pour les scripts nécessitant une seule instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {

        private static T _instance;
        public static T Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Retourne vrai si l'instance est initialisée
        /// </summary>
        public static bool IsInitialized
        {
            get
            {
                return _instance != null;
            }
        }

        /// <summary>
        /// Awake Method qui initialise le singleton
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Debug.LogErrorFormat("Vous essayer de recréer une instance de {0}", GetType().Name);
            }
            else
            {
                _instance = (T)this;
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }

}


