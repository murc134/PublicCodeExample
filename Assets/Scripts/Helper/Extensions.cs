using UnityEngine;

/// <summary>
/// Extension methods
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Calculates a random color
    /// </summary>
    /// <param name="c">This color object</param>
    /// <param name="randomAlpha">Determies wether we want to randomize the alpha value as well. If false, we use alpha value of <see cref="c"/></param>
    /// <returns>Random Color</returns>
    public static Color Randomize(this Color c, bool randomAlpha = false)
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), randomAlpha ? Random.Range(0.0f, 1.0f) : c.a);
    }

    /// <summary>
    /// Compares the similarity of this Color to another. It is better to use HSV Color Space but for this example this is sufficient
    /// </summary>
    /// <param name="c1">This color</param>
    /// <param name="c2">Other Color</param>
    /// <returns>Value mapped beween 0 and 1 that indicates similatiry of two Colors => 1 indicates equality, 0 indicates not equal at all</returns>
    public static float Similarity(this Color c1, Color c2)
    {
        return 1.0f-Mathf.Sqrt(Mathf.Pow(c2.r - c1.r, 2) + Mathf.Pow(c2.g - c1.g, 2) + Mathf.Pow(c2.b - c1.b, 2)) / 1.732051f;
    }
}
