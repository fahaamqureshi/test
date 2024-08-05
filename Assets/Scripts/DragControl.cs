using UnityEngine;

public class DragControl : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;

    private Vector2 startTouch;
    private bool isDragging = false;

    private void Update()
    {
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion

        // Calculate the distance
        if (isDragging)
        {
            Vector2 currentTouch = Input.touches.Length > 0 ? (Vector2)Input.touches[0].position : (Vector2)Input.mousePosition;
            Vector2 swipeDelta = currentTouch - startTouch;

            // Move the player horizontally based on swipe delta
            Vector3 move = new Vector3(swipeDelta.x * Time.deltaTime * moveSpeed, 0, 0);
            player.Translate(move, Space.World);

            // Clamp the player's position within the boundaries
            Vector3 clampedPosition = player.position;
            clampedPosition.x = Mathf.Clamp(player.position.x, leftBoundary, rightBoundary);
            player.position = clampedPosition;

            // Update startTouch to the current position to ensure continuous dragging
            startTouch = currentTouch;
        }
    }

    private void Reset()
    {
        startTouch = Vector2.zero;
    }
}
