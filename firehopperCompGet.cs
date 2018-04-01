using System;
using Grasshopper.Kernel;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace firehopper
{
    public class firehopperCompGet : GH_Component
    {
        public string apiKey;
        public string databaseURL;
        public string databaseNode;
        public bool trigger;

        public string response;

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public firehopperCompGet()
          : base("Firehopper Get", "fhGET",
              "GET request to fetch data from Google Firebase",
              "Firehopper", "HTTP")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("apiKey", "KEY", "apiKey provided by Firebase", GH_ParamAccess.item);
            pManager.AddTextParameter("databaseURL", "URL", "databaseURL provided by Firebase", GH_ParamAccess.item);
            pManager.AddTextParameter("databaseNode", "NO", "databaseNode in the Firebase", GH_ParamAccess.item, "");
            pManager.AddBooleanParameter("trigger", "T", "Trigger the GET request", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("JSON String", "J", "JSON String received from the Firebase", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            DA.GetData<string>(0, ref apiKey);
            DA.GetData<string>(1, ref databaseURL);
            DA.GetData<string>(2, ref databaseNode);
            DA.GetData<bool>(3, ref trigger);

            if (trigger == true)
            {
                DA.DisableGapLogic();
                try
                {
                    response = firebaseManager.getSync(apiKey, databaseURL, databaseNode);
                    DA.IncrementIteration();
                    DA.SetData(0, response);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            if (trigger == false)
            {
                response = null;
                DA.IncrementIteration();
                DA.SetData(0, response);
            }
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e0c92102-0f6f-4434-8de0-f1774eb2d70c"); }
        }
    }
}
