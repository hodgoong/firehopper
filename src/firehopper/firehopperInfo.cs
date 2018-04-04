using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace firehopper
{
    public class firehopperInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "firehopper";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "Grasshopper component RESTful interface for Google Firebase";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("e8c5deef-782e-41ee-a006-de54f85c9680");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Hojoong Chung";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "hodgoong@gmail.com";
            }
        }
    }
}
