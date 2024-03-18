using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class PostProcessing : MonoBehaviour
{
    public float intensity;
    public float currentintensity = 0;

    Volume _volume;
    UnityEngine.Rendering.Universal.Vignette  _vignette;
    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
        if (!_vignette)
        {
            print("error");
        }
        else
        {
            _vignette.intensity.Override(0);
        }
    }

    // Update is called once per frame
    public IEnumerator TakeDamageEffect()
    {
        currentintensity = intensity;
        _vignette.intensity.Override(currentintensity);

        yield return new WaitForSeconds(currentintensity);

        while (currentintensity > 0)
        {
            currentintensity -= 0.01f;
            if (currentintensity < 0) currentintensity = 0;
            _vignette.intensity.Override(currentintensity);
            yield return new WaitForSeconds(0.1f);
        }
        _vignette.intensity.Override(intensity);
        
        yield break;
    }
}
