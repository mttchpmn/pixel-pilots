using UnityEngine;

public class AirplaneInputBase : MonoBehaviour
{
    public float Pitch { get; protected set; } = 0f;
    public float Roll { get; protected set; } = 0f;
    public float Yaw { get; protected set; } = 0f;
    public int Flaps { get; protected set; } = 0;
    public float Brake { get; protected set; } = 0f;
    public float Throttle { get; protected set; } = 0f;

    public int MaxFlapIncrements = 2;
    public KeyCode BrakeKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    protected virtual void HandleInput()
    {
        Pitch = Input.GetAxis("Vertical");
        Roll = Input.GetAxis("Horizontal");
        Yaw = Input.GetAxis("Yaw");
        Throttle = Input.GetAxis("Throttle");

        Brake = Input.GetKey(BrakeKey) ? 1f : 0f;

        if (Input.GetKeyDown(KeyCode.F))
            Flaps += 1;
        if (Input.GetKeyDown(KeyCode.G))
            Flaps -= 1;

        Flaps = Mathf.Clamp(Flaps, 0, MaxFlapIncrements);
    }
}