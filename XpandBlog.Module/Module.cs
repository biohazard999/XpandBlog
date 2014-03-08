using System;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using XpandBlog.Model;

namespace XpandBlog.Module
{
    public interface IUserMapper<T, TUser, TUserPoco> 
        where T : class 
        where TUser : T 
        where TUserPoco : T
    {
        TUser MapUser(TUserPoco from, TUser to);
        TUserPoco MapUser(TUser from, TUserPoco to);
    }

    public abstract class UserMapper<T, TUser, TUserPoco> : IUserMapper<T, TUser, TUserPoco> where T : class
        where TUser : T
        where TUserPoco : T
    {

        public abstract TUser MapUser(TUserPoco from, TUser to);

        public abstract TUserPoco MapUser(TUser from, TUserPoco to);
    }


    public sealed class XpandBlogModule : ModuleBase
    {
        protected override ModuleTypeList GetRequiredModuleTypesCore()
        {
            return new ModuleTypeList(
                 typeof(DevExpress.ExpressApp.SystemModule.SystemModule)
                 );
        }

        protected override IEnumerable<Type> GetDeclaredExportedTypes()
        {
            return ModelTypeList.ExportedTypes;
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
