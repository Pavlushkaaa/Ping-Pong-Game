using UnityEngine;
using System.Collections.Generic;
using Delaunay;
using Core;

public static class SpriteExploder 
{
    public static List<GameObject> Explode(DestructInfo info)
    {
        List<GameObject> pieces = new List<GameObject>();

        var mat = createFragmentMaterial(info);

        //get transform information
        Vector3 origScale = info.Transform.localScale;
        info.Transform.localScale = Vector3.one;
        Quaternion origRotation = info.Transform.localRotation;
        info.Transform.localRotation = Quaternion.identity;

        Vector2 origVelocity = Vector2.zero;

        if (info.Rigidbody != null)
            origVelocity = info.Rigidbody.velocity;

        List<Vector2> points = new List<Vector2>();
        List<Vector2> borderPoints = new List<Vector2>();

        if (info.PolygonCollider != null)
        {
            points = getPoints(info.PolygonCollider);
            borderPoints = getPoints(info.PolygonCollider);
        }
        else
        {
            points = getPoints(info.BoxCollider);
            borderPoints = getPoints(info.BoxCollider);
        }

        Rect rect = getRect(info);

        for (int i = 0; i < info.FragmentsNumber; i++)
        {
            points.Add(new Vector2(Random.Range(
                rect.width / -2 + rect.center.x, rect.width / 2 + rect.center.x),
                Random.Range(rect.height / -2 + rect.center.y, rect.height / 2 + rect.center.y)
                ));
        }

        Voronoi voronoi = new Voronoi(points, null, rect);
        List<List<Vector2>> clippedRegions = new List<List<Vector2>>();
        foreach (List<Vector2> region in voronoi.Regions())
        {
            clippedRegions = ClipperHelper.clip(borderPoints, region);
            foreach (List<Vector2> clippedRegion in clippedRegions)
                pieces.Add(generateVoronoiPiece(info, clippedRegion, origVelocity, origScale, origRotation, mat));
        }

        //reset transform information
        info.Transform.localScale = origScale;
        info.Transform.localRotation = origRotation;

        foreach (var piece in pieces)
        {
            piece.AddComponent<PartsAutoDestroy>();
            piece.layer = info.LayerId;
        }

        return pieces;
    }

    private static GameObject generateVoronoiPiece(DestructInfo info, List<Vector2> region, Vector2 origVelocity, Vector3 origScale, Quaternion origRotation, Material mat)
    {
        //Create Game Object and set transform settings properly
        GameObject piece = new GameObject(info.GameObject.name + " piece");
        piece.transform.position = info.Transform.position;
        piece.transform.rotation = info.Transform.rotation;
        piece.transform.localScale = info.Transform.localScale;

        //Create and Add Mesh Components
        MeshFilter meshFilter = (MeshFilter)piece.AddComponent(typeof(MeshFilter));
        MeshRenderer meshRenderer = (MeshRenderer)piece.AddComponent(typeof(MeshRenderer));

        Mesh uMesh = meshFilter.sharedMesh;
        if (uMesh == null)
        {
            meshFilter.mesh = new Mesh();
            uMesh = meshFilter.sharedMesh;
        }

		Voronoi voronoi = new Voronoi(region, null, getRect(region));

		Vector3[] vertices = calcVerts(voronoi);
        int[] triangles = calcTriangles(voronoi);

        uMesh.vertices = vertices;
        uMesh.triangles = triangles;
        //if (source.GetComponent<SpriteRenderer>() != null)
            uMesh.uv = calcUV(vertices, info.SpriteRenderer, info.Transform);
        //else
        //    uMesh.uv = calcUV(vertices, source.GetComponent<MeshRenderer>(), source.transform);

        //set transform properties before fixing the pivot for easier rotation
        piece.transform.localScale = origScale;
        piece.transform.localRotation = origRotation;

        Vector3 diff = calcPivotCenterDiff(piece);
        centerMeshPivot(piece, diff);
        uMesh.RecalculateBounds();
		uMesh.RecalculateNormals();

        //setFragmentMaterial(piece, source);
        meshRenderer.sharedMaterial = mat;

        //assign mesh
        meshFilter.mesh = uMesh;

        //Create and Add Polygon Collider
        PolygonCollider2D collider = piece.AddComponent<PolygonCollider2D>();
        collider.SetPath(0, calcPolyColliderPoints(region,diff));

        //Create and Add Rigidbody
        Rigidbody2D rigidbody = piece.AddComponent<Rigidbody2D>();
        rigidbody.AddForce((rigidbody.transform.position - info.Transform.position).normalized * info.Force);

        return piece;
    }

    /// <summary>
    /// generates a list of points from a box collider
    /// </summary>
    /// <param name="collider">source box collider</param>
    /// <returns>list of points</returns>
    private static List<Vector2> getPoints(BoxCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();

        Vector2 center = collider.offset;
        Vector2 size = collider.size;
        //bottom left
        points.Add(new Vector2((center.x - size.x / 2), (center.y - size.y / 2)));
        //top left
        points.Add(new Vector2((center.x - size.x / 2), (center.y + size.y / 2)));
        //top right
        points.Add(new Vector2((center.x + size.x / 2), (center.y + size.y / 2)));
        //bottom right
        points.Add(new Vector2((center.x + size.x / 2), (center.y - size.y / 2)));

        return points;
    }
    /// <summary>
    /// generates a list of points from a polygon collider
    /// </summary>
    /// <param name="collider">source polygon collider</param>
    /// <returns>list of points</returns>
    private static List<Vector2> getPoints(PolygonCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();

        foreach (Vector2 point in collider.GetPath(0))
        {
            points.Add(point);
        }

        return points;
    }

    /// <summary>
    /// generates a rectangle based on the rendering bounds of the object
    /// </summary>
    /// <param name="source">gameobject to get the rectangle from</param>
    /// <returns>a Rectangle representing the rendering bounds of the object</returns>
    private static Rect getRect(DestructInfo info)
    {
		Bounds bounds = info.SpriteRenderer.bounds;
		Rect rect = new Rect(bounds.extents.x * -1, bounds.extents.y * -1, bounds.size.x, bounds.size.y);
		rect.center = new Vector2(rect.center.x + bounds.center.x - info.Transform.position.x, rect.center.y + bounds.center.y - info.Transform.position.y);
		return rect;
    }
    private static Rect getRect(List<Vector2> region)
    {
        Vector2 center = new Vector2();
        float minX = region[0].x;
        float maxX = minX;
        float minY = region[0].y;
        float maxY = minY;
        foreach (Vector2 v in region)
        {
            center += v;
            if (v.x < minX)
            {
                minX = v.x;
            }
            if (v.x > maxX)
            {
                maxX = v.x;
            }
            if (v.y < minY)
            {
                minY = v.y;
            }
            if (v.y > maxY)
            {
                maxY = v.y;
            }
        }
        center /= region.Count;
        Vector2 size = new Vector2(maxX - minX, maxY - minY);
        return new Rect(center, size);
    }

    /// <summary>
    /// calculates the UV coordinates for the given vertices based on the provided Sprite
    /// </summary>
    /// <param name="vertices">vertices to generate the UV coordinates for</param>
    /// <param name="sRend">Sprite Renderer of original object</param>
    /// <param name="sTransform">Transform of the original object</param>
    /// <returns>array of UV coordinates for the mesh</returns>
    private static Vector2[] calcUV(Vector3[] vertices, SpriteRenderer sRend, Transform sTransform)
    {
        float texHeight = (sRend.bounds.extents.y * 2) / sTransform.localScale.y;
        float texWidth = (sRend.bounds.extents.x * 2) / sTransform.localScale.x;
        Vector3 botLeft = sTransform.InverseTransformPoint(new Vector3(sRend.bounds.center.x - sRend.bounds.extents.x, sRend.bounds.center.y - sRend.bounds.extents.y, 0));
        Vector2[] uv = new Vector2[vertices.Length];

        Vector2[] sourceUV = sRend.sprite.uv;
        Vector2 uvMin;
        Vector2 uvMax;
        getUVRange(out uvMin, out uvMax, sourceUV);

        for (int i = 0; i < vertices.Length; i++)
        {

            float x = (vertices[i].x - botLeft.x) / texWidth;
            x = scaleRange(x, 0, 1, uvMin.x, uvMax.x);
            float y = (vertices[i].y - botLeft.y) / texHeight;
            y = scaleRange(y, 0, 1, uvMin.y, uvMax.y);

            uv[i] = new Vector2(x, y);
        }
        return uv;
    }
    private static Vector2[] calcUV(Vector3[] vertices, MeshRenderer mRend, Transform sTransform)
    {
        float texHeight = (mRend.bounds.extents.y * 2) / sTransform.localScale.y;
        float texWidth = (mRend.bounds.extents.x * 2) / sTransform.localScale.x;
        Vector3 botLeft = sTransform.InverseTransformPoint(new Vector3(mRend.bounds.center.x - mRend.bounds.extents.x, mRend.bounds.center.y - mRend.bounds.extents.y, 0));
        Vector2[] uv = new Vector2[vertices.Length];

        Vector2[] sourceUV = sTransform.GetComponent<MeshFilter>().sharedMesh.uv;
        Vector2 uvMin;
        Vector2 uvMax;
        getUVRange(out uvMin, out uvMax, sourceUV);

        for (int i = 0; i < vertices.Length; i++)
        {
            float x = (vertices[i].x - botLeft.x) / texWidth;
            x = scaleRange(x, 0, 1, uvMin.x, uvMax.x);
            float y = (vertices[i].y - botLeft.y) / texHeight;
            y = scaleRange(y, 0, 1, uvMin.y, uvMax.y);

            uv[i] = new Vector2(x, y);
        }
        return uv;
    }
    private static void getUVRange(out Vector2 min, out Vector2 max, Vector2[]uv)
    {
        min = uv[0];
        max = uv[0];

        foreach (Vector2 p in uv)
        {
            if (p.x < min.x)
            {
                min.x = p.x;
            }
            if (p.x > max.x)
            {
                max.x = p.x;
            }
            if (p.y < min.y)
            {
                min.y = p.y;
            }
            if (p.y > max.y)
            {
                max.y = p.y;
            }
        }
    }
    private static float scaleRange(float target, float oldMin, float oldMax, float newMin, float newMax)
    {
        return (target / ((oldMax - oldMin) / (newMax - newMin))) + newMin;
    }
    private static Vector3[] calcVerts(Voronoi region)
    {
        List<Site> sites = region.Sites()._sites;
        Vector3[] vertices = new Vector3[sites.Count];
        int idx = 0;
        foreach (Site s in sites)
        {
            vertices[idx++] = new Vector3(s.x,s.y,0);
        }
        return vertices;
    }
    private static int[] calcTriangles(Voronoi region)
    {
        //calculate unity triangles
        int[] triangles = new int[region.Triangles().Count*3];

        List<Site> sites = region.Sites()._sites;
        int idx = 0;
        foreach (Triangle t in region.Triangles())
        {
            triangles[idx++] = sites.IndexOf(t.sites[0]);
            triangles[idx++] = sites.IndexOf(t.sites[1]);
            triangles[idx++] = sites.IndexOf(t.sites[2]);
        }
        return triangles;
    }
    private static Vector2[] calcPolyColliderPoints(List<Vector2> points, Vector2 offset)
    {
        Vector2[] result = new Vector2[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            result[i] = points[i] + offset;
        }
        return result;
    }

    /// <summary>
    /// calculates the distance between the targets pivot and it's actual center
    /// </summary>
    /// <param name="target">target gameobject to do the calculation on</param>
    /// <returns>distance between center and pivot</returns>
    private static Vector3 calcPivotCenterDiff(GameObject target)
    {
        Mesh uMesh = target.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices = uMesh.vertices;

        Vector3 sum = new Vector3();

        for (int i = 0; i < vertices.Length; i++)
        {
            sum += vertices[i];
        }
        Vector3 triCenter = sum / vertices.Length;
        Vector3 pivot = target.transform.InverseTransformPoint(target.transform.position);
        return pivot - triCenter;
    }
    /// <summary>
    /// Sets the pivot of the target object to it's center
    /// </summary>
    /// <param name="target">Target Gameobject</param>
    /// <param name="diff">the distance from pivot to center</param>
    private static void centerMeshPivot(GameObject target, Vector3 diff)
    {
        //initialize mesh and vertices variables from source
        Mesh uMesh = target.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices = uMesh.vertices;

        //calculate adjusted vertices
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += diff;
        }
        //set adjusted vertices
        uMesh.vertices = vertices;

        //calculate and assign adjusted trasnsform position
        Vector3 pivot = target.transform.InverseTransformPoint(target.transform.position);
        target.transform.localPosition = target.transform.TransformPoint(pivot - diff);
        
    }
    /// <summary>
    /// assigns a new material for a fragment
    /// </summary>
    /// <param name="newSprite">sprite of the fragment</param>
    /// <param name="source">original gameobject that was shattered</param>
    private static Material createFragmentMaterial(DestructInfo info)
    {
        Material mat = new Material(info.SpriteRenderer.sharedMaterial);
        mat.SetTexture("_MainTex", info.SpriteRenderer.sprite.texture);
        mat.color = info.DestroyCcolor;
        return mat;
    }
}
