using UnityEngine;



[System.Serializable]
public class LongShakerParameters
{
    [Tooltip("Strength of motion in X and Y axes.")]
    public float positionStrength = 0.08f;  
    [Tooltip("Strength of rotation in Z axis.")]
    public float rotationStrength = 0.1f; 
    [Tooltip("Duration of the shake.")]
    public float duration = 1.0f;
}

[System.Serializable]
public class ShortShakerParameters
{
    [Tooltip("Strength of motion in X and Y axes.")]
    public float positionStrength = 0.08f; 
    [Tooltip("Strength of rotation in Z axis.")]
    public float rotationStrength = 0.1f; 
    [Tooltip("Frequency of shaking")]
    public float freq = 25;
    [Tooltip("Number of vibrations before stop.")]
    public int numBounces = 5;
}

/// <summary>
/// Contains shorthands for creating common shake types.
/// </summary>
public class CameraShakePresets
{
    readonly CameraShaker shaker;

    public CameraShakePresets(CameraShaker shaker)
    {
        this.shaker = shaker;
    }

        /// <summary>
        /// Suitable for short and snappy shakes in 2D. Moves camera in X and Y axes and rotates it in Z axis. 
        /// </summary>
        /// <param name="positionStrength">Strength of motion in X and Y axes.</param>
        /// <param name="rotationStrength">Strength of rotation in Z axis.</param>
        /// <param name="freq">Frequency of shaking.</param>
        /// <param name="numBounces">Number of vibrations before stop.</param>
    public void ShortShake2D(float positionStrength = 0.08f, float rotationStrength = 0.1f, float freq = 25, int numBounces = 5)
    {
        BounceShake.Params pars = new BounceShake.Params
        {
            positionStrength = positionStrength,
            rotationStrength = rotationStrength,
            freq = freq,
            numBounces = numBounces
        };
        shaker.RegisterShake(new BounceShake(pars));
    }
    public void ShortShake2D(ShortShakerParameters shakeParameters)
    {
        BounceShake.Params pars = new BounceShake.Params
        {
            positionStrength = shakeParameters.positionStrength,
            rotationStrength = shakeParameters.rotationStrength,
            freq = shakeParameters.freq,
            numBounces = shakeParameters.numBounces
        };
        shaker.RegisterShake(new BounceShake(pars));
    }
    /// <summary>
    /// Suitable for longer and stronger shakes in 2D. Moves camera in X and Y axes and rotates it in Z axis.
    /// </summary>
    /// <param name="positionStrength">Strength of motion in X and Y axes.</param>
    /// <param name="rotationStrength">Strength of rotation in Z axis.</param>
    /// <param name="duration">Duration of the shake.</param>
    public void Explosion2D(float positionStrength = 1f, float rotationStrength = 3, float duration = 0.5f)
    {
        PerlinShake.NoiseMode[] modes =
        {
            new PerlinShake.NoiseMode(8, 1),
            new PerlinShake.NoiseMode(20, 0.3f)
        };
        Envelope.EnvelopeParams envelopePars = new Envelope.EnvelopeParams();
        envelopePars.decay = duration <= 0 ? 1 : 1 / duration;
        PerlinShake.Params pars = new PerlinShake.Params
        {
            strength = new Displacement(new Vector3(1, 1) * positionStrength, Vector3.forward * rotationStrength),
            noiseModes = modes,
            envelope = envelopePars,
        };
        shaker.RegisterShake(new PerlinShake(pars));
    }
    public void Explosion2D(LongShakerParameters shakeParameters)
    {
        float duration = shakeParameters.duration;
        PerlinShake.NoiseMode[] modes =
        {
            new PerlinShake.NoiseMode(8, 1),
            new PerlinShake.NoiseMode(20, 0.3f)
        };
        Envelope.EnvelopeParams envelopePars = new Envelope.EnvelopeParams();
        envelopePars.decay = duration <= 0 ? 1 : 1 / duration;
        PerlinShake.Params pars = new PerlinShake.Params
        {
            strength = new Displacement(new Vector3(1, 1) * shakeParameters.positionStrength, Vector3.forward * shakeParameters.rotationStrength),
            noiseModes = modes,
            envelope = envelopePars,
        };
        shaker.RegisterShake(new PerlinShake(pars));
    }
}

