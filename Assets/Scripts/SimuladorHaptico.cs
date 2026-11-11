
using System.Collections;
using UnityEngine;

public class SimuladorHaptico : MonoBehaviour
{
    [Range(0f, 1f)]
    public float amplitud = 0.5f;

    [Min(0.01f)]
    public float duracion = 0.1f;

    [Min(1f)]
    public float pulsosPorSegundo = 10f;

    [Min(1)]
    public int cantidadPulsos = 1;

    public ParticleSystem particulas;
    public AudioSource audioImpacto;

    private Renderer renderizador;
    private Color colorOriginal;
    private Coroutine rutinaActual;

    private void Awake()
    {
        renderizador = GetComponent<Renderer>();

        if (renderizador != null)
        {
            colorOriginal = renderizador.material.color;
        }
    }

    public void ActivarFeedback()
    {
        if (rutinaActual != null)
        {
            StopCoroutine(rutinaActual);
        }

        RestaurarColor();

        if (particulas != null)
            particulas.Play();

        if (audioImpacto != null && audioImpacto.clip != null)
            audioImpacto.PlayOneShot(audioImpacto.clip);

        rutinaActual = StartCoroutine(EjecutarPatron());
    }

    private IEnumerator EjecutarPatron()
    {
        float intervalo = 1f / pulsosPorSegundo;

        for (int i = 0; i < cantidadPulsos; i++)
        {
            float intensidad = amplitud;

            if (cantidadPulsos > 1)
            {
                intensidad *= Random.Range(0.7f, 1f);
            }

            intensidad = Mathf.Clamp01(intensidad);

            Debug.Log(
                $"[{gameObject.name}] Impulso {i + 1}: " +
                $"Amplitud={intensidad:F2}, " +
                $"DuraciÃ³n={duracion:F2}s"
            );

            if (renderizador != null)
            {
                renderizador.material.color =
                    Color.Lerp(colorOriginal, Color.white, intensidad);
            }

            yield return new WaitForSeconds(duracion);

            RestaurarColor();

            if (i < cantidadPulsos - 1)
            {
                float espera = Mathf.Max(0f, intervalo - duracion);
                yield return new WaitForSeconds(espera);
            }
        }

        rutinaActual = null;
    }

    private void RestaurarColor()
    {
        if (renderizador != null)
        {
            renderizador.material.color = colorOriginal;
        }
    }

    private void OnDisable()
    {
        if (rutinaActual != null)
        {
            StopCoroutine(rutinaActual);
            rutinaActual = null;
        }

        RestaurarColor();
    }
}