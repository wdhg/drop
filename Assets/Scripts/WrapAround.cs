using UnityEngine;

public class WrapAround : MonoBehaviour {

    private void Wrap() {
        if (transform.position.x < -Globals.maxXPos) {
            transform.position = new Vector2(Globals.maxXPos, transform.position.y);
        } else if (transform.position.x > Globals.maxXPos) {
            transform.position = new Vector2(-Globals.maxXPos, transform.position.y);
        }
    }

    private void Update() {
        Wrap();
    }
}
