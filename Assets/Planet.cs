using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;

    public ShapeSettings shapeSettings = new ShapeSettings();
    public ColorSettings colorSettings = new ColorSettings();

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;

    private ShapeGenerator shapeGenerator;
    private ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    private MeshFilter[] meshFilters;
    private TerrainFace[] terrainFaces;

    private void Initialize()
    {
        // 1. Inizializziamo i generatori con i rispettivi dati
        shapeGenerator = new ShapeGenerator(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);

        Vector3[] directions = {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            // Assegniamo il materiale definito in ColorSettings a tutte le facce
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    public void OnShapeSettingsUpdated()
    {
        Initialize();
        GenerateMesh();
    }

    public void OnColorSettingsUpdated()
    {
        Initialize();
        GenerateColors();
    }

    private void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            terrainFaces[i].ConstructMesh();
        }

        // Dopo aver costruito la mesh, inviamo i limiti di quota registrati allo shader
        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    private void GenerateColors()
    {
        // Genera la texture 1D e la invia alla GPU
        colorGenerator.UpdateColors();
    }

    private void OnValidate()
    {
        GeneratePlanet();
    }
}