using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockEffect : MonoBehaviour
{
    [SerializeField]
    private Vector3 _directionToFly;

    [SerializeField]
    private float _flyDevider;

    [SerializeField]
    private GameObject _winParticles;

    private string _animWinName = "EndOfLevel";
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            MainCharacterController mainCharacterController = collision.gameObject.GetComponent<MainCharacterController>();
            mainCharacterController._canRun = false;
            mainCharacterController._animator.SetBool("CanRun", mainCharacterController._canRun);
            mainCharacterController._animator.SetBool(_animWinName, true);
            StartCoroutine(EndLevelEffect(mainCharacterController));
        }
        else if (collision.gameObject.CompareTag("WinLine"))
        {
            Instantiate(_winParticles,gameObject.transform.position,_winParticles.transform.rotation, gameObject.transform);
        }
    }

    IEnumerator EndLevelEffect(MainCharacterController player)
    {
        yield return new WaitForSeconds(1.65f);
        int multiplier = player._straightLevel;
        if (player._straightLevel <= 25)
            multiplier = 26;
        GetComponent<Rigidbody>().AddForce(_directionToFly * multiplier / 30);
        Camera.main.GetComponent<TrackTarget>().ChangeTargetToTrack(gameObject);
    }
}
