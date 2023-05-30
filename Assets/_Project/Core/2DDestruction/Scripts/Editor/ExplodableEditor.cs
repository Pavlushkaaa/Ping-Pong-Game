using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Explodable))]
public class ExplodableEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        Explodable myTarget = (Explodable)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate Fragments"))
        {
            myTarget.fragmentInEditor();
            EditorUtility.SetDirty(myTarget);
        }
        if (GUILayout.Button("Destroy Fragments"))
        {
            myTarget.deleteFragments();
            EditorUtility.SetDirty(myTarget);
        }

    }
}
