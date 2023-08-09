using System;
using DG.Tweening;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Vector3 ballPositionOffset = new(0, 0.4f, 0);
    [SerializeField] private float speed;

    private Vector2 mouseStartPosition;
    private bool canMove = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            mouseStartPosition = Input.mousePosition;

        else if (Input.GetMouseButtonUp(0))
        {
            var mouseEndPosition = Input.mousePosition;

            Move(GetSwipeDirection(mouseStartPosition, mouseEndPosition));
        }
    }

    private void Move(Vector3 direction)
    {
        var ballRay = new Ray(transform.position, direction);

        if (Physics.Raycast(ballRay, out var ballHit))
        {
            if (ballHit.collider.CompareTag("Wall"))
            {
                var targetPos = ballHit.transform.position + -1 * direction + ballPositionOffset;
                if (canMove)
                {
                    canMove = false;
                    transform.DOMove(targetPos, 1 / speed).OnComplete(() => { canMove = true; });
                }
            }
        }
    }

    private static Vector3 GetSwipeDirection(Vector2 startPos, Vector2 endPos)
    {
        var swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

        if (Math.Abs(swipe.x) > Math.Abs(swipe.y))
            return swipe.x > 0 ? Vector3.right : Vector3.left;
        else
            return swipe.y > 0 ? Vector3.forward : Vector3.back;
    }
}