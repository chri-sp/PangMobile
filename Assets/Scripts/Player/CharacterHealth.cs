using System.Collections;
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
        Death();
    }

    void Death()
    {
        GameManager.Instance.GameOver();
    }
}