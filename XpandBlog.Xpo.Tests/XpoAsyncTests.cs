using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XpandBlog.Xpo.Tests
{
    [TestClass]
    public class XpoAsyncTests
    {

        private IDataLayer _DataLayer;

        [TestInitialize]
        public void Initialize()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            _DataLayer = CreateDataLayer();

            using (var uow = CreateUnitOfWork())
            {
                for (int i = 0; i < 100; i++)
                {
                    new TestObject(uow)
                    {
                        Name = "Name" + i
                    };
                }
                uow.CommitChanges();
            }
        }

        private IDataLayer CreateDataLayer()
        {
            XPDictionary dictionary = new ReflectionDictionary();
            dictionary.CollectClassInfos(typeof(XpoAsyncTests).Assembly);

            var memoryDataStore = new InMemoryDataStore(AutoCreateOption.DatabaseAndSchema);

            return new ThreadSafeDataLayer(dictionary, memoryDataStore);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _DataLayer.Dispose();
        }

        private UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(_DataLayer, new IDisposable[] { });
        }


        [TestMethod]
        public async Task TestFindObjectAsync()
        {
            using (var uow = CreateUnitOfWork())
            {
                var result = await uow.FindObjectAsync<TestObject>(new BinaryOperator("Name", "Name1", BinaryOperatorType.Equal));

                Assert.AreEqual("Name1", result.Name);
            }
        }

        [TestMethod]
        public async Task TestFindObjectsAsync_WithOffset()
        {
            using (var uow = CreateUnitOfWork())
            {
                const int skippedRecords = 10;
                const int topRecords = 50;

                var result = await uow.GetObjectsAsync<TestObject>(CriteriaOperator.Parse("1=1"), new SortingCollection(new SortProperty("Oid", SortingDirection.Ascending)), skipSelectedRecords: skippedRecords, topSelectedRecords: topRecords);

                var items = result.Select(m => m.Name).ToList();
                var expecedItems = Enumerable.Range(skippedRecords, topRecords).Select(m => "Name" + m).ToList();

                CollectionAssert.AreEquivalent(expecedItems, items);
            }
        }

        [TestMethod]
        public async Task TestFindObjectsAsync()
        {
            using (var uow = CreateUnitOfWork())
            {
                var result = await uow.GetObjectsAsync<TestObject>(CriteriaOperator.Parse("1=1"));

                var items = result.Select(m => m.Name).ToList();

                Assert.AreEqual(100, items.Count);
            }
        }


        [TestMethod]
        public async Task TestModifyAndCommitAsyncChanges()
        {
            TestObject result;
            using (var uow = CreateUnitOfWork())
            {
                result = await uow.FindObjectAsync<TestObject>(CriteriaOperator.Parse("[Name] == 'MyObj1'"));

                if (result == null)
                {
                   result= new TestObject(uow) { Name = "MyObj1" };
                }

                uow.CommitChanges();
            }

            using (var uow = CreateUnitOfWork())
            {
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

                result = await uow.FindObjectAsync<TestObject>(CriteriaOperator.Parse("[Name] == ?", result.Name));

                Assert.AreEqual("MyObj1", result.Name);

                result.Name = "ChangeMe";

                uow.CommitChanges();

                Assert.AreEqual("ChangeMe", result.Name);
            }
        }

    }
}