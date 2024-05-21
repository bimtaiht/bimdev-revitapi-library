using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Model.EntException;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    public static class MEPCurveUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static ElementId GetLevelId(this MEPCurve mepCurve)
        {
            return mepCurve.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM).AsElementId();
        }

        public static ElementId? GetSystemTypeId(this MEPCurve mepCurve)
        {
            return mepCurve.MEPSystem?.GetTypeId();
        }

        private static void UpdateParameter(this MEPCurve mepCurve, MEPCurve sourceMEPCurve)
        {
            switch (mepCurve)
            {
                case Pipe:
                    mepCurve.CopyParameterValue(sourceMEPCurve, BuiltInParameter.RBS_PIPE_DIAMETER_PARAM);
                    break;
                case Duct:
                    mepCurve.CopyParameterValue(sourceMEPCurve, BuiltInParameter.RBS_CURVE_WIDTH_PARAM);
                    mepCurve.CopyParameterValue(sourceMEPCurve, BuiltInParameter.RBS_CURVE_HEIGHT_PARAM);
                    break;
                case CableTray:
                    mepCurve.CopyParameterValue(sourceMEPCurve, BuiltInParameter.RBS_CABLETRAY_HEIGHT_PARAM);
                    mepCurve.CopyParameterValue(sourceMEPCurve, BuiltInParameter.RBS_CABLETRAY_WIDTH_PARAM);
                    break;
            }
        }

        public static (List<MEPCurve> MEPCurves, List<FamilyInstance> Fittings) CreateMEPCurveSystem(this List<XYZ> points, MEPCurve refMEPCurve, bool isAutoConnect = true)
        {
            switch (refMEPCurve)
            {
                case Pipe:
                    {
                        var mepCurveFttings = CreateMEPCurveSystem<Pipe>(points, refMEPCurve, isAutoConnect);
                        return (mepCurveFttings.MEPCurves.Cast<MEPCurve>().ToList(), mepCurveFttings.Fittings);
                    }
                case Duct:
                    {
                        var mepCurveFttings = CreateMEPCurveSystem<Duct>(points, refMEPCurve, isAutoConnect);
                        return (mepCurveFttings.MEPCurves.Cast<MEPCurve>().ToList(), mepCurveFttings.Fittings);
                    }
                case CableTray:
                    {
                        var mepCurveFttings = CreateMEPCurveSystem<CableTray>(points, refMEPCurve, isAutoConnect);
                        return (mepCurveFttings.MEPCurves.Cast<MEPCurve>().ToList(), mepCurveFttings.Fittings);
                    }
            }
            throw new UnreachableCodeException();
        }

        public static (List<T> MEPCurves, List<FamilyInstance> Fittings) CreateMEPCurveSystem<T>(this List<XYZ> points, MEPCurve refMEPCurve, bool isAutoConnect = true) where T : MEPCurve
        {
            var systemTypeId = refMEPCurve.GetSystemTypeId();
            var typeId = refMEPCurve.GetTypeId();
            var levelId = refMEPCurve.GetLevelId();

            Action<T> onCreated = (newMepCurve) => newMepCurve.UpdateParameter(refMEPCurve);

            return CreateMEPCurveSystem<T>(points, new MEPCurveCreateOption<T>
            {
                SystemTypeId = systemTypeId,
                TypeId = typeId,
                LevelId = levelId,
                MainDirection = refMEPCurve.Location.Convert<LocationCurve>()!.Curve.Convert<Line>()!.Direction,
                OnCreated = onCreated,
                IsAutoConnect = isAutoConnect
            }); ;
        }

        private static (List<T> MEPCurves, List<FamilyInstance> Fittings) CreateMEPCurveSystem<T>(this List<XYZ> points, MEPCurveCreateOption<T> option) where T : MEPCurve
        {
            var doc = revitData.Document;
            var mepCurves = new List<T>();

            var systemTypeId = option.SystemTypeId;
            var typeId = option.TypeId;
            var levelId = option.LevelId;
            var onCreated = option.OnCreated;
            var isAutoConnect = option.IsAutoConnect;
            var mainDir = option.MainDirection;

            var basisX = mainDir;
            var basisZ = XYZ.BasisZ;
            var basisY = basisZ.CrossProduct(basisX);

            void CheckAndRotateVerticalMEPCurve(MEPCurve mepCurve)
            {
                var mepCurveLoc = mepCurve!.Location.Convert<LocationCurve>()!;
                var mepCurveLine = mepCurveLoc.Curve.Convert<Line>()!;
                var mepCurveDir = mepCurveLine.Direction;
                if (mepCurveDir.IsParallel(XYZ.BasisZ))
                {
                    var mepCurveBasisY = mepCurveDir.IsSameDirection(XYZ.BasisZ) ? XYZ.BasisX : -XYZ.BasisX;
                    var angle = basisY.GetAngle(mepCurveBasisY);
                    mepCurveLoc.Rotate(mepCurveLine, angle);
                }
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                var startPoint = points[i];
                var endPoint = points[i + 1];

                T? mepCurve = null;
                switch (typeof(T).Name)
                {
                    case nameof(Pipe):
                        mepCurve = Pipe.Create(doc, systemTypeId, typeId, levelId, startPoint, endPoint) as T;
                        break;
                    case nameof(Duct):
                        mepCurve = Duct.Create(doc, systemTypeId, typeId, levelId, startPoint, endPoint) as T;
                        CheckAndRotateVerticalMEPCurve(mepCurve!);
                        break;
                    case nameof(CableTray):
                        mepCurve = CableTray.Create(doc, typeId, startPoint, endPoint, levelId) as T;
                        CheckAndRotateVerticalMEPCurve(mepCurve!);
                        break;
                }

                if (mepCurve is null) continue;
                onCreated?.Invoke(mepCurve);
                mepCurves.Add(mepCurve);
            }

            var fittings = isAutoConnect ? mepCurves.Cast<MEPCurve>().ToList().AutoConnect() : new();

            return (mepCurves, fittings);
        }

        private static FamilyInstance connectToByElbow(MEPCurve mepCurve1, MEPCurve mepCurve2)
        {
            var doc = revitData.Document;

            var connectors1 = mepCurve1.ConnectorManager.UnusedConnectors.Cast<Connector>();
            var connectors2 = mepCurve2.ConnectorManager.UnusedConnectors.Cast<Connector>();

            Connector? connector1 = null;
            Connector? connector2 = null;

            double? minDistance = null;
            foreach (var conn1 in connectors1)
            {
                var origin1 = conn1.Origin;
                foreach (var conn2 in connectors2)
                {
                    var origin2 = conn2.Origin;
                    var distance = (origin1 - origin2).GetLength();

                    if (minDistance == null || minDistance > distance)
                    {
                        connector1 = conn1;
                        connector2 = conn2;
                        minDistance = distance;
                    }
                }
            }

            var elbowFitting = doc.Create.NewElbowFitting(connector1, connector2);
            return elbowFitting;
        }

        public static FamilyInstance ConnectTo(this MEPCurve mepCurve1, MEPCurve mepCurve2)
        {
            return connectToByElbow(mepCurve1, mepCurve2);
        }

        public static FamilyInstance ConnectTo(this MEPCurve mepCurve1, MEPCurve mepCurve2, MEPCurve mepCurve3)
        {
            var doc = revitData.Document;

            var connectors1 = mepCurve1.ConnectorManager.UnusedConnectors.Cast<Connector>();
            var connectors2 = mepCurve2.ConnectorManager.UnusedConnectors.Cast<Connector>();

            Connector? connector1 = null;
            Connector? connector2 = null;

            double? minDistance = null;
            foreach (var conn1 in connectors1)
            {
                var origin1 = conn1.Origin;
                foreach (var conn2 in connectors2)
                {
                    var origin2 = conn2.Origin;
                    var distance = (origin1 - origin2).GetLength();

                    if (minDistance == null || minDistance > distance)
                    {
                        connector1 = conn1;
                        connector2 = conn2;
                        minDistance = distance;
                    }
                }
            }

            var pipeLine = ((mepCurve1.Location as LocationCurve)!.Curve as Line)!;
            var connectors3 = mepCurve3.ConnectorManager.UnusedConnectors.Cast<Connector>();

            Connector? connector3 = null;
            minDistance = null;

            foreach (var conn3 in connectors3)
            {
                var origin = conn3.Origin;
                var distance = (origin - pipeLine.GetProjectPoint(origin)).GetLength();
                if (minDistance == null || minDistance > distance)
                {
                    connector3 = conn3;
                    minDistance = distance;
                }
            }

            var teeFitting = doc.Create.NewTeeFitting(connector1, connector2, connector3);
            return teeFitting;
        }

        public static void ConnectTo(this MEPCurve mepCurve, FamilyInstance fittingOrFixture)
        {
            var connectors1 = mepCurve.ConnectorManager.UnusedConnectors.Cast<Connector>();
            var connectors2 = fittingOrFixture.MEPModel.ConnectorManager.UnusedConnectors.Cast<Connector>();

            Connector? connector1 = null;
            Connector? connector2 = null;

            foreach (var conn1 in connectors1)
            {
                if (connector1 != null && connector2 != null)
                {
                    break;
                }

                var connDir1 = conn1.CoordinateSystem.BasisZ;
                foreach (var conn2 in connectors2)
                {
                    var connDir2 = conn2.CoordinateSystem.BasisZ;
                    var dotProduct = connDir1.DotProduct(connDir2);
                    if (connDir1.IsOppositeDirection(connDir2, true))
                    {
                        connector1 = conn1;
                        connector2 = conn2;
                        break;
                    }
                }
            }

            connector1!.ConnectTo(connector2);
        }

        public static void ConnectTo(this FamilyInstance fittingOrFixture, MEPCurve mepCurve)
        {
            ConnectTo(mepCurve, fittingOrFixture);
        }

        public static List<FamilyInstance> AutoConnect<T>(this List<T> mepCurves) where T : MEPCurve
        {
            var fittings = new List<FamilyInstance>();
            for (int i = 0; i < mepCurves.Count - 1; i++)
            {
                var firstMepCurve = mepCurves[i];
                var secondMepCurve = mepCurves[i + 1];
                fittings.Add(firstMepCurve.ConnectTo(secondMepCurve));
            }
            return fittings;
        }

        public static List<FamilyInstance> AutoConnect<T>(this T mepCurve1, params T[] restMepCurves) where T : MEPCurve
        {
            var mepCurves = new List<T> { mepCurve1 };
            mepCurves.AddRange(restMepCurves);
            return mepCurves.AutoConnect();
        }

        public static List<FamilyInstance> AutoConnect<T>(this T mepCurve1, IEnumerable<T> restMepCurves) where T : MEPCurve
        {
            var mepCurves = new List<T> { mepCurve1 };
            mepCurves.AddRange(restMepCurves);
            return mepCurves.AutoConnect();
        }

        public static List<T> Split<T>(this T mepCurve, XYZ splitPoint, double offset = 0) where T : MEPCurve
        {
            var doc = mepCurve.Document;

            var location = mepCurve.Location.Convert<LocationCurve>()!;
            var line = location.Curve.Convert<Line>()!;
            var lineDir = line.Direction;

            var startPoint = line.GetEndPoint(0);
            var endPoint = line.GetEndPoint(1);

            // check endConnector
            var endConnector = mepCurve.ConnectorManager.Connectors.Cast<Connector>()
                .Where(conn => conn.IsConnected)
                .FirstOrDefault(conn => conn.Origin.IsEqual(endPoint));

            FamilyInstance? connect2EndConnector_Owner = null;
            if (endConnector != null)
            {
                var connect2EndConnector = endConnector.AllRefs.Cast<Connector>()
                    .First(conn => conn.Owner.Id != mepCurve.Id);

                connect2EndConnector_Owner = connect2EndConnector.Owner as FamilyInstance;
                endConnector.DisconnectFrom(connect2EndConnector);
            }

            // create pipe
            location.Curve = Line.CreateBound(startPoint, splitPoint - lineDir * offset);

            var systemTypeId = mepCurve.GetSystemTypeId();
            var typeId = mepCurve.GetTypeId();
            var levelId = mepCurve.GetLevelId();

            T? secondPartMepCurve = null;
            var secondStartPoint = splitPoint + lineDir * offset;
            switch (mepCurve)
            {
                case Pipe:
                    secondPartMepCurve = Pipe.Create(doc, systemTypeId, typeId, levelId, secondStartPoint, endPoint) as T;
                    break;
                case Duct:
                    secondPartMepCurve = Duct.Create(doc, systemTypeId, typeId, levelId, secondStartPoint, endPoint) as T;
                    break;
                case CableTray:
                    secondPartMepCurve = CableTray.Create(doc, typeId, secondStartPoint, endPoint, levelId) as T;
                    break;
            }
            secondPartMepCurve!.UpdateParameter(mepCurve);

            // set endConnector
            if (connect2EndConnector_Owner != null)
            {
                var secondEndConnector = secondPartMepCurve!.ConnectorManager.Connectors.Cast<Connector>()
                    .First(conn => conn.Origin.IsEqual(endPoint));
                var secondEndConnector_BasisZ = secondEndConnector.CoordinateSystem.BasisZ;

                var connect2EndConnector = connect2EndConnector_Owner.MEPModel.ConnectorManager.UnusedConnectors.Cast<Connector>()
                    .First(x => x.CoordinateSystem.BasisZ.IsOppositeDirection(secondEndConnector_BasisZ));

                secondEndConnector.ConnectTo(connect2EndConnector);
            }

            return new List<T> { mepCurve, secondPartMepCurve! };
        }

        public static Line GetLocationLine(this MEPCurve mepCurve)
        {
            return mepCurve.Location.Convert<LocationCurve>()!.Curve.Convert<Line>()!;
        }

        public static PlanarFace GetCrossSectionFace(this MEPCurve mepCurve)
        {
            var solid = mepCurve.get_Geometry(new Options()).Cast<GeometryObject>().OfType<Solid>().First();
            var locationDir = mepCurve.GetLocationLine().Direction;
            return solid.Faces.Cast<Face>().OfType<PlanarFace>().First(x => x.FaceNormal.IsParallel(locationDir));
        }

        public static double GetHeightByCrossSection(this MEPCurve mepCurve)
        {
            var face = mepCurve.GetCrossSectionFace();
            var bb = face.GetBoundingBox();
            return !face.XVector.IsParallel(XYZ.BasisZ) ? bb.Max.V - bb.Min.V : bb.Max.U - bb.Min.U;
        }

        public static double GetWidthByCrossSection(this MEPCurve mepCurve)
        {
            var face = mepCurve.GetCrossSectionFace();
            var bb = face.GetBoundingBox();
            return !face.XVector.IsParallel(XYZ.BasisZ) ? bb.Max.U - bb.Min.U : bb.Max.V - bb.Min.V;
        }

        internal class MEPCurveCreateOption<T> where T : MEPCurve
        {
            public ElementId? SystemTypeId { get; set; }
            public ElementId TypeId { get; set; } = ElementId.InvalidElementId;
            public ElementId LevelId { get; set; } = ElementId.InvalidElementId;
            public XYZ? MainDirection { get; set; }
            public Action<T>? OnCreated { get; set; }
            public bool IsAutoConnect { get; set; } = true;
        }
    }
}
