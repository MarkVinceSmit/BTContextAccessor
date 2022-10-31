// BizTalk 2006 Context Accessor Functoid
// 1st Februari 2007
// Marvin Smit, 
// Common Sense Solutions
namespace www.marvdashin.net.biztalk2006.functoids
{
    #region imported assemblies
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Reflection;

    using System.Drawing;
    using System.Runtime.InteropServices;

    using Microsoft.BizTalk.Component.Interop;
    using Microsoft.BizTalk.BaseFunctoids;
    using System.Diagnostics;
    using Microsoft.BizTalk.Message.Interop;
    #endregion

    /// <summary>
    /// This is the main Context Accesor functoid.
    /// It requires a pipeline with the ContextProxyComponent
    /// inserted at the &quot;PartyResolution&quote; stage.
    /// </summary>
    public class ContextAccessor : BaseFunctoid
    {
        public ContextAccessor() : base()
        {
            // Specify an iunique ID for this functoid.
            this.ID = 54009;

            // set up the resource assembly to our own assembly
            this.SetupResourceAssembly("ContextAccessor.Resources", Assembly.GetExecutingAssembly());

            // Set up the name, description and tooltip.
            // (as resource entries)
            this.SetName("CAFUNC_NAME");
            this.SetDescription("CAFUNC_DESC");
            this.SetTooltip("CAFUNC_TOOLTIP");
            // Bitmap = 16x16
            this.SetBitmap("CAFUNC_BITMAP");

            // We are expecting Three parameter, no optionals
            // The first will be the context property name
            // the second the namespace the property is defined in.
            // The third is optional and is the replacement value if the requested 
            // context property could not be found
            this.HasVariableInputs = true;
            this.SetMinParams(2);
            this.SetMaxParams(3);

            // Set the Assembly, Class and Method specification for the functionality
            // that will be executed when this functoid is used.
            this.SetExternalFunctionName(GetType().Assembly.FullName, "www.marvdashin.net.biztalk2006.functoids.ContextAccessor", "AccessContext");

            // Bind to the Advanced category
            // This determines the location in the toolbox within Visual Studio
            this.Category = FunctoidCategory.Unknown;
            
            // We output a field.
            this.OutputConnectionType = ConnectionType.Field;

            // We only allows fields or elements as input.
            this.AddInputConnectionType(ConnectionType.AllExceptRecord);
            this.AddInputConnectionType(ConnectionType.AllExceptRecord);
        }

        public string AccessContext(string contextItemName, string contextItemNamespace)
        {
            return this.AccessContext(contextItemName, contextItemNamespace, "");
        }

        // This is the actuall method retrieving properties from the context.
        // The context interface is stored by the pipeline component in the thread static
        // storage.
        // This implies that: this functoid only works if the counterpart pipeline component is used.
        //                    this functoid relies on the assumption that there will never be a thread 
        //                     switch between the completion of the pipeline and the start of the map
        public string AccessContext(string contextItemName, string contextItemNamespace, string replacementValue)
        {
            object retval = null;
            try
            {
                // Try to retrieve the context stored by the pipeline component.
                IBaseMessageContext ibMC = ContextProxyComponent.context;
                if (ibMC != null)
                {
                    // if it's available 
                    retval = ibMC.Read(contextItemName, contextItemNamespace);
                }
            }
            catch
            {
                // If an exception occurs while retrieving the interface to the context or
                // accessing the properties with that interface, we ignore the failure and 
                // force the retval to null.
                retval = null;
            }

            // if retval has not been filled at this point, either an exception occured or
            // the ItemName+ItemNamespaceUri is non existing or empty.
            // we will replace the value with the given replacement value.
            if (retval == null)
            {
                retval = replacementValue;
            }

            // return the value to the Xslt engine.
            return retval as string;
        }       
    }
}