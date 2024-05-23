using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEMASaw.DAO
{
    [Serializable]
    public class User
    {
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? TermDate { get; set; }
        public int EmployeeRole { get; set; }
        public string EmployeeRoleDesc { get; set; }
        public string EmployeePassword { get; set; }
        public bool FirstTimeLogin { get; set; }
        public bool bChangePassword { get; set; }
        public string HashedPassword { get; set; }
    }

    public class WOData
    {
        public string Material { get; set; }
        public string SliceBatch { get; set; }
        public double Density { get; set; }
        public double DensityTol { get; set; }
        public string VisualPartID { get; set; }
        public string Description { get; set; }
        public string SliceNum { get; set; }
        public string Thickness { get; set; }
        public string ThicknessTol { get; set; }
        
        public string TargetCellCount { get; set; }
        public string MinCellCount { get; set; }
        public string MaxCellCount { get; set; }
        public string CellCount { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Weight { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public int LastSliceNum { get; set; }
        public string Comments { get; set; }
    }
}