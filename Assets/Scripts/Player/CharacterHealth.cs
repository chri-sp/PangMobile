using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        int balloonLayer = LayerMask.NameToLayer("Balloon");
        if (collision.gameObject.layer == balloonLayer)
        {
            PlayerHit();
        }
    }

    void PlayerHit()
    {
        GameManager.Instance.GameOver();
    }
}
