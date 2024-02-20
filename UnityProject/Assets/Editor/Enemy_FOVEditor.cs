using UnityEditor;
using UnityEngine;

//sets it so it only works on Enemy Behavior script
[CustomEditor(typeof(Enemy_Behavior))]
public class Enemy_FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        //gets the enemybehavior script on the object
        Enemy_Behavior fov = (Enemy_Behavior)target;
        //style stuff
        Handles.color = Color.white;
        //makes a circle the size of the radius at the position of the enemy
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        //creates two vectors to make the lines for the veiwing angle
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        if (fov.detected)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
