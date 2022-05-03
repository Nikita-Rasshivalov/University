using CourseWork.BLL.Interfaces;
using CourseWork.BLL.ModelMakers;
using CourseWork.BLL.Models;
using CourseWork.BLL.UIElements;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CourseWork.UI
{
    public class MainViewModel
    {
        private readonly ISolutionMaker _solutionMaker;
        private readonly IMeshModelMaker _geometryMaker;
        public MainViewModel(IElementsProvider elementsProvider, IMaterialsProvider materialsProvider, ISolutionMaker solutionMaker, IMeshModelMaker geometryMaker, DetailMaterial material = default)
        {
            OriginalModel = elementsProvider.GetModel();
            Materials = materialsProvider.GetMaterials();
            _solutionMaker = solutionMaker;
            _geometryMaker = geometryMaker;
            if(Materials.Count == 0)
            {
                Materials.Add(new DetailMaterial("Steel", 200000000000, 0.3, Color.FromArgb(0x99, 0x53, 0xd2, 0xf5)));
            }
            CurrentMaterial = Materials.First();
        }


        public double Coefficient { get; set; }

        public string CurrentFileType { get; set; }

        public FiniteElementModel OriginalModel { get; set; }

        public FiniteElementModel CurrentModel { get; set; }

        public Model3DGroup OriginalModelGeometry
        {
            get
            {
                return _geometryMaker.GenerateGeometry(OriginalModel, CurrentMaterialGroup);
            }
        }

        public Model3DGroup DisplacedModelGeometry
        {
            get
            {
                CurrentModel = (FiniteElementModel)OriginalModel.Clone();
                for(var i = 0; i < CurrentModel.Nodes.Count; i++)
                {
                    CurrentModel.Nodes[i].Position.X += SolutionNodesDisplacement[i * 3];
                    CurrentModel.Nodes[i].Position.Y += SolutionNodesDisplacement[i * 3 + 1];
                    CurrentModel.Nodes[i].Position.Z += SolutionNodesDisplacement[i * 3 + 2];
                }

                SolutionElementsDisplacement = _solutionMaker.GetElementsDisplacement(CurrentModel, SolutionNodesDisplacement, Coefficient);
                return _geometryMaker.GenerateCharacteristicModel(CurrentModel, SolutionElementsDisplacement);
            }
        }

        private Transform3DGroup _transforms;
        public Transform3DGroup Transforms
        {
            get
            {
                if (_transforms == null)
                {
                    const double scale = 40;
                    _transforms = new Transform3DGroup();
                    _transforms.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 30)));
                    _transforms.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 30)));
                    _transforms.Children.Add(new ScaleTransform3D(scale, scale, scale, 0, 0, 0));
                }
                return _transforms;
            }
        }

        public void Scale(bool upscale = true)
        {
            if (upscale)
            {
                ((ScaleTransform3D)(Transforms).Children[2]).ScaleX *= 1.2;
                ((ScaleTransform3D)(Transforms).Children[2]).ScaleY *= 1.2;
                ((ScaleTransform3D)(Transforms).Children[2]).ScaleZ *= 1.2;
            }
            else
            {
                ((ScaleTransform3D)(Transforms).Children[2]).ScaleX *= 0.8;
                ((ScaleTransform3D)(Transforms).Children[2]).ScaleY *= 0.8;
                ((ScaleTransform3D)(Transforms).Children[2]).ScaleZ *= 0.8;
            }
        }

        public void Rotate(double xRotate, double yRotate)
        {
            ((AxisAngleRotation3D)((RotateTransform3D)(Transforms).Children[0]).Rotation).Angle += xRotate;
            ((AxisAngleRotation3D)((RotateTransform3D)(Transforms).Children[1]).Rotation).Angle += yRotate;
        }

        public IList<DetailMaterial> Materials { get; }

        public DetailMaterial CurrentMaterial { get; set; }

        public MaterialGroup CurrentMaterialGroup
        {
            get
            {
                MaterialGroup geometryMaterial = new MaterialGroup();
                geometryMaterial.Children = new MaterialCollection();
                geometryMaterial.Children.Add(new DiffuseMaterial(new SolidColorBrush(CurrentMaterial.Color)));

                return geometryMaterial;
            }
        }

        public FiniteElementModel SolutionMesh { get; set; }
        public Model3DGroup SolutionMeshModel
        {
            get
            {
                return _geometryMaker.GenerateGeometry(SolutionMesh, null);
            }
        }
        public double[] SolutionNodesDisplacement { get; set; }

        public void Solve()
        {
            SolutionNodesDisplacement = _solutionMaker.GetNodeDisplacements(OriginalModel, CurrentMaterial);
            CurrentModel = (FiniteElementModel)OriginalModel.Clone();
        }

        public double[] SolutionElementsDisplacement { get; set; }

        public GradientScale DisplacementGradientScale
        {
            get
            {
                return new GradientScale
                    (
                    new BLL.Tools.GradientMaker(),
                    SolutionElementsDisplacement.Min(),
                    SolutionElementsDisplacement.Max(),
                    SolutionElementsDisplacement[2],
                    SolutionElementsDisplacement[SolutionElementsDisplacement.Length - 3],
                    10
                    );
            }
        }

        public double[] SolutionElementsStrain { get; set; }

        public Model3DGroup GetStrainMeshModel()
        {
            CurrentModel = (FiniteElementModel)OriginalModel.Clone();
            SolutionElementsStrain = _solutionMaker.GetElementsStrain(OriginalModel, SolutionNodesDisplacement);
            return _geometryMaker.GenerateCharacteristicModel(CurrentModel, SolutionElementsStrain);
        }

        public GradientScale StrainGradientScale
        {
            get
            {
                return new GradientScale
                    (
                    new BLL.Tools.GradientMaker(),
                    SolutionElementsStrain.Min(),
                    SolutionElementsStrain.Max(),
                    SolutionElementsStrain[1],
                    SolutionElementsStrain[SolutionElementsStrain.Length - 2],
                    10
                    );
            }
        }


        public double[] SolutionElementsStresses { get; set; }

        public Model3DGroup GetStressMeshModel()
        {
            CurrentModel = (FiniteElementModel)OriginalModel.Clone();
            SolutionElementsStresses = _solutionMaker.GetElementsStresses(OriginalModel, SolutionNodesDisplacement, CurrentMaterial);
            return _geometryMaker.GenerateCharacteristicModel(CurrentModel, SolutionElementsStresses);
        }

        public GradientScale StressesGradientScale
        {
            get
            {
                return new GradientScale
                    (
                    new BLL.Tools.GradientMaker(),
                    SolutionElementsStresses.Min(),
                    SolutionElementsStresses.Max(),
                    SolutionElementsStresses[1],
                    SolutionElementsStresses[SolutionElementsStresses.Length - 2],
                    10
                    );
            }
        }
    }
}
