using FEM.BLL.Data;
using FEM.BLL.Generators;
using FEM.BLL.ModelMakers;
using FEM.BLL.Services;
using FEM.BLL.Solvers;
using FEM.BLL.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FEM.WPFGUI
{
    public class MainViewModel
    {
        MeshService _meshService;
        MaterialService _materialService;
        NodeLoadService _nodeLoadService;
        IMeshModelMaker _geometryMaker;
        ISolver _solver;
        NodeLoadsGenerator _nodeLoadsGenerator;
        MeshGenerator _meshGenerator;
        public MainViewModel(MeshService meshService, MaterialService materialService, NodeLoadService nodeLoadService,
            IMeshModelMaker geometryMaker, ISolver solver, NodeLoadsGenerator nodeLoadsGenerator, MeshGenerator meshGenerator)
        {

            _meshService = meshService;
            _materialService = materialService;
            _nodeLoadService = nodeLoadService;
            _geometryMaker = geometryMaker;
            _solver = solver;
            _nodeLoadsGenerator = nodeLoadsGenerator;
            _meshGenerator = meshGenerator;
        }

        private MeshDTO _originalMesh;
        public MeshDTO OriginalMesh
        {
            get
            {
                if (_originalMesh == null)
                {
                    _originalMesh = _meshGenerator.GetCylinderMesh(0.2, 0.5);
                }
                return _originalMesh;
            }
        }

        public Model3DGroup OriginalMeshModel
        {
            get
            {
                return _geometryMaker.GenerateGeometry(OriginalMesh, MaterialGroup, NodeLoads);
            }
        }

        private Transform3DGroup _transforms;
        public Transform3DGroup Transforms
        {
            get
            {
                if (_transforms == null)
                {
                    _transforms = new Transform3DGroup();
                    _transforms.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 30)));
                    _transforms.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 30)));
                    _transforms.Children.Add(new ScaleTransform3D(7, 7, 7, 0, 0, 0));
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

        private IList<MaterialDTO> _materials;
        public IList<MaterialDTO> Materials
        {
            get
            {
                if (_materials == null)
                {
                    _materials = _materialService.Materials;
                }
                return _materials;
            }
        }

        private MaterialDTO _selectedMaterial;
        public MaterialDTO SelectedMaterial
        {
            get
            {
                if (_selectedMaterial == null)
                {
                    _selectedMaterial = Materials.LastOrDefault();
                }
                return _selectedMaterial;
            }
            set
            {
                _selectedMaterial = value;
            }
        }

        private MaterialDTO _currentMaterial;
        public MaterialDTO CurrentMaterial
        {
            get
            {
                if (_currentMaterial == null)
                {
                    _currentMaterial = Materials.LastOrDefault();
                }
                return _currentMaterial;
            }
            set
            {
                _currentMaterial = value;
            }
        }

        public MaterialGroup MaterialGroup
        {
            get
            {
                MaterialGroup geometryMaterial = new MaterialGroup();
                geometryMaterial.Children = new MaterialCollection();
                geometryMaterial.Children.Add(new DiffuseMaterial(new SolidColorBrush(CurrentMaterial.Color)));
                return geometryMaterial;
            }
        }

        public string GetSelectedMaterialInfo()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Материал {SelectedMaterial.Name}\n" +
                $"Модуль Юнга {SelectedMaterial.YoungModulus}\n" +
                $"Коэффициент Пуассона {SelectedMaterial.PuassonsCoefficient}");
            return builder.ToString();
        }

        public string GetSolutionInfo()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Сетка состоит из {OriginalMesh.Elements.Count} элементов и {OriginalMesh.Nodes.Count} узлов\n" +
                $"Текущий материал детали {CurrentMaterial.Name}");
            return builder.ToString();
        }

        private IList<NodeLoadDTO> _nodeLoads;
        public IList<NodeLoadDTO> NodeLoads
        {
            get
            {
                if (_nodeLoads == null)
                {
                    _nodeLoads = _nodeLoadService.NodeLoads;
                }
                return _nodeLoads;
            }
        }

        public void GeneratePressLoads(double value)
        {
            NodeLoads.Clear();
            foreach (var load in _nodeLoadsGenerator.GeneratePressLoads(OriginalMesh, value, Math.PI * Math.Pow(0.2, 2)))
            {
                NodeLoads.Add(load);
            }
        }

        public MeshDTO SolutionMesh { get; set; }
        public Model3DGroup SolutionMeshModel
        {
            get
            {
                return _geometryMaker.GenerateGeometry(SolutionMesh, MaterialGroup, NodeLoads);
            }
        }
        public double[] SolutionNodesDisplacement { get; set; }

        public void Solve()
        {
            SolutionNodesDisplacement = _solver.GetNodesDisplacement(OriginalMesh, CurrentMaterial, NodeLoads);
            SolutionMesh = _meshGenerator.GenerateMovedMesh(OriginalMesh, SolutionNodesDisplacement);
        }

        public double[] SolutionElementsDisplacement { get; set; }

        public Model3DGroup GetDisplacementMeshModel()
        {
            SolutionElementsDisplacement = _solver.GetElementsDisplacement(OriginalMesh, SolutionNodesDisplacement);
            return _geometryMaker.GenerateCharacteristicModel(SolutionMesh, SolutionElementsDisplacement);
        }

        public GradientScale DisplacementGradientScale
        {
            get
            {
                return new GradientScale(new BLL.Tools.GradientMaker(), SolutionElementsDisplacement.Min(), SolutionElementsDisplacement.Max(), 10);
            }
        }

        public double[] SolutionElementsStrain { get; set; }

        public Model3DGroup GetStrainMeshModel()
        {
            SolutionElementsStrain = _solver.GetElementsStrain(OriginalMesh, SolutionNodesDisplacement);
            return _geometryMaker.GenerateCharacteristicModel(SolutionMesh, SolutionElementsStrain);
        }

        public GradientScale StrainGradientScale
        {
            get
            {
                return new GradientScale(new BLL.Tools.GradientMaker(), SolutionElementsStrain.Min(), SolutionElementsStrain.Max(), 10);
            }
        }

        public double[] SolutionElementsStresses { get; set; }

        public Model3DGroup GetStressMeshModel()
        {
            SolutionElementsStresses = _solver.GetElementsStresses(OriginalMesh, SolutionNodesDisplacement, CurrentMaterial);
            return _geometryMaker.GenerateCharacteristicModel(SolutionMesh, SolutionElementsStresses);
        }

        public GradientScale StressesGradientScale
        {
            get
            {
                return new GradientScale(new BLL.Tools.GradientMaker(), SolutionElementsStresses.Min(), SolutionElementsStresses.Max(), 10);
            }
        }
    }
}
