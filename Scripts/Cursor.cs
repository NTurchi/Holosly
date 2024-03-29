﻿// <copyright file="Cursor.cs">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>TURCHI Nicolas</author>
// <date>04/27/2017 15:00 AM </date>

using UnityEngine;

namespace Holosly
{
    /// <summary>
    /// Classe gérant le curseur au sein de l'application
    /// </summary>
    public class Cursor : MonoBehaviour, ICursor
    {
        /// <summary>
        /// Représentation visuelle du curseur visant les hologrammes
        /// </summary>
        private MeshRenderer _cursorOnHologramMesh;

        /// <summary>
        /// Lumière correspondant au curseur qui s'affiche lorsqu'aucun Hologramme n'est touché
        /// </summary>
        private Light _cursorOutOfHologramLight;

        void Start()
        {
            // Récupération des deux types de curseur (Sur Hologrammes et en dehos des Hologrammes)
            _cursorOnHologramMesh = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();

            _cursorOutOfHologramLight = transform.GetChild(1).gameObject.GetComponent<Light>();
        }

        public void OnGameObjectFocused(Vector3 normalVector, Vector3 position)
        {
            // On désactive le curseur qui est affiché lorsqu'un gameObject n'est pas visé
            _cursorOutOfHologramLight.enabled = false;

            // On affiche le curseur qui se pose sur les Hologrammes lorsque ceux-ci sont visé
            _cursorOnHologramMesh.enabled = true;

            // On positionne le curseur sur l'écran de l'utilisateur
            transform.position = new Vector3(position.x, position.y, position.z - 0.0001f);

            // On effectue une rotation du curseur afin qu'il soit bien surperposé sur le gameObject
            transform.rotation = Quaternion.LookRotation(normalVector);
        }

        public void OnGameObjectUnfocused(Vector3 position)
        {
            // Aucun hologramme n'est visé, donc nous affichons le curseur de base
            _cursorOutOfHologramLight.enabled = true;
            _cursorOnHologramMesh.enabled = false;

            // On positionne le curseur à l'écran
            transform.position = position;
        }
    }
}
