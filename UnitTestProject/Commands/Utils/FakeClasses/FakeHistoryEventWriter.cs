using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models.Contracts;
namespace WIMSystem.Tests.Commands.Utils.FakeClasses
{
    public class FakeHistoryEventWriter : HistoryEventWriter
    {
        public FakeHistoryEventWriter(IHistoryItemsCollection historyItemsList, IComponentsFactory factory) : base(historyItemsList, factory)
        {
        }
        public IHistoryItemsCollection HistoryItemsList
        {
            get
            {
                return base.historyItemsList;
            }
        }
        public IComponentsFactory Factory
        {
            get
            {
                return base.factory;
            }
        }
    }
}
