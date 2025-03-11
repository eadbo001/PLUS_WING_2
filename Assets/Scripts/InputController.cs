using UnityEngine;

public class InputController : SingletonMono<InputController>
{
    public delegate void MouseAxisXY (Vector2 mouseXY);
    public event MouseAxisXY OnMouseAxisXY;

    public delegate void KeyAxisXY(Vector2 keyXY);
    public event KeyAxisXY OnKeyAxisXY;

    public delegate void FireDown(bool isDown);
    public event FireDown OnFireDown;

    private void Update()
    {
        OnMouseAxisXY?.Invoke(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        OnKeyAxisXY?.Invoke(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if (Input.GetButtonDown("Fire1"))
            OnFireDown?.Invoke(true);
        else if (Input.GetButtonUp("Fire1"))
            OnFireDown?.Invoke(false);
    }
}
