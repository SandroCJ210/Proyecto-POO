using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Camera gameCamera;
    public Vector3 directionToChangeRoom;
    public float lerpDuration;
    private Vector3 gameCameraVector;
    private enum Directions { Up, Down, Left, Right };
    [SerializeField] private Directions direction;
    [SerializeField] private AnimationCurve curve;

    void Start()
    {
        gameCamera = Camera.main;
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
            Vector3 actualPosition = gameCamera.transform.position;
            Vector3 targetPosition = actualPosition + Vector3.Scale(directionToChangeRoom, gameCameraVector);
            StartCoroutine(MoveCamera(actualPosition, targetPosition, lerpDuration));
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
