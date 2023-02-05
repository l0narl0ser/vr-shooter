using Controllers;
using UnityEditor;
using UnityEngine;


namespace DebugPanel
{
    [CustomEditor(typeof(DebugMonoBehaviour))]
    public class DebugPanel : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Kill All Enemy"))
            {
                EnemyController[] enemyControllers = GameObject.FindObjectsOfType<EnemyController>();
                foreach (EnemyController enemyController in enemyControllers)
                {
                    enemyController.DestroyEnemy();
                }
            }
            
            if (GUILayout.Button("Enemy Get Damage"))
            {
                EnemyController[] enemyControllers = GameObject.FindObjectsOfType<EnemyController>();
                foreach (EnemyController enemyController in enemyControllers)
                {
                    enemyController.TakeDamage(15);
                }
            }
        }
    }
}