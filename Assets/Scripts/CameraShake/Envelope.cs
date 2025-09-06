using UnityEngine;

//Controls strength of the shake over time.
public class Envelope : IAmplitudeController
{

    readonly EnvelopeParams pars;
    readonly EnvelopeControlMode controlMode;

    float amplitude;
    float targetAmplitude;
    float sustainEndTime;
    bool finishWhenAmplitudeZero;
    bool finishImmediately;
    EnvelopeState state;

    /// Creates an Envelope instance.
    public Envelope(EnvelopeParams pars, float initialTargetAmplitude, EnvelopeControlMode controlMode)
    {
        this.pars = pars;
        this.controlMode = controlMode;
        SetTarget(initialTargetAmplitude);
    }

        /// <summary>
        /// The value by which you want to multiply shake displacement.
        /// </summary>
    public float Intensity { get; private set; }

    public bool IsFinished
    {
        get
        {
            if (finishImmediately) return true;
            return (finishWhenAmplitudeZero || controlMode == EnvelopeControlMode.Auto)
                && amplitude <= 0 && targetAmplitude <= 0;
        }
    }

    public void Finish()
    {
        finishWhenAmplitudeZero = true;
        SetTarget(0);
    }

    public void FinishImmediately()
    {
        finishImmediately = true;
    }

    // Update is called every frame by the shake.
    public void Update(float deltaTime)
    {
        if (IsFinished) return;

        if (state == EnvelopeState.Increase)
        {
            if (pars.attack > 0)
            {
                amplitude += deltaTime * pars.attack;
            }
            if (amplitude > targetAmplitude || pars.attack <= 0)
            {
                amplitude = targetAmplitude;
                state = EnvelopeState.Sustain;
                if (controlMode == EnvelopeControlMode.Auto)
                {
                    sustainEndTime = Time.time + pars.sustain;
                }
            }
        }
        else
        {
            if (state == EnvelopeState.Decrease)
            {
                if (pars.decay > 0)
                {
                    amplitude -= deltaTime * pars.decay;
                }
                if (amplitude < targetAmplitude || pars.decay <= 0)
                {
                    amplitude = targetAmplitude;
                    state = EnvelopeState.Sustain;
                }
            }
            else
            {
                if (controlMode == EnvelopeControlMode.Auto && Time.time > sustainEndTime)
                {
                    SetTarget(0);
                }
            }
        }

        amplitude = Mathf.Clamp01(amplitude);
        Intensity = Power.Evaluate(amplitude, pars.degree);
    }

    public void SetTargetAmplitude(float value)
    {
         if (controlMode == EnvelopeControlMode.Manual && !finishWhenAmplitudeZero)
        {
            SetTarget(value);
        }
    }

    private void SetTarget(float value)
    {
        targetAmplitude = Mathf.Clamp01(value);
        state = targetAmplitude > amplitude ? EnvelopeState.Increase : EnvelopeState.Decrease;
    }


    [System.Serializable]
    public class EnvelopeParams
    {
        [Tooltip("How fast the amplitude increases.")]
        public float attack = 10;

        [Tooltip("How long in seconds the amplitude holds maximum value.")]
        public float sustain = 0;

        [Tooltip("How fast the amplitude decreases.")]
        public float decay = 1f;


        [Tooltip("Power in which the amplitude is raised to get intensity.")]
        public Degree degree = Degree.Cubic;
    }

    public enum EnvelopeControlMode { Auto, Manual }

    public enum EnvelopeState { Sustain, Increase, Decrease }
    }

public interface IAmplitudeController
{
    /// <summary>
    /// Sets value to which amplitude will move over time.
    /// </summary>
    void SetTargetAmplitude(float value);

    /// <summary>
    /// Sets amplitude to zero and finishes the shake when zero is reached.
    /// </summary>
    void Finish();

    /// <summary>
    /// Immediately finishes the shake.
    /// </summary>
    void FinishImmediately();
}
