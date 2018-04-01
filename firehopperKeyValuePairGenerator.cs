using System;
using System.Collections.Generic;
using Grasshopper.Kernel;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace firehopper
{
    public class firehopperKeyValuePairGenerator : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public firehopperKeyValuePairGenerator()
          : base("Firehopper Key-Value Pair Generator", "fhKeyValueGen",
              "GET request to fetch data from Google Firebase",
              "Firehopper", "HTTP")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("List of Keys", "KEY", "apiKey provided by Firebase", GH_ParamAccess.list);
            pManager.AddTextParameter("List of Values", "VAL", "databaseURL provided by Firebase", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("List of Key-Value Pairs", "KV", "JSON String received from the Firebase", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<string> keys = new List<string>();
            List<string> values = new List<string>();
            string keyValuePairs;

            DA.GetDataList(0, keys);
            DA.GetDataList(1, values);  

            if (keys.Count == values.Count)
            {
                DA.DisableGapLogic();
                try
                {
                    keyValuePairs = "{";
                    for(int i = 0; i < keys.Count; i++)
                    {
                        string template = "\"{0}\":\"{1}\"";
                        keyValuePairs += string.Format(template, keys[i], values[i]);
                        //keyValuePairs += "\"" + keys[i] + "\":" + "\"" + values[i] + "\"";
                        if (i < keys.Count - 1)
                        {
                            keyValuePairs += ",";
                        }
                    }
                    keyValuePairs += "}";
                    
                    DA.IncrementIteration();
                    DA.SetData(0, keyValuePairs);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
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
            get { return new Guid("13653ba3-51b9-4939-90ef-96cd678d7771"); }
        }
    }
}
