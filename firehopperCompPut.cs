using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace firehopper
{
    public class firehopperCompPut : GH_Component
    {
        public string apiKey;
        public string databaseURL;
        public string databaseNode;
        public string keyValuePair;
        public bool trigger;

        public string response;

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public firehopperCompPut()
          : base("Firehopper Put", "PUT",
              "Create header for http request to Google Firebase using Firebase credentials",
              "Firehopper", "Firehopper basic")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("apiKey", "key", "apiKey provided by Firebase", GH_ParamAccess.item);
            pManager.AddTextParameter("databaseURL", "url", "databaseURL provided by Firebase", GH_ParamAccess.item);
            pManager.AddTextParameter("databaseNode", "node", "databaseNode in the Firebase", GH_ParamAccess.item, "");
            pManager.AddTextParameter("keyValuePair", "value", "key-value pair to store in the Firebase", GH_ParamAccess.item);
            pManager.AddBooleanParameter("trigger", "trigger", "Trigger the GET request", GH_ParamAccess.item);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("response", "res", "Response received from the Firebase", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override async void SolveInstance(IGH_DataAccess DA)
        {
            DA.GetData<string>(0, ref apiKey);
            DA.GetData<string>(1, ref databaseURL);
            DA.GetData<string>(2, ref databaseNode);
            DA.GetData<string>(3, ref keyValuePair);
            DA.GetData<bool>(4, ref trigger);

            if (trigger == true)
            {
                try
                {
                    response = await putAsync(apiKey, databaseURL, databaseNode, keyValuePair);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            if (trigger == false)
            {
                response = null;
            }

            DA.SetData(0, response);
        }

        public static async Task<string> putAsync(string _apiKey, string _databaseURL, string _databaseNode, string _keyValuePair)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_databaseURL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", "Bearer " + _apiKey);

            HttpContent httpContent = new StringContent(_keyValuePair);
            HttpResponseMessage res = await httpClient.PutAsync(_databaseURL + _databaseNode + ".json", httpContent);

            return res.StatusCode.ToString();
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
            get { return new Guid("39aa9974-c135-4e21-bcdf-1c4d4b0caf49"); }
        }
    }
}
