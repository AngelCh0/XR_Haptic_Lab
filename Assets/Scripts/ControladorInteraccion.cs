
using UnityEngine;

public class ControladorInteraccion : MonoBehaviour
{
    public Camera camara;

    public float distanciaMaxima = 100f;

    private void Start()
    {
        if (camara == null)
        {
            camara = Camera.main;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectarObjeto();
        }
    }

    private void DetectarObjeto()
    {
        if (camara == null)
        {
            Debug.LogWarning("No se encontrÃ³ una cÃ¡mara.");
            return;
        }

        Ray rayo = camara.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayo, out RaycastHit impacto,
                            distanciaMaxima))
        {
            SimuladorHaptico objeto =
                impacto.collider.GetComponent<SimuladorHaptico>();

            if (objeto != null)
            {
                Debug.Log("Objeto seleccionado: " +
                          impacto.collider.name);

                objeto.ActivarFeedback();
            }
        }
    }
}