using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSample.API.Helpers
{
    public interface IConvertableViewModel<TDocument> : ITableData
    {
        void ApplyToDocument(TDocument document);

        void CopyFromDocument(TDocument document);
    }
}
