using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

namespace Utils
{
    [CustomPropertyDrawer(typeof(AnimationClipPickerAttribute))]
    public class AnimationClipPickerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = property.serializedObject.targetObject as MonoBehaviour;
            if (target == null)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var animator = target.GetComponent<Animator>();
            if (animator == null)
            {
                animator = target.GetComponentInChildren<Animator>();
                if (animator == null)
                {
                    EditorGUI.LabelField(position, label.text, "Animator not found");
                    return;
                }               
            }

            var controller = animator.runtimeAnimatorController as AnimatorController;
            if (controller == null)
            {
                EditorGUI.LabelField(position, label.text, "No AnimatorController");
                return;
            }

            var clips = controller.animationClips;
            if (clips.Length == 0)
            {
                EditorGUI.LabelField(position, label.text, "No clips found");
                return;
            }

            string[] names = new string[clips.Length];
            int index = -1;
            for (int i = 0; i < clips.Length; i++)
            {
                names[i] = clips[i].name;
                if (property.stringValue == clips[i].name)
                    index = i;
            }

            int newIndex = EditorGUI.Popup(position, label.text, Mathf.Max(0, index), names);
            property.stringValue = names[newIndex];
        }
    }
}