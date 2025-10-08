using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SwitchRandomMarker : MonoBehaviour
{

    [Tooltip("El modelo 3D que se moverá entre los Targets.")]
    public GameObject model3d;

    [Tooltip("Lista de todos los Targets (ObserverBehaviour) a considerar.")]
    public ObserverBehaviour[] Targets;

    [Tooltip("Velocidad de movimiento del modelo 3D.")]
    public float speed = 1.0f;


    private bool isMoving = false;
    private int targetIndex = -1;

    public void SwitchMarker()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveModel());
        }
    }

    private IEnumerator MoveModel()
    {
        isMoving = true;

        ObserverBehaviour target = GetRandomTarget();

        if (target == null)
        {
            Debug.LogWarning("No se encontró ningún Target de Vuforia activo para moverse.");
            isMoving = false;
            yield break;
        }

        Vector3 startPos = model3d.transform.position;
        Vector3 endPos = target.transform.position;

        float journey = 0f;

        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed;
            model3d.transform.position = Vector3.Lerp(startPos, endPos, Mathf.Min(journey, 1.0f));
            yield return null;
        }

        model3d.transform.position = endPos;

        isMoving = false;
    }
    private ObserverBehaviour GetRandomTarget()
    {
        List<ObserverBehaviour> detectedTargets = new List<ObserverBehaviour>();

        foreach (ObserverBehaviour target in Targets)
        {
            if (target != null && (target.TargetStatus.Status == Status.TRACKED || target.TargetStatus.Status == Status.EXTENDED_TRACKED))
            {
                detectedTargets.Add(target);
            }
        }

        if (detectedTargets.Count > 0)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, detectedTargets.Count);
            }
            while (detectedTargets.Count > 1 && randomIndex == targetIndex);

            targetIndex = randomIndex;
            return detectedTargets[randomIndex];
        }

        return null;
    }
}