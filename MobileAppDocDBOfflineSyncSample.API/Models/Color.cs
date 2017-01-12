using Microsoft.Azure.Documents;
using Microsoft.Azure.Mobile.Server.Tables;
using MobileAppDocDBOfflineSyncSample.API.TableViewModels;
using MobileAppDocDBOfflineSyncSampleService.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppDocDBOfflineSyncSampleService.Models
{
    public class Color : DocumentResource, ITableData
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "business")]
        public string Business { get; set; }

        [JsonProperty(PropertyName = "market")]
        public string Market { get; set; }

        [JsonProperty(PropertyName = "codes")]
        public List<Code> Codes { get; set; }
    }
}
