using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Camera gameCamera;
    private Player player;
    private static float lerpDuration;
    private Vector3 gameCameraVector;
    private AnimationCurve curve;

    void Start()
    {
        gameCamera = Camera.main;
        player = FindAnyObjectByType<Player>();
        float gameCameraHeight = 2 * gameCamera.orthographicSize;
        float gameCameraWidth = gameCameraHeight * gameCamera.aspect;

        gameCameraVector = new Vector3(gameCameraWidth, gameCameraHeight, gameCamera.transform.position.z);
        lerpDuration = 0.5f;
        curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        curve.preWrapMode = WrapMode.Clamp;
        curve.postWrapMode = WrapMode.Clamp;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            MovePlayerToRoom();
            Vector3 actualCamPosition = gameCamera.transform.position;
            Vector3 targetCamPosition = actualCamPosition + Vector3.Scale(player.directionToMove, gameCameraVector);
            StartCoroutine(MoveCamera(actualCamPosition, targetCamPosition, lerpDuration));
        }
    }
    private void MovePlayerToRoom()
    {
        Vector3 actualPlayerPosition = player.transform.position;
        Vector3 advance = new Vector3(1.4f, 1.4f, actualPlayerPosition.z);
        Vector3 targetPosition = actualPlayerPosition + Vector3.Scale(player.directionToMove, advance);
        player.transform.position = targetPosition;        
    }
    private IEnumerator MoveCamera(Vector3 start, Vector3 target, float lerpDuration)
    {
        float elapsedTime = 0;

        while (elapsedTime < lerpDuration)
        {
            gameCamera.transform.position = Vector3.Lerp(start, target, curve.Evaluate(elapsedTime / lerpDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameCamera.transform.position = target;

    }

}
