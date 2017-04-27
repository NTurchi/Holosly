using Holosly.EventData;
using System;
using System.Collections;
using UnityEngine;
using HoldEventData = Holosly.EventData.HoldEventData;
using ManipulationEventData = Holosly.EventData.ManipulationEventData;
using Random = UnityEngine.Random;

/// <summary>
/// Script de test des événements sur un GameObject
/// </summary>
public class TapAndHold : MonoBehaviour
{
    private bool _isMoving;


    void Update()
    {
        if (!_isMoving)
        {
            return;
        }

        transform.position = Camera.main.transform.position + Camera.main.transform.forward * Vector3.Distance(Camera.main.transform.position, transform.position);
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
    }

    /// <summary>
    /// Quand l'objet se fait cliquer dessus
    /// </summary>
    void OnTapped(TappedEventData eventData)
    {
        _isMoving = !_isMoving;
    }

    void OnHoldStart(HoldEventData eventData)
    {
        StartCoroutine(Wait2Second());
    }

    void OnHoldCancel(HoldEventData eventData)
    {
        StopAllCoroutines();
    }

    void OnHoldComplete(HoldEventData eventData)
    {
        StopAllCoroutines();
        //GetComponent<Renderer>().material.color = Color.yellow;
    }

    void OnManipulationStart(ManipulationEventData eventData)
    {
        GetComponent<Renderer>().material.color = Color.green;
        Destroy(gameObject);
    }

    void OnManipulationUpdate(ManipulationEventData eventData)
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnManipulationComplete(ManipulationEventData eventData)
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }


    IEnumerator Wait2Second()
    {
        while (true)
        {
            byte r = Convert.ToByte(Random.Range(0, 255));
            byte g = Convert.ToByte(Random.Range(0, 255));
            byte b = Convert.ToByte(Random.Range(0, 255));

            GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);

            yield return new WaitForSeconds(1);
        }
    }
}
