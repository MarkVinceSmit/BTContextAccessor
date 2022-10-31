// BizTalk 2006 Context Accessor Functoid
// 1st Februari 2007
// Marvin Smit, 
// Common Sense Solutions
namespace www.marvdashin.net.biztalk2006.functoids
{
    #region Imported Assemblies
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.BizTalk.Component.Interop;
    using Microsoft.BizTalk.Message.Interop;
    using System.Reflection;
    using System.Drawing;
    using System.Resources;
    using System.ComponentModel;
    #endregion

    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    [ComponentCategory(CategoryTypes.CATID_PartyResolver)]
    [System.Runtime.InteropServices.Guid("1AF374CB-20D0-464a-AC56-45BFABE390A2")]
    public class ContextProxyComponent : IBaseComponent, Microsoft.BizTalk.Component.Interop.IComponent, IComponentUI, IPersistPropertyBag
    {
        private System.Resources.ResourceManager resManager;
        
        public ContextProxyComponent() : base()
        {
            resManager = new ResourceManager("ContextAccessor.Resources", 
                                              Assembly.GetExecutingAssembly());
        }

        #region IBaseComponent Members

        [Browsable(false)]
        public string Description
        {
            get 
            {
                return "Enables access to the message context for the ContextAccesor functoid";
            }
        }

        [Browsable(false)]
        public string Name
        {
            get 
            {
                return "ContextAccessorProvider";
            }
        }

        [Browsable(false)]
        public string Version
        {
            get
            {
                return "1.0.0.0";
            }
        }

        #endregion

        #region IComponent Members

        [ThreadStatic]
        internal static IBaseMessageContext context;

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            context = pInMsg.Context;
            return pInMsg;
        }

        #endregion

        #region IComponentUI Members
        [Browsable(false)]
        public IntPtr Icon
        {
            get 
            { 
                // The image used in design time.
                return ((Icon)resManager.GetObject("CPC_ICON")).Handle;
            }
        }

        public System.Collections.IEnumerator Validate(object projectSystem)
        {
            // We have no validation to do.
            return null;
        }

        #endregion

        #region IPersistPropertyBag Members

        public void GetClassID(out Guid classID)
        {
            classID = new Guid("1AF374CB-20D0-464a-AC56-45BFABE390A2");
        }

        public void InitNew()
        {
            
        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            
        }

        #endregion
    }
}
