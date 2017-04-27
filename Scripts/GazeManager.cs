// <copyright file="GazeManager.cs">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>TURCHI Nicolas</author>
// <date>04/27/2017 15:00 AM </date>

using System;
using UnityEngine;

namespace Holosly
{
    /// <summary>
    /// Classe pour la gestion du regard de l'utilisateur
    /// </summary>
    public class GazeManager : Singleton<GazeManager>
    {
        /// <summary>
        /// Rayon se dirigeant là où la tête de l'utilisateur est dirigée
        /// </summary>
        public Ray UserHeadRay { get; set; }

        /// <summary>
        /// Distance entre la camera et le curseur pour l'affichage de celui-ci lorsqu'un gameObject n'est pas visé par le regard de l'utilisateur
        /// </summary>
        [Tooltip("Distance entre la camera et le curseur pour l\'affichage de celui-ci lorsqu\'un gameObject n\'est pas visé par le regard de l\'utilisateur")]
        public float DistanceShowCursor = 5f;

        /// <summary>
        /// Le curseur de l'application
        /// </summary>
        [Tooltip("Le curseur de l\'application")]
        public GameObject CursorGameObject;

        /// <summary>
        /// Script du curseur
        /// </summary>
        private ICursor _cursor;

        /// <summary>
        /// Le GameObject actuellement focus par le regard de l'utilisateur 
        /// </summary>
        public GameObject GameObjectFocused;


        void Start()
        {
            if (CursorGameObject == null)
            {
                Debug.LogError("Veuillez insérer un curseur dans l'application");
            }

            try
            {
                _cursor = CursorGameObject.GetComponent<ICursor>();
            }
            catch (Exception)
            {
                Debug.LogError("Erreur, le curseur n'a pas de composant implémentant l'interface ICursor");
            }

        }

        void Update()
        {
            if (_cursor == null)
            {
                Debug.LogError("Le curseur est null");
                return;
            }

            // Nous récupérons la direction vers laquelle est tournée le regard de l'utilisateur
            Vector3 userHeadDirection = Camera.main.transform.forward;

            // Nous récupérons la position actuelle de la tête de l'utilisateur dans l'espace, c'est la source du rayon
            Vector3 userHeadOrigin = Camera.main.transform.position;

            // Nous créeons le rayon à partir des deux valeurs définis ci-dessus
            UserHeadRay = new Ray(userHeadOrigin, userHeadDirection);

            // Les informations que l'on va récupérer sur l'objet touché par le rayon
            RaycastHit hitInfo;

            // Nous testons si le rayon touche un GameObject à une distance maximum de 10 mêtres
            if (Physics.Raycast(UserHeadRay, out hitInfo, maxDistance: 10f))
            {
                // On enregistre le Gameobject touché par le rayon provenant du regard de l'utilisateur
                this.GameObjectFocused = hitInfo.transform.gameObject;

                // Vecteur normal du gameObject focus (Vecteur normal = Vecteur perpendiculaire à la surface de l'objet)
                Vector3 normalVector = hitInfo.normal;

                // Position du Gameobject dans l'espace
                Vector3 position = hitInfo.point;

                // On notifie le curseur de ces changements
                _cursor.OnGameObjectFocused(normalVector, position);
            }
            else // Aucun GameObject n'est touché
            {
                // On set le gameObject actuellement visé à null
                this.GameObjectFocused = null;

                // On notifie le curseur de ces changements
                _cursor.OnGameObjectUnfocused(userHeadOrigin + userHeadDirection * DistanceShowCursor);
            }
        }
    }
}
