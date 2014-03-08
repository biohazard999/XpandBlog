using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;

namespace XpandBlog.Module.Win
{
    public sealed class XpandBlogWindowsFormsModule : ModuleBase
    {
        protected override ModuleTypeList GetRequiredModuleTypesCore()
        {
            return new ModuleTypeList(
                 typeof(DevExpress.ExpressApp.SystemModule.SystemModule),
                 typeof(DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule),
                 typeof(XpandBlogModule)
                 );
        }

        protected override IEnumerable<Type> GetDeclaredExportedTypes()
        {
            return Type.EmptyTypes;
        }

        protected override IEnumerable<Type> GetDeclaredControllerTypes()
        {
            return Type.EmptyTypes;
        }

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            return ModuleUpdater.EmptyModuleUpdaters;
        }
        protected override void RegisterEditorDescriptors(List<EditorDescriptor> editorDescriptors)
        {

        }
    }
}
