using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.Helpers;

namespace XpandBlog.Xpo
{
    public static class SessionAsyncExtentions
    {
        public static Task<T> FindObjectAsync<T>(this Session session, CriteriaOperator criteriaOperator)
        {
            var tcs = new TaskCompletionSource<T>();

            AsyncFindObjectCallback h = null;

            h = (o, exception) =>
            {
                h = null;
                if (exception != null)
                {
                    tcs.SetException(exception);
                }
                else
                {
                    tcs.SetResult((T)o);
                }
            };

            session.FindObjectAsync<T>(criteriaOperator, h);

            return tcs.Task;
        }

        public static Task<ICollection<T>> GetObjectsAsync<T>(this Session session, CriteriaOperator criteriaOperator = null, SortingCollection sorting = null, int skipSelectedRecords = 0, int topSelectedRecords = int.MaxValue, bool selectDeleted = false, bool force = false)
        {
            var tcs = new TaskCompletionSource<ICollection<T>>();

            AsyncLoadObjectsCallback h = null;

            h = (collections, exception) =>
            {
                h = null;
                if (exception != null)
                {
                    tcs.SetException(exception);
                }
                else
                {
                    tcs.SetResult(collections[0].OfType<T>().ToArray());
                }
            };

            session.GetObjectsAsync(session.GetClassInfo<T>(), criteriaOperator, sorting, skipSelectedRecords, topSelectedRecords, selectDeleted, force, h);

            return tcs.Task;
        }
    }
}