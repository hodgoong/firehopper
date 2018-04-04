using System;
using System.Collections.Generic;
using Grasshopper.Kernel;

namespace firehopper
{
    public class fhKeyValuePair : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public fhKeyValuePair()
          : base("Firehopper Key-Value Pair Generator", "fhKeyValuePair",
              "Generate key-value pairs from the given list inputs",
              "Firehopper", "Data")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("List of Keys", "K", "List of keys", GH_ParamAccess.list);
            pManager.AddTextParameter("List of Values", "V", "List of values", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("List of Key-Value Pairs", "KVP", "Generated list of key-value pairs", GH_ParamAccess.item);
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
                return Resources.Resources.firehopper_icon_keyval;
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
