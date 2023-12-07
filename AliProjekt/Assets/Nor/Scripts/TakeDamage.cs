using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TakeDamage : MonoBehaviour
{
    //intensity of the vignette
    public float intensisty = 0;

    PostProcessVolume volume;
    Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);

        if(! vignette)
        {
            print("error");
        }
        else
        {
            vignette.enabled.Override(false);
        }
    }

    private void Update()
    {
       
    }

    private IEnumerator TakeDamageEffect()
    {
        intensisty = 0.4f;

        vignette.enabled.Override(true);
        vignette.intensity.Override(0.4f);

        yield return new WaitForSeconds(0.4f);

        while (intensisty >0)
        {

            intensisty -= 0.1f;

            if (intensisty < 0) intensisty = 0;

            vignette.intensity.Override(intensisty);

            yield return new WaitForSeconds(0.1f);

        }

        vignette.enabled.Override(false);
        yield break;

    }
}
