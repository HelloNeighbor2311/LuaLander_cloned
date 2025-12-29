using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private const float GRAVITY_NORMAL = 0.7f;

    public static Lander Instance;

    public event EventHandler onMovingUp;
    public event EventHandler onMovingLeft;
    public event EventHandler onMovingRight;
    public event EventHandler onIdling;
    
    public event EventHandler<onPickUpCoinEventArg> onPickUpCoin;
    public class onPickUpCoinEventArg
    {
        public Vector3 position;
    }
    
    public event EventHandler<onPickUpFuelEventArg> onPickUpFuel;
    public class onPickUpFuelEventArg
    {
        public Vector3 position;
    }


    [SerializeField] private ParticleSystem confetti;
    public event EventHandler <onStateChangedEventArgs> onStateChanged;
    public class onStateChangedEventArgs
    {
        public State state;
    }

    public event EventHandler<onLandedEventArgs> onLanded;

    private State state;

   
    public class onLandedEventArgs: EventArgs
    {
        public LandingType type;
        public int score;
        public float landingSpeed;
        public float landingAngle;
        public float multiplier;
        
    }

    private Rigidbody2D landerRB;
    float landerSpeed = 700f;
    float landerRotationSpeed = 80f;
    float fuelAmount;
    float fuelAmountMax = 20f;

    public enum LandingType
    {
        SuccessLanding, 
        LandingOnTerrain,
        TooStepAngle,
        TooFastLanding
    }

    public enum State
    {
        WaitForStart,
        Normal,
        GameOver
    }
    
    private void Awake()
    {
        fuelAmount = fuelAmountMax;
        landerRB = GetComponent<Rigidbody2D>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            default:
                break;
            case State.WaitForStart:
                if (GameInput.instance.isUpActionPressed() || GameInput.instance.isLeftActionPressed() || GameInput.instance.isRightActionPressed())
                {
                    
                    landerRB.gravityScale = GRAVITY_NORMAL;
                    SetState(State.Normal);
                }
                break;
            case State.Normal:
                if (GameInput.instance.isUpActionPressed() || GameInput.instance.isLeftActionPressed() || GameInput.instance.isRightActionPressed())
                {
                    ConsumeFuel();
                }

                onIdling?.Invoke(this, EventArgs.Empty);
                if (fuelAmount <= 0)
                {
                    return;
                }
                if (GameInput.instance.isUpActionPressed())
                {
                    landerRB.AddForce(transform.up * landerSpeed * Time.deltaTime);
                    onMovingUp?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.instance.isLeftActionPressed())
                {
                    landerRB.AddTorque(landerRotationSpeed * Time.deltaTime);
                    onMovingLeft?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.instance.isRightActionPressed())
                {
                    landerRB.AddTorque(-landerRotationSpeed * Time.deltaTime);
                    onMovingRight?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
        
    }
    private void ConsumeFuel()
    {
        float fuelConsumption = 1f;
        fuelAmount-= fuelConsumption * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log("Crashed on a terrain");
            onLanded?.Invoke(this, new onLandedEventArgs
            {
                type = LandingType.TooFastLanding,
                score = 0,
                landingSpeed = 0,
                landingAngle = 0,
                multiplier = 0,

            });
            SetState(State.GameOver);
            return;
        }
       
        float softLandingVelocityMagnitude = 4f;
        float relativeVelocityMagnitude = collision.relativeVelocity.magnitude;

        if (relativeVelocityMagnitude > softLandingVelocityMagnitude) { 
        
            Debug.Log("Are you trying to kill all the astronomers ?");
            onLanded?.Invoke(this, new onLandedEventArgs
            {
                type = LandingType.TooFastLanding,
                score = 0,
                landingSpeed = Mathf.RoundToInt(relativeVelocityMagnitude),
                landingAngle = 0,
                multiplier = 0,

            });
            SetState(State.GameOver);
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = .9f;
        if(dotVector < minDotVector)
        {
            //Landed on a too steep angle
            Debug.Log("Landed on a too steep angle");
            onLanded?.Invoke(this, new onLandedEventArgs
            {
                type = LandingType.TooStepAngle,
                score = 0,
                landingSpeed = Mathf.RoundToInt(relativeVelocityMagnitude),
                landingAngle = 0,
                multiplier = landingPad.getScoreMultiplier()

            });
            SetState(State.GameOver);
            return;
        }

        Debug.Log("Successful landing!");
        confetti.Play();
        float maxScoreAmountLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector-1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;

        float maxScoreLandingSpeed = 100;
        float landingSpeedScore = (softLandingVelocityMagnitude - relativeVelocityMagnitude) * maxScoreLandingSpeed;

        Debug.Log("LandingAngleScore: " + landingAngleScore);
        Debug.Log("LandingSpeedScore: " + landingSpeedScore);


        
        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.getScoreMultiplier());
        Debug.Log("Score: " + score);

        onLanded?.Invoke(this, new onLandedEventArgs
        {
            type = LandingType.SuccessLanding,
            score = score,
            landingSpeed = Mathf.RoundToInt(relativeVelocityMagnitude),
            landingAngle = Mathf.RoundToInt(landingAngleScore),
            multiplier = landingPad.getScoreMultiplier()

        });
        SetState(State.GameOver);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Fuel fuel))
        {

            float fuelAdded = 5f;
            fuelAmount += fuelAdded;
            if(fuelAmount >= fuelAmountMax)
            {
                fuelAmount = fuelAmountMax;
            }
            fuel.DestroySelf();
            onPickUpFuel?.Invoke(this, new onPickUpFuelEventArg
            {
                position = collision.transform.position
            });
        }
        if (collision.TryGetComponent(out Coin coin))
        {
            coin.DestroySelf();
            onPickUpCoin?.Invoke(this, new onPickUpCoinEventArg
            {
                position = coin.GetPosition()
            });
        }
    }
    public float getFuel()
    {
        return Mathf.RoundToInt(fuelAmount);
    }
    public float getSpeedX()
    {
        return Mathf.RoundToInt(landerRB.linearVelocityX);
    }
    public float getSpeedY()
    {
        return Mathf.RoundToInt(landerRB.linearVelocityY);
    }
    public float getFuelNormalized()
    {
        return (fuelAmount / fuelAmountMax);
    }
    private void SetState(State state)
    {
        this.state = state;
        onStateChanged?.Invoke(this, new onStateChangedEventArgs
        {
            state = this.state,
        });
    }
}
