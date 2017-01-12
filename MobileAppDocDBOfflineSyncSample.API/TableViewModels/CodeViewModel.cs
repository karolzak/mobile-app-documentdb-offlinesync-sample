using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileAppDocDBOfflineSyncSample.API.TableViewModels
{
    public class CodeViewModel : ITableData
    {
        public string ColorId { get; set; }

        public string Id { get; set; }

        public string System { get; set; }

        public string Value { get; set; }

        public byte[] Version { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}