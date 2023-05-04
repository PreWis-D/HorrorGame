using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fieldOfView = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fieldOfView.transform.position, Vector3.up, Vector3.forward, 360, fieldOfView.Radius);

        Vector3 viewAngle01 = DirectionFromAngle(fieldOfView.transform.eulerAngles.y, -fieldOfView.Angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fieldOfView.transform.eulerAngles.y, fieldOfView.Angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + viewAngle01 * fieldOfView.Radius);
        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + viewAngle02 * fieldOfView.Radius);

        if (fieldOfView.CanSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fieldOfView.transform.position, fieldOfView.Player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegress)
    {
        angleInDegress += eulerY;

        return new Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }
}
