using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipHolder : MonoBehaviour
{
    #region Variables
    public GameObject rocketHolder;
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rocketHolder.SetActive(true);
        }
    }

}// main class
