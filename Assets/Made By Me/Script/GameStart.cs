using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] WayPoint wayPoint;
    void Start()
    {
        FindObjectOfType<PlayerHealth>().gameObject.transform.position = new Vector3(wayPoint.x, wayPoint.y, 0);
        SceneManager.LoadScene(1);
    }

}
