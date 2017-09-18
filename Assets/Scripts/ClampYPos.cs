using UnityEngine;

public class ClampYPos : MonoBehaviour {

    private void Update() {
        if (transform.position.y < -Globals.maxYPos || transform.position.y > Globals.maxYPos) {
            if (tag == "Player") {
                GetComponent<PlayerController>().Die();
            } else {
                Destroy(gameObject);
            }
        }
    }
}
