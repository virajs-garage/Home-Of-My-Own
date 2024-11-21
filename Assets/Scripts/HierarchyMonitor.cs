using System.Linq;
using UnityEditor;
using UnityEngine;

//[InitializeOnLoad]
//public static class HierarchyMonitor
//{
//    static HierarchyMonitor()
//    {
//        EditorApplication.hierarchyChanged += OnHierarchyChanged;
//    }

//    static void OnHierarchyChanged()
//    {
//        var allGamePieces = Resources.FindObjectsOfTypeAll(typeof(GamePiece));
        
//        int count = 0;
//        foreach (GamePiece gamePiece in allGamePieces.OrderBy(z => int.Parse(z.name)))
//        {
//            gamePiece.UpdateList();
//            count++;
//        }

//        //Debug.LogFormat("{0} GamePiece neighbors were updated.", count);
//    }
//}
