using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingEffectsManager : MonoBehaviour
{
    [Header("Bloom Boom!")]
    [SerializeField] private float boomBloomFadeInSpeed = 5f;
    [SerializeField] private float boomBloomMaxValue = 15f;
    [SerializeField] private float boomBloomFadeOutSpeed = 0.2f;
    [SerializeField] private float boomBloomMinValue = 0f;
    [SerializeField] private bool boomBloomDisableAfterBoom = false;
    private BloomModel.Settings bloomSettings;

    [Header("Vignette Boom!")]
    [SerializeField]
    private float boomVignetteFadeInSpeed = 5f;
    [SerializeField] private float boomVignetteMaxValue = 15f;
    [SerializeField] private float boomVignetteFadeOutSpeed = 0.2f;
    [SerializeField] private float boomVignetteMinValue = 0f;
    [SerializeField] private float pauseBetween = 0f;
    private VignetteModel.Settings vignetteSettings;

    private GrainModel.Settings grainSettings;

    private PostProcessingProfile profile;

    void Start()
    {
        profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
        bloomSettings = profile.bloom.settings;
        vignetteSettings = profile.vignette.settings;
        grainSettings = profile.grain.settings;

        ResetGrain();
        ResetVignette();
    }


    public void BloomBoom()
    {
        StartCoroutine("BloomBoomCoroutine");
    }

    public void VignetteBoom()
    {
        StartCoroutine("VignetteBoomCoroutine");
    }

    IEnumerator BloomBoomCoroutine()
    {
        profile.bloom.enabled = true;

        for (float i = boomBloomMinValue; i < boomBloomMaxValue; i += boomBloomFadeInSpeed)
        {
            bloomSettings.bloom.intensity = i;
            profile.bloom.settings = bloomSettings;
            yield return new WaitForEndOfFrame();
        }

        for (float i = boomBloomMaxValue; i > boomBloomMinValue; i -= boomBloomFadeOutSpeed)
        {
            bloomSettings.bloom.intensity = i;
            profile.bloom.settings = bloomSettings;
            yield return null;
        }

        if (boomBloomDisableAfterBoom)
            profile.bloom.enabled = false;
    }

    IEnumerator VignetteBoomCoroutine()
    {
        profile.vignette.enabled = true;
        profile.grain.enabled = true;

        for (float i = boomVignetteMinValue; i < boomVignetteMaxValue; i += boomVignetteFadeInSpeed)
        {
            vignetteSettings.intensity = i;
            vignetteSettings.intensity += Random.Range(-0.01f, 0.01f);
            profile.vignette.settings = vignetteSettings;

            grainSettings.intensity = Random.Range(0.1f, 0.4f);
            grainSettings.luminanceContribution = Random.Range(0.1f, 0.4f);
            grainSettings.size = Random.Range(0, 1);
            grainSettings.colored = Random.Range(0, 1) > 0.8f;
            profile.grain.settings = grainSettings;

            yield return new WaitForEndOfFrame();
        }

        float startValue = vignetteSettings.intensity;

        for (float i = 0; i < pauseBetween; i += Time.deltaTime)
        {
            vignetteSettings.intensity += Random.Range(-0.01f, 0.01f);
            profile.vignette.settings = vignetteSettings;


            grainSettings.intensity = Random.Range(0.4f, 0.8f);
            grainSettings.luminanceContribution = Random.Range(0.5f, 0.8f);
            grainSettings.size = Random.Range(1, 2);
            grainSettings.colored = Random.Range(0, 1) > 0.2f;
            profile.grain.settings = grainSettings;

            yield return new WaitForEndOfFrame();
        }

        for (float i = vignetteSettings.intensity; i > boomVignetteMinValue; i -= boomVignetteFadeOutSpeed)
        {
            vignetteSettings.intensity = i;
            profile.vignette.settings = vignetteSettings;

            grainSettings.intensity = Random.Range(0f, 0.9f);
            grainSettings.luminanceContribution = Random.Range(0.8f, 0.1f);
            grainSettings.size = Random.Range(0.5f, 1.1f);
            profile.grain.settings = grainSettings;


            yield return new WaitForEndOfFrame();
        }

        ResetGrain();
        ResetVignette();
    }

    private void ResetGrain()
    {
        grainSettings.intensity = 0;
        grainSettings.luminanceContribution = 0;
        grainSettings.size = 0;
        grainSettings.colored = false;
        profile.grain.settings = grainSettings;
    }

    private void ResetVignette()
    {
        vignetteSettings.intensity = 0;
        profile.vignette.settings = vignetteSettings;
    }


}