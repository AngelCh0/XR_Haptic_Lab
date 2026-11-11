using UnityEngine;

public class DetectorColisionHaptica : MonoBehaviour
{
    public float velocidadMinima = 0.5f;

    public float intervaloMinimo = 0.3f;

    private SimuladorHaptico simulador;
    private float ultimoImpacto = -100f;

    private void Awake()
    {
        simulador = GetComponent<SimuladorHaptico>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < velocidadMinima)
            return;

        if (Time.time - ultimoImpacto < intervaloMinimo)
            return;

        ultimoImpacto = Time.time;

        Debug.Log("ColisiÃ³n detectada con: " +
                  collision.gameObject.name);

        if (simulador != null)
        {
            simulador.ActivarFeedback();
        }
    }
}