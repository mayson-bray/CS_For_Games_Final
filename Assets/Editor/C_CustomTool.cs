using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEditor;

public class MeshImportTool : EditorWindow
{
    private Mesh defaultMesh;
    private Color materialColor = Color.white;
    private float sizeMultiplier = 1.0f; // Default size multiplier

    // Specify the file path for the default mesh
    private string defaultMeshPath = "Assets/Editor/Meshes/SM_Cable.obj"; // Change this path to your default mesh file

    [MenuItem("Window/Mesh Import Tool")]
    public static void ShowWindow()
    {
        GetWindow<MeshImportTool>("Mesh Import Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Mesh Import Tool", EditorStyles.boldLabel);

        GUILayout.Space(10);

        // Display a field for the default mesh
        defaultMesh = EditorGUILayout.ObjectField("Default Mesh:", defaultMesh, typeof(Mesh), false) as Mesh;

        GUILayout.Space(10);

        // Display a color picker for the material color
        materialColor = EditorGUILayout.ColorField("Material Color:", materialColor);

        GUILayout.Space(10);

        // Display a field for the size multiplier
        sizeMultiplier = EditorGUILayout.FloatField("Size Multiplier:", sizeMultiplier);

        GUILayout.Space(10);

        // Button to trigger the mesh import
        if (GUILayout.Button("Import Mesh"))
        {
            ImportMesh();
        }
    }

    private void ImportMesh()
    {
        if (defaultMesh == null)
        {
            // Load the default mesh from the specified file path
            defaultMesh = AssetDatabase.LoadAssetAtPath<Mesh>(defaultMeshPath);

            if (defaultMesh == null)
            {
                Debug.LogError("Failed to load the default mesh. Please check the file path.");
                return;
            }
        }

        // Create a new GameObject with a MeshFilter and MeshRenderer
        GameObject meshObject = new GameObject("CustomMesh");
        MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();

        // Assign the default mesh to the MeshFilter
        meshFilter.sharedMesh = defaultMesh;

        // Create a new HDRP material and set its color
        Material material = new Material(Shader.Find("HDRP/Lit"));
        material.SetColor("_BaseColor", materialColor); // _BaseColor is the HDRP property for the material color

        // Assign the material to the MeshRenderer
        meshRenderer.sharedMaterial = material;

        // Apply the size multiplier to the transform scale
        meshObject.transform.localScale = Vector3.one * sizeMultiplier;

        Debug.Log("Mesh imported successfully with HDRP material and size multiplier!");
    }
}
