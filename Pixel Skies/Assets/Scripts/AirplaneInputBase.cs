using UnityEngine;

public class AirplaneInputBase : MonoBehaviour
{
    public float Pitch { get; protected set; } = 0f;
    public float Roll { get; protected set; } = 0f;
    public float Yaw { get; protected set; } = 0f;
    public int Flaps { get; protected set; } = 0;
    public float Brake { get; protected set; } = 0f;
    public float Throttle { get; protected set; } = 0f;
    public float ThrottleSetting { get; protected set; }

    [Header("Input Attributes")]
    public float ThrottleSpeed = 0.1f;
    public int MaxFlapIncrements = 2;
    public KeyCode BrakeKey = KeyCode.Space;
    
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
        HandleThrottleChange();

        Brake = Input.GetKey(BrakeKey) ? 1f : 0f;

        if (Input.GetKeyDown(KeyCode.F))
            Flaps += 1;
        if (Input.GetKeyDown(KeyCode.G))
            Flaps -= 1;

        Flaps = Mathf.Clamp(Flaps, 0, MaxFlapIncrements);
    }

    private void HandleThrottleChange()
    {
        ThrottleSetting += (Throttle * ThrottleSpeed * Time.deltaTime);
        ThrottleSetting = Mathf.Clamp01(ThrottleSetting);
    }
}