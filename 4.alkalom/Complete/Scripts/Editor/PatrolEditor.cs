using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Complete;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Patrol))]
public class PatrolEditor : Editor
{
    List<Transform> Waypoints => (target as Patrol).Waypoints;
    Patrol patrol => target as Patrol;
    int length;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("+", GUILayout.Width(25)))
        {
            Waypoints.Add(null);
        }

        length = Waypoints.Count;
        if (length > 0)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawElement(0, true, (length == 1));

            if (length > 1)
            {
                for (int i = 1; i < length - 1; i++)
                {
                    DrawElement(i);
                }

                DrawElement(length - 1, false, true);
            }
            EditorGUILayout.EndVertical();
        }
    }
    public void DrawElement(int index, bool First = false, bool Last = false)
    {
        float ButtonWidth = First || Last ? 54f : 25f;
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label((index + 1) + ". ", GUILayout.Width(50));
        var temp = Waypoints[index];
        Waypoints[index] = EditorGUILayout.ObjectField(Waypoints[index], typeof(Transform), true) as Transform;
        if (temp != Waypoints[index])
            EditorSceneManager.MarkSceneDirty(patrol.gameObject.scene);
        if (!Last)
        {
            if (GUILayout.Button("↓", GUILayout.Width(ButtonWidth)))
            {
                MoveDown(index);
            }
        }
        if (!First)
        {
            if (GUILayout.Button("↑", GUILayout.Width(ButtonWidth)))
            {
                MoveUp(index);
            }
        }
        GUI.color = new Color(1f, 0.5f, 0.5f, 1f);
        if (GUILayout.Button("×", GUILayout.Width(25f)))
        {
            Remove(index);
        }
        GUI.color = Color.white;
        EditorGUILayout.EndHorizontal();

    }
    public void MoveDown(int index)
    {
        Transform temp = Waypoints[index];
        Waypoints[index] = Waypoints[index + 1];
        Waypoints[index + 1] = temp;
        EditorSceneManager.MarkSceneDirty(patrol.gameObject.scene);
    }
    public void MoveUp(int index)
    {
        Transform temp = Waypoints[index];
        Waypoints[index] = Waypoints[index - 1];
        Waypoints[index - 1] = temp;
        EditorSceneManager.MarkSceneDirty(patrol.gameObject.scene);
    }
    public void Remove(int index)
    {
        Waypoints.RemoveAt(index);
        length = Waypoints.Count;
        EditorSceneManager.MarkSceneDirty(patrol.gameObject.scene);
    }
}
