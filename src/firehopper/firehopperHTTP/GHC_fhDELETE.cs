﻿using System;
using Grasshopper.Kernel;

namespace firehopper
{
    public class fhDELETE : GH_Component
    {
        public string apiKey;
        public string databaseURL;
        public string databaseNode;
        public bool trigger;

        public string response;
        private bool asyncDone = false;
        private bool asyncCalled = false;

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public fhDELETE()
          : base("Firehopper DELETE", "fhDELETE",
              "Trigger DELETE request to delete key-value pair data in Google Firebase",
              "Firehopper", "HTTP")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("API Key", "AK", "API Key provided by Firebase", GH_ParamAccess.item);
            pManager.AddTextParameter("Database URL", "U", "Database URL provided by Firebase", GH_ParamAccess.item);
            pManager.AddTextParameter("Database Node", "N", "Database Node in the Firebase", GH_ParamAccess.item, "");
            pManager.AddBooleanParameter("Trigger", "T", "Trigger the PUT request", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Status", "S", "Status received from Firebase", GH_ParamAccess.item);
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

            DA.IncrementIteration();
            DA.SetData(0, response);
        }

        /// <summary>
        /// This is the method called after setting up the output data.
        /// It is necessary to enable the asynchronous behaviour.
        /// </summary>
        protected override void AfterSolveInstance()
        {
            base.AfterSolveInstance();

            if (asyncDone == true && asyncCalled == true)
            {
                asyncDone = false;
                asyncCalled = false;
            }
            else if (asyncDone == false && asyncCalled == false)
            {
                if (trigger == true)
                {
                    try
                    {
                        firebaseManager.deleteAsync(apiKey, databaseURL, databaseNode).ContinueWith(r =>
                        {
                            Grasshopper.Instances.ActiveCanvas.Invoke((Action)delegate
                            {
                                response = r.Result;
                                asyncDone = true;
                                ExpireSolution(true);
                            });
                        });
                        asyncCalled = true;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }
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
                return Resources.Resources.firehopper_icon_delete;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8991293d-984b-4b2c-9c40-0b0cf5fd8a50"); }
        }
    }
}
