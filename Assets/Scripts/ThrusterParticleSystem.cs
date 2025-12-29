using UnityEngine;

public class ThrusterParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;
    [SerializeField] private ParticleSystem middleThrusterParticleSystem;
    [SerializeField] private GameObject gameObjectExplosionVFX;



    private void Start()
    {
        Lander.Instance.onMovingUp += Lander_onMovingUp;
        Lander.Instance.onMovingLeft += Lander_onMovingLeft;
        Lander.Instance.onMovingRight += Lander_onMovingRight;
        Lander.Instance.onIdling += Lander_onIdling;
        Lander.Instance.onLanded += Lander_onLanded;


        SetThrusterEnable(leftThrusterParticleSystem, false);
        SetThrusterEnable(rightThrusterParticleSystem, false);
        SetThrusterEnable(middleThrusterParticleSystem, false);
    }

    private void Lander_onLanded(object sender, Lander.onLandedEventArgs e)
    {
        switch (e.type) {
            case Lander.LandingType.TooStepAngle:
            case Lander.LandingType.LandingOnTerrain:
            case Lander.LandingType.TooFastLanding:
                Instantiate(gameObjectExplosionVFX, transform.position, Quaternion.identity);
                ShakeCamera.instance.ShakeMyCamera(3, 1.5f);
                gameObject.SetActive(false);    
                break;
            
        }
    }

    private void Lander_onIdling(object sender, System.EventArgs e)
    {
        SetThrusterEnable(leftThrusterParticleSystem, false);
        SetThrusterEnable(rightThrusterParticleSystem, false);
        SetThrusterEnable(middleThrusterParticleSystem, false);
    }

    private void Lander_onMovingRight(object sender, System.EventArgs e)
    {
    
        SetThrusterEnable(rightThrusterParticleSystem, true);
        
    }

    private void Lander_onMovingLeft(object sender, System.EventArgs e)
    {
        SetThrusterEnable(leftThrusterParticleSystem, true);
   
    }

    private void Lander_onMovingUp(object sender, System.EventArgs e)
    {
        SetThrusterEnable(leftThrusterParticleSystem, true);
        SetThrusterEnable(rightThrusterParticleSystem, true);
        SetThrusterEnable(middleThrusterParticleSystem, true);
    }

    private void SetThrusterEnable(ParticleSystem particleSystem, bool enable)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enable;
    }
}
