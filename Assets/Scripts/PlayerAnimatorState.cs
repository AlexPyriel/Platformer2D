using UnityEngine;

public static class PlayerAnimatorState
{
    private static int _dead = Animator.StringToHash("isDead");
    private static int _win = Animator.StringToHash("hasWon");
    private static int _spawn = Animator.StringToHash("justSpawned");
    private static int _start = Animator.StringToHash("gameStarted");
    private static int _walk = Animator.StringToHash("isWalking");
    private static int _jump = Animator.StringToHash("isJumping");

    public static int Dead => _dead;
    public static int Win => _win;
    public static int Spawn => _spawn;
    public static int Start => _start;
    public static int Walk => _walk;
    public static int Jump => _jump;
}
