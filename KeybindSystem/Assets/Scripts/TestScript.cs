using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float speed = 5f;
    public Keybindings playerKeybinds = null;

    void Update()
    {
        if (Input.GetKey(playerKeybinds.GetKeyCode("forward")))
            Move(Vector3.forward);
        else if (Input.GetKey(playerKeybinds.GetKeyCode("back")))
            Move(Vector3.back);
    }

    private void Move(Vector3 dir)
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }
}
