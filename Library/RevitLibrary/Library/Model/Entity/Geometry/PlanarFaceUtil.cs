using Autodesk.Revit.DB;

namespace Utility
{
    public static class PlanarFaceUtil
    {
        public static Transform GetTransform(this PlanarFace planarFace)
        {
            var transform = Transform.Identity;
            transform.Origin = planarFace.Origin;
            transform.BasisX = planarFace.XVector;
            transform.BasisY = planarFace.YVector;
            transform.BasisZ = planarFace.FaceNormal;
            return transform;
        }

        public static BoundingBoxXYZ GetBoundingBoxXYZ(this PlanarFace planarFace)
        {
            var transform = planarFace.GetTransform();
            var bbUV = planarFace.GetBoundingBox();
            return new BoundingBoxXYZ
            {
                Min = transform.OfPoint(new XYZ(bbUV.Min.U, bbUV.Min.V, 0)),
                Max = transform.OfPoint(new XYZ(bbUV.Max.U, bbUV.Max.V, 0)),
            };
        }
    }
}
