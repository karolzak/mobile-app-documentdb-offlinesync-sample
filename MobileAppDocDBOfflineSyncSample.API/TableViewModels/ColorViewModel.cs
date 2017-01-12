using Microsoft.Azure.Mobile.Server.Tables;
using MobileAppDocDBOfflineSyncSample.API.Helpers;
using MobileAppDocDBOfflineSyncSampleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileAppDocDBOfflineSyncSample.API.TableViewModels
{
    public class ColorViewModel : IConvertableViewModel<Color>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Business { get; set; }

        public string Market { get; set; }

        public byte[] Version { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public bool Deleted { get; set; }

        public void ApplyToDocument(Color document)
        {
            document.Name = Name;
            document.Business = Business;
            document.Market = Market;
            document.Version = Version;
            document.CreatedAt = CreatedAt;
            document.UpdatedAt = UpdatedAt;
            document.Deleted = Deleted;
        }

        public void CopyFromDocument(Color document)
        {
            Id = document.Id;
            Name = document.Name;
            Business = document.Business;
            Market = document.Market;
            Version = document.Version;
            CreatedAt = document.CreatedAt;
            UpdatedAt = document.UpdatedAt;
            Deleted = document.Deleted;
        }
    }
}