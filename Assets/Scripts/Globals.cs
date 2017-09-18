using UnityEngine;

public static class Globals {
    public static readonly float StartMoveUpSpeed = 5f;
    public static float moveUpSpeed;
    public static readonly float MaxMoveUpSpeed = 7f;
    public static float maxXPos = Camera.main.orthographicSize * Camera.main.aspect;
    public static float maxYPos = Camera.main.orthographicSize;
}
