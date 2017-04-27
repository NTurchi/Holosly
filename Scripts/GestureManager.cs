// <copyright file="GestureManager.cs">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>TURCHI Nicolas</author>
// <date>04/27/2017 15:00 AM </date>

using Holosly.EventData;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace Holosly
{
    /// <summary>
    /// Classe permettant la gestion des gestes sur les Hololens
    /// </summary>
    public class GestureManager : Singleton<GestureManager>
    {
        /// <summary>
        /// Le dernir GameObject Focus
        /// </summary>
        private GameObject _oldGameObject;


        /// <summary>
        /// Le detecteur de geste
        /// </summary>
        private GestureRecognizer _recognizer;

        /// <summary>
        /// Le container de nos hologrammes au sein de notre application
        /// </summary>
        public GameObject HologramsContainer;

        protected override void Awake()
        {
            base.Awake();

            // Le detecteur de geste
            _recognizer = new GestureRecognizer();

            // On initialise chaque évenement
            _recognizer.TappedEvent += OnTappedEventHandler;
            _recognizer.HoldStartedEvent += OnHoldStartedEventHandler;
            _recognizer.HoldCompletedEvent += OnHoldCompletedEventHandler;
            _recognizer.HoldCanceledEvent += OnHoldCanceledEventHandler;
            _recognizer.ManipulationStartedEvent += OnManipulationStartedEventHandler;
            _recognizer.ManipulationUpdatedEvent += OnManipulationUpdatedEventHandler;
            _recognizer.ManipulationCompletedEvent += OnManipulationCompletedEventHandler;



            // On lance le listener
            _recognizer.StartCapturingGestures();

            // On récpère le conteneur des hologrammes de notres application
            HologramsContainer = GameObject.Find("Holograms");
        }

        void Update()
        {
            // Si l'objet étant focus à changé alors on relance la détéction des mouvements de l'utilisateur
            if (GazeManager.Instance.GameObjectFocused != _oldGameObject)
            {
                _recognizer.StopCapturingGestures();
                _oldGameObject = GazeManager.Instance.GameObjectFocused;
                _recognizer.StartCapturingGestures();
            }

            if (Input.anyKeyDown)
            {
                // Pour tester sur unity directement au lieu de déployer toujours sur hololens
            }
        }

        #region CreateCube

        /// <summary>
        /// Méthode pour la création d'un cube au sein de notre espace 3D 
        /// </summary>
        private void CreateCube()
        {
            float distance = Vector3.Distance(GazeManager.Instance.UserHeadRay.origin, HologramsContainer.transform.position);



            GameObject go = Instantiate(GameObject.Find("Cube"), HologramsContainer.transform);

            go.transform.position = GazeManager.Instance.UserHeadRay.origin +
                                    GazeManager.Instance.UserHeadRay.direction *
                                    distance;
            // On tourne l'object de façon à ce qu'il nous regarde
            go.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);

            // On lui ajoute le script avec les différentes méthodes réagissant aux événements déclenché par les geste de l'utilisateur
            go.AddComponent<TapAndHold>();
        }


        #endregion

        #region TappedEvent


        /// <summary>
        /// Lorsque l'utilisateur clique sur un GameObject
        /// </summary>
        /// <param name="source">La source de l'événement (Les mains, la voix etc...)</param>
        /// <param name="tapCount">Le nombre de tap effectué</param>
        /// <param name="ray">Le rayon du tap (d'où il vient)</param>
        private void OnTappedEventHandler(InteractionSourceKind source, int tapCount, Ray ray)
        {
            if (GazeManager.Instance.GameObjectFocused != null)
            {
                TappedEventData eventData = new TappedEventData()
                {
                    Source = source,
                    TapCount = tapCount,
                    HeadRay = ray
                };
                GazeManager.Instance.GameObjectFocused.SendMessageUpwards("OnTapped", eventData);
            }
            else
            {
                CreateCube();
            }
        }

        #endregion

        #region HoldEvent

        /// <summary>
        /// Lorsque l'utilisateur commence un appuie long sur un objectgameObject
        /// </summary>
        /// <param name="source">La source de l'événement (Les mains, la voix etc...)</param>
        /// <param name="ray">Le rayon partant du regard de l'utilisateur</param>
        private void OnHoldStartedEventHandler(InteractionSourceKind source, Ray ray)
        {
            if (GazeManager.Instance.GameObjectFocused != null)
            {
                HoldEventData eventData = new HoldEventData()
                {
                    Source = source,
                    HeadRay = ray
                };
                GazeManager.Instance.GameObjectFocused.SendMessageUpwards("OnHoldStart", eventData);
            }
        }

        /// <summary>
        /// Lorsque l'utilisateur termine un appuie long sur un gameObject
        /// </summary>
        /// <param name="source">La source de l'événement (Les mains, la voix etc...)</param>
        /// <param name="ray">Le rayon partant du regard de l'utilisateur</param>
        private void OnHoldCompletedEventHandler(InteractionSourceKind source, Ray ray)
        {
            if (GazeManager.Instance.GameObjectFocused != null)
            {
                HoldEventData eventData = new HoldEventData()
                {
                    Source = source,
                    HeadRay = ray
                };
                GazeManager.Instance.GameObjectFocused.SendMessageUpwards("OnHoldComplete", eventData);
            }
        }

        /// <summary>
        /// Lorsque l'utilisateur annule un appuie long sur un gameObject
        /// </summary>
        /// <param name="source">La source de l'événement (Les mains, la voix etc...)</param>
        /// <param name="ray">Le rayon partant du regard de l'utilisateur</param>
        private void OnHoldCanceledEventHandler(InteractionSourceKind source, Ray ray)
        {
            if (GazeManager.Instance.GameObjectFocused != null)
            {
                HoldEventData eventData = new HoldEventData()
                {
                    Source = source,
                    HeadRay = ray
                };
                GazeManager.Instance.GameObjectFocused.SendMessageUpwards("OnHoldCancel", eventData);
            }
        }

        #endregion

        #region ManipulationEvent

        /// <summary>
        /// Lorsque l'utilisateur commence un geste de manipulation sur un gameObject
        /// </summary>
        /// <param name="source">La source de l'événement (Les mains, la voix etc...)</param>
        /// <param name="cumulativeDelta">Distance totale parcouru depuis le début de l'évenement</param>
        /// <param name="ray">La rayon venant du regard de l'utilisateur</param>
        private void OnManipulationStartedEventHandler(InteractionSourceKind source, Vector3 cumulativeDelta, Ray ray)
        {
            if (GazeManager.Instance.GameObjectFocused != null)
            {
                ManipulationEventData eventData = new ManipulationEventData()
                {
                    Source = source,
                    CumulativeDelta = cumulativeDelta,
                    HeadRay = ray
                };
                GazeManager.Instance.GameObjectFocused.SendMessageUpwards("OnManipulationStart", eventData);
            }
        }

        /// <summary>
        /// Lorsque l'utilisateur continue un geste de manipulation sur un gameObject
        /// </summary>
        /// <param name="source">La source de l'événement (Les mains, la voix etc...)</param>
        /// <param name="cumulativeDelta">Distance totale parcouru depuis le début de l'évenement</param>
        /// <param name="ray">La rayon venant du regard de l'utilisateur</param>
        private void OnManipulationUpdatedEventHandler(InteractionSourceKind source, Vector3 cumulativeDelta, Ray ray)
        {
            if (GazeManager.Instance.GameObjectFocused != null)
            {
                ManipulationEventData eventData = new ManipulationEventData()
                {
                    Source = source,
                    CumulativeDelta = cumulativeDelta,
                    HeadRay = ray
                };
                GazeManager.Instance.GameObjectFocused.SendMessageUpwards("OnManipulationUpdate", eventData);
            }
        }

        /// <summary>
        /// Lorsque l'utilisateur termine son geste de manipulation sur un gameObject
        /// </summary>
        /// <param name="source">La source de l'événement (Les mains, la voix etc...)</param>
        /// <param name="cumulativeDelta">Distance totale parcouru depuis le début de l'évenement</param>
        /// <param name="ray">La rayon venant du regard de l'utilisateur</param>
        private void OnManipulationCompletedEventHandler(InteractionSourceKind source, Vector3 cumulativeDelta, Ray ray)
        {
            if (GazeManager.Instance.GameObjectFocused != null)
            {
                ManipulationEventData eventData = new ManipulationEventData()
                {
                    Source = source,
                    CumulativeDelta = cumulativeDelta,
                    HeadRay = ray
                };
                GazeManager.Instance.GameObjectFocused.SendMessageUpwards("OnManipulationComplete", eventData);
            }
        }


        #endregion
    }
}
