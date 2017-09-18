using UnityEngine;

public class DifficultyIncrementer : MonoBehaviour {

    public float multiplier;
    public float incrementInterval;
    public PlayerController pc;

    private float nextIncrementTime;

    public void Reset() {
        Debug.Log("Reseting difficulty");
        Globals.moveUpSpeed = Globals.StartMoveUpSpeed;
        nextIncrementTime = Time.time + incrementInterval;
    }

    private void Start() {
        pc.OnPlayerSpawn.AddListener(Reset);
    }

    private void Update() {
       if(Globals.moveUpSpeed < Globals.MaxMoveUpSpeed && pc.alive && Time.time > nextIncrementTime) {
            Globals.moveUpSpeed *= multiplier;
            Debug.Log("Move up speed: " + Globals.moveUpSpeed.ToString());
            nextIncrementTime = Time.time + incrementInterval;
        }
    }
}
