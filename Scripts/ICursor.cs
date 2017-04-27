using UnityEngine;

namespace Holosly
{
    public interface ICursor
    {
        /// <summary>
        /// Méthode qui sera déclenchée lorsque le regard de l'utilisateur visera un GameObject
        /// </summary>
        /// <param name="normalVector">Le vecteur normal (vecteur parallèle à la surface de l'objet) du GameObject</param>
        /// <param name="position">La position du rayon du regard dans l'espace de l'utilisateur</param>
        void OnGameObjectFocused(Vector3 normalVector, Vector3 position);

        /// <summary>
        /// Lorsque qu'un objet n'est pas viser par le regard de l'utilisateur, il est juste nécessaire d'afficher
        /// le curseur normal
        /// </summary>
        /// <param name="position">La position du rayon du regard dans l'espace de l'utilisateur</param>
        void OnGameObjectUnfocused(Vector3 position);
    }
}
