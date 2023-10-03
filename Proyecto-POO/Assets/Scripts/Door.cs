using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Camera gameCamera;
    GameObject player;
    public Vector3 directionToChangeRoom;
    public float lerpDuration;
    private Vector3 gameCameraVector;
    private enum Directions { Up, Down, Left, Right };
    [SerializeField] private Directions direction;
    [SerializeField] private AnimationCurve curve;

    void Start()
    {
        gameCamera = Camera.main;
        player = GameObject.Find("Player");
        float gameCameraHeight = 2 * gameCamera.orthographicSize;
        float gameCameraWidth = gameCameraHeight * gameCamera.aspect;
        gameCameraVector = new Vector3(gameCameraWidth, gameCameraHeight, gameCamera.transform.position.z);
        lerpDuration = 0.5f;
    }

    void Update()
    {
        switch (direction)
        {
            case Directions.Up:
                directionToChangeRoom = Vector3.up;
                break;
            case Directions.Down:
                directionToChangeRoom = Vector3.down;
                break;
            case Directions.Left:
                directionToChangeRoom = Vector3.left;
                break;
            case Directions.Right:
                directionToChangeRoom = Vector3.right;
                break;
            default:
                directionToChangeRoom = Vector3.left;
                break;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            MovePlayerToRoom();
            Vector3 actualCamPosition = gameCamera.transform.position;
            Vector3 targetCamPosition = actualCamPosition + Vector3.Scale(directionToChangeRoom, gameCameraVector);
            StartCoroutine(MoveCamera(actualCamPosition, targetCamPosition, lerpDuration));
            switch (direction)
            {
                case Directions.Up:
                    direction = Directions.Down;
                    break;
                case Directions.Down:
                    direction = Directions.Up;
                    break;
                case Directions.Left:
                    direction = Directions.Right;
                    break;
                case Directions.Right:
                    direction = Directions.Left;
                    break;
            }
        }
    }

    void MovePlayerToRoom()
    {
        Vector3 actualPlayerPosition = player.transform.position;
        Vector3 advance = new Vector3(3,3,actualPlayerPosition.z);
        Vector3 targetPosition = actualPlayerPosition + Vector3.Scale(directionToChangeRoom, advance);
        player.transform.position = targetPosition;
    }
    IEnumerator MoveCamera(Vector3 start, Vector3 target, float lerpDuration)
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
