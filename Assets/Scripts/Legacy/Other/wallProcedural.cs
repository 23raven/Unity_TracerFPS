using UnityEngine;

/// <summary>
/// Attach to any Cube-wall. Reads BoxCollider size (X, Y, Z),
/// fills the XZ footprint with vertical pillars of randomised height (50–100% of wall Y).
/// Original MeshRenderer is hidden; BoxCollider stays as the physics boundary.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class WallProcedural : MonoBehaviour
{
    [Header("Grid (XZ footprint)")]
    [Tooltip("Columns along local X (width)")]
    public int columnsX = 10;

    [Tooltip("Columns along local Z (depth / thickness)")]
    public int columnsZ = 3;

    [Tooltip("Fraction of tile slot used by the pillar (0.7 = 30% gap)")]
    [Range(0.5f, 1.0f)]
    public float fillFactor = 0.82f;

    [Header("Height")]
    [Tooltip("Minimum pillar height as fraction of wall height")]
    [Range(0.1f, 1.0f)]
    public float minHeightFraction = 0.5f;

    [Tooltip("Maximum pillar height as fraction of wall height")]
    [Range(0.1f, 1.0f)]
    public float maxHeightFraction = 1.0f;

    [Header("Colour")]
    public Material baseMaterial;
    public Color baseColor = new Color(0.45f, 0.45f, 0.45f);

    [Range(0f, 0.35f)]
    public float colorVariance = 0.12f;

    // ---------------------------------------------------------------

    void Start()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        Vector3 size = col.size; // local-space

        // Hide original mesh, keep collider
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null) mr.enabled = false;

        GameObject container = new GameObject("BambooWall");
        container.transform.SetParent(transform, false);

        float stepX = size.x / columnsX;
        float stepZ = size.z / columnsZ;

        float pillarW = stepX * fillFactor;
        float pillarD = stepZ * fillFactor;

        float startX = -size.x * 0.5f + stepX * 0.5f;
        float startZ = -size.z * 0.5f + stepZ * 0.5f;
        float bottomY = -size.y * 0.5f; // local bottom of the original wall

        for (int iz = 0; iz < columnsZ; iz++)
        {
            for (int ix = 0; ix < columnsX; ix++)
            {
                float heightFrac = Random.Range(minHeightFraction, maxHeightFraction);
                float pillarH = size.y * heightFrac;

                // Pillar grows upward from the bottom of the wall
                float localY = bottomY + pillarH * 0.5f;

                Vector3 localPos = new Vector3(
                    startX + ix * stepX,
                    localY,
                    startZ + iz * stepZ
                );

                GameObject pillar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                pillar.name = $"Pillar_{ix}_{iz}";
                pillar.transform.SetParent(container.transform, false);
                pillar.transform.localPosition = localPos;
                pillar.transform.localScale = new Vector3(pillarW, pillarH, pillarD);

                Destroy(pillar.GetComponent<Collider>());

                Renderer rend = pillar.GetComponent<Renderer>();
                Material mat = baseMaterial != null
                    ? new Material(baseMaterial)
                    : new Material(Shader.Find("Standard"));

                mat.color = RandomTint(baseColor);
                rend.material = mat;
            }
        }
    }

    Color RandomTint(Color c)
    {
        float brightness = Random.Range(-colorVariance, colorVariance);
        return new Color(
            Mathf.Clamp01(c.r + brightness),
            Mathf.Clamp01(c.g + brightness),
            Mathf.Clamp01(c.b + brightness),
            c.a
        );
    }
}