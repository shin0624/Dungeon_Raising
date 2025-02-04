using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Quest))]//Quest Ÿ�� ����
public class QuestEditor : Editor
//����Ʈ �����Ȳ �ð�ȭ�� ���� ������ Ŀ���� �ν�����
{
    private bool showProgress = true;
    public override void OnInspectorGUI()//����Ʈ ��ũ���ͺ� ������Ʈ Ŭ�� ��, ����Ʈ�� InProgress�����̸� �ν����� �ϴܿ� [ ��ǥ �� : 0/1 ]�� ���� ���� ��Ȳ�� �ǽð����� ��ǥ �� ���൵ ǥ��.
    {
        base.OnInspectorGUI();//�⺻ �ν����� ������
        Quest quest = (Quest)target;//���� ���� ���� ����Ʈ(��ũ���ͺ� ������Ʈ)

        EditorGUILayout.Space(10);
        showProgress = EditorGUILayout.Foldout(showProgress, "�� ����Ʈ ���൵ ����");//���� �� �ִ� �������� ���� ���൵ Ʈ��ŷ ������ ��� ����.

        if(showProgress && quest.status == QuestStatus.InProgress)
        {
            EditorGUI.indentLevel++;
            foreach (QuestObjective objective in quest.objectives)
            {
                DrawObjectiveProgress(objective);
            }

            EditorGUI.indentLevel--;
        }

    }

    private void DrawObjectiveProgress(QuestObjective objective)//���α׷����� + �ؽ�Ʈ�� ǥ���ϰ�, �Ϸ� �� üũ ������ ǥ��, �ڽ� ��Ÿ�Ϸ� ���м��� ����� �ð������� ���� ���� ��.
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        
        // ���൵ ���
        float progress = Mathf.Clamp01((float)objective.currentCount / objective.requiredCount);

        // ��Ÿ�� ����
        GUIStyle style = new GUIStyle(EditorStyles.label)//�Ϸ�� ��ǥ : ����ü + üũ������ / �̿Ϸ� ��ǥ : �⺻ ��Ÿ��
        {
            fontStyle = progress >= 1f ? FontStyle.Bold : FontStyle.Normal
        };

        // ���α׷��� �� + �ؽ�Ʈ
        Rect rect = EditorGUILayout.GetControlRect();
        EditorGUI.ProgressBar(rect, progress, $"{objective.objectiveName}: {objective.currentCount}/{objective.requiredCount}");
        
        // �Ϸ� ���� ������
        if (progress >= 1f)
        {
            Rect iconRect = new Rect(
                rect.xMax - 20, 
                rect.y + 2, 
                16, 
                16
            );
            GUI.DrawTexture(iconRect, EditorGUIUtility.IconContent("Collab").image);
        }
        EditorGUILayout.EndVertical();
    }

    
    public override bool RequiresConstantRepaint()// �ǽð� ������Ʈ�� ���� �޼���
    {
        return Application.isPlaying; // �÷��� ��忡���� �ǽð� ����
    }
}
