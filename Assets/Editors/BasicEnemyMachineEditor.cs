using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(BasicEnemyMachine))]
public class BasicEnemyMachineEditor : Editor
{
    private BasicEnemyMachine basicEnemyMachine;

    void OnEnable()
    {
        basicEnemyMachine = target as BasicEnemyMachine;
    }

    void OnSceneGUI()
    {
        Vector3[] points = basicEnemyMachine.stateParameters.passive.patrol.patrolPoints;
        for (int i = 0; i < points.Length; i++)
        {
            if (i == 0)
                Handles.color = new Color(0, 1, 0, 0.2f);
            else if (i == points.Length - 1)
                Handles.color = new Color(0, 0, 1, 0.2f);
            else
                Handles.color = new Color(1, 0, 0, 0.2f);
            Handles.DrawSolidDisc(points[i], Vector3.forward, basicEnemyMachine.stateParameters.waypointThreshold);
            points[i] = Handles.PositionHandle(points[i], Quaternion.identity);
        }
        Handles.color = new Color(1, 1, 1, 0.3f);
        Handles.DrawAAPolyLine(points);
        if (basicEnemyMachine.stateParameters.passive.patrol.patrolEndProtocol == PatrolEndProtocol.Loop)
            Handles.DrawLine(points[0], points[points.Length - 1]);
        #if UNITY_EDITOR
        if (GUI.changed)
        {
            EditorUtility.SetDirty(basicEnemyMachine);
            Undo.RecordObject(target, "Set Patrol Point");
            PrefabUtility.RecordPrefabInstancePropertyModifications(target);
        }
        #endif
    }
}
