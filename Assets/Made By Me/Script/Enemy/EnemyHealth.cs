using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {

        GameObject CollideObject = other.GetContact(0).collider.gameObject;


        if(CollideObject.tag == "Attack")
        {

        }
        else if(CollideObject.tag == "Sheild")
        {

        }
        else if(CollideObject.tag == "Player Body")
        {

        }
    }
}
