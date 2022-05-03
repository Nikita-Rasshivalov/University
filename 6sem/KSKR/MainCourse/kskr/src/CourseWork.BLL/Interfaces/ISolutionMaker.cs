using CourseWork.BLL.Models;

namespace CourseWork.BLL.Interfaces
{
    public interface ISolutionMaker
    {
        double[] GetNodeDisplacements(FiniteElementModel model, DetailMaterial material);

        double[] GetElementsDisplacement(FiniteElementModel model, double[] nodeDisplacements, double coefficient = 1000);
        public double[] GetElementsStrain(FiniteElementModel originalMesh, double[] nodesDisplacement);
        public double[] GetElementsStresses(FiniteElementModel originalMesh, double[] nodesDisplacement, DetailMaterial material);
    }
}
