using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var quest = (Quest)target;
        if(quest.status == QuestStatus.InProgress)
        {
            foreach(var objective in quest.objectives)
            {
                EditorGUILayout.LabelField($"{objective.objectiveName}: {objective.currentCount}/{objective.requiredCount}");
            }
        }
    }
}
