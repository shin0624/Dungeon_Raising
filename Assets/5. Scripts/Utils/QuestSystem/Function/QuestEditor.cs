using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Quest))]//Quest 타겟 지정
public class QuestEditor : Editor
//퀘스트 진행상황 시각화를 위한 간단한 커스텀 인스펙터
{
    private bool showProgress = true;
    public override void OnInspectorGUI()//퀘스트 스크립터블 오브젝트 클릭 시, 퀘스트가 InProgress상태이면 인스펙터 하단에 [ 목표 명 : 0/1 ]과 같이 진행 상황을 실시간으로 목표 별 진행도 표시.
    {
        base.OnInspectorGUI();//기본 인스펙터 렌더링
        Quest quest = (Quest)target;//현재 편집 중인 퀘스트(스크립터블 오브젝트)

        EditorGUILayout.Space(10);
        showProgress = EditorGUILayout.Foldout(showProgress, "▶ 퀘스트 진행도 추적");//접을 수 있는 섹션으로 만들어서 진행도 트래킹 섹션을 토글 가능.

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

    private void DrawObjectiveProgress(QuestObjective objective)//프로그레스바 + 텍스트를 표시하고, 완료 시 체크 아이콘 표시, 박스 스타일로 구분선을 만들어 시각적으로 보기 쉽게 함.
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        
        // 진행도 계산
        float progress = Mathf.Clamp01((float)objective.currentCount / objective.requiredCount);

        // 스타일 설정
        GUIStyle style = new GUIStyle(EditorStyles.label)//완료된 목표 : 볼드체 + 체크아이콘 / 미완료 목표 : 기본 스타일
        {
            fontStyle = progress >= 1f ? FontStyle.Bold : FontStyle.Normal
        };

        // 프로그레스 바 + 텍스트
        Rect rect = EditorGUILayout.GetControlRect();
        EditorGUI.ProgressBar(rect, progress, $"{objective.objectiveName}: {objective.currentCount}/{objective.requiredCount}");
        
        // 완료 상태 아이콘
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

    
    public override bool RequiresConstantRepaint()// 실시간 업데이트를 위한 메서드
    {
        return Application.isPlaying; // 플레이 모드에서만 실시간 갱신
    }
}
