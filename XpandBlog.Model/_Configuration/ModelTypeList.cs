using System;
using System.Collections.Generic;
using XpandBlog.Model.Security;

namespace XpandBlog.Model
{
    public static class ModelTypeList
    {
        public static IEnumerable<Type> ExportedTypes
        {
            get
            {
                return new[]
                {
                    typeof(User),
                    typeof(Role)
                };
            }
        }
    }
}