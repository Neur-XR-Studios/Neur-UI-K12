using System.Linq;
using UnityEngine;

public class OwnOutline : MonoBehaviour
{
    private Material outlineMaskMaterial, outlineFillMaterial;

    private void Awake()
    {     
        // Load materials
        outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
        outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));
    }
    private void Start()
    {

    }
    public void EnableOutline()
    {
        string color = DataManager.StaticVariables.HIGHLIGHT_COLOR;
        // Set the outline color
        outlineFillMaterial.SetColor("_OutlineColor", ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.white);

        // Add outline materials to each renderer
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            var materials = renderer.sharedMaterials.ToList();
            if (!materials.Contains(outlineFillMaterial))
            {
                materials.Add(outlineMaskMaterial);
                materials.Add(outlineFillMaterial);
                renderer.materials = materials.ToArray();
            }
        }
    }

    public void DisableOutline()
    {
        // Remove outline materials from each renderer
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            var materials = renderer.sharedMaterials.ToList();
            materials.Remove(outlineMaskMaterial);
            materials.Remove(outlineFillMaterial);
            renderer.materials = materials.ToArray();
        }
    }

    private void OnDestroy()
    {
        Destroy(outlineMaskMaterial);
        Destroy(outlineFillMaterial);
    }

    //subribed on SPAW event
    private void OnEnable()
    {
        EventManager.AddHandler(EVENTS.ENABLE_HIGHLIGHT, EnableOutline);
        EventManager.AddHandler(EVENTS.DISABLE_HIGHLIGHT, DisableOutline);
    }
    //unsubribed from SPAW event
    private void OnDisable()
    {
        EventManager.RemoveHandler(EVENTS.ENABLE_HIGHLIGHT, EnableOutline);
        EventManager.RemoveHandler(EVENTS.DISABLE_HIGHLIGHT, DisableOutline);
    }
}