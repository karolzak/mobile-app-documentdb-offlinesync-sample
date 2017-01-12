using Microsoft.Azure.Mobile.Server.Tables;
using MobileAppDocDBOfflineSyncSample.API.Helpers;
using MobileAppDocDBOfflineSyncSampleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileAppDocDBOfflineSyncSample.API.TableViewModels
{
    public class CodeViewModel : IConvertableViewModel<Color>
    {
        public string ColorId { get; set; }

        public string Id { get; set; }

        public string System { get; set; }

        public string Value { get; set; }

        public byte[] Version { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public bool Deleted { get; set; }

        public void ApplyToDocument(Color document)
        {
            var existingCode = document.Codes.FirstOrDefault(x => x.System == System);
            if (existingCode == null)
            {
                document.Codes.Add(new Code()
                {
                    System = System,
                    Value = Value
                });
            }
            else
            {
                existingCode.Value = Value;
            }
        }

        public void CopyFromDocument(Color document)
        {
            throw new NotImplementedException();
        }
    }
}